using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssistantScrapMechanic.Domain.AppFiles;
using AssistantScrapMechanic.Domain.Constant;
using AssistantScrapMechanic.Domain.DataFiles;
using AssistantScrapMechanic.Domain.Dto.Enum;
using AssistantScrapMechanic.Domain.Dto.ViewModel;
using AssistantScrapMechanic.Domain.Entity;
using AssistantScrapMechanic.Domain.Enum;
using AssistantScrapMechanic.Domain.GameFiles;
using AssistantScrapMechanic.Domain.IntermediateFiles;
using AssistantScrapMechanic.Domain.Result;
using AssistantScrapMechanic.Integration;
using AssistantScrapMechanic.Integration.Repository;
using AssistantScrapMechanic.Logic;
using AssistantScrapMechanic.Logic.Calculator;
using AssistantScrapMechanic.Logic.Mapper.AppMapper;
using Newtonsoft.Json;

namespace AssistantScrapMechanic.GameFilesReader.FileHandlers
{
    public class DataFileHandler
    {
        private readonly FileSystemRepository _appDataSysRepo;
        private readonly FileSystemRepository _inputFileSysRepo;
        private readonly FileSystemRepository _attackFileSysRepo;
        private readonly FileSystemRepository _languageFileSysRepo;

        public DataFileHandler(FileSystemRepository inputFileSysRepo, FileSystemRepository appDataSysRepo, FileSystemRepository attackFileSysRepo, FileSystemRepository languageFileSysRepo)
        {
            _appDataSysRepo = appDataSysRepo;
            _inputFileSysRepo = inputFileSysRepo;
            _attackFileSysRepo = attackFileSysRepo;
            _languageFileSysRepo = languageFileSysRepo;
        }

        public void GenerateDataFiles(List<GameItemLocalised> localisedGameItems)
        {
            GenerateLootDataFiles(localisedGameItems);
            GenerateAttackDataFiles(localisedGameItems);
            GenerateCrateRequirementFiles(localisedGameItems);
            GenerateDevDetails(localisedGameItems);
        }

        public void GenerateLootDataFiles(List<GameItemLocalised> localisedGameItems)
        {
            LootDataFile lootData = _inputFileSysRepo.LoadJsonFile<LootDataFile>(DataFile.SurvivalLoot);

            List<string> tempAllNames = new List<string>();
            tempAllNames.AddRange(LootCalculator.GetListOfGameNames(lootData.LootTable.RandomLoot));
            tempAllNames.AddRange(LootCalculator.GetListOfGameNames(lootData.LootTable.RandomEpicLoot));
            //tempAllNames.AddRange(LootCalculator.GetListOfGameNames(lootData.LootTable.RandomLootWarehouse));

            tempAllNames.AddRange(LootCalculator.GetListOfGameNames(lootData.LootCrate.SelectOne));
            tempAllNames.AddRange(LootCalculator.GetListOfGameNames(lootData.LootCrateEpic.SelectOne));
            //tempAllNames.AddRange(LootCalculator.GetListOfGameNames(lootData.LootCrateEpicWareHouse.SelectOne));

            HashSet<string> allNamesHashSet = new HashSet<string>();
            foreach (string tempAllName in tempAllNames)
            {
                allNamesHashSet.Add(tempAllName);
            }

            List<string> allNames = allNamesHashSet.ToList();
            Dictionary<string, string> gameNameToAppIdLookup = new Dictionary<string, string>();

            foreach (string gameName in allNames)
            {
                if (gameNameToAppIdLookup.ContainsKey(gameName)) continue;
                bool found = false;
                foreach (GameItemLocalised gameItemLoc in localisedGameItems)
                {
                    if (!gameName.Equals(gameItemLoc.GameName)) continue;
                    if (gameNameToAppIdLookup.ContainsKey(gameName)) continue;

                    gameNameToAppIdLookup.Add(gameName, gameItemLoc.AppId);
                    found = true;
                    break;
                }

                if (!found) throw new Exception("Not found");
            }

            Dictionary<string, LootQuantitiesLookup> quantitiesLookup = new Dictionary<string, LootQuantitiesLookup>();
            foreach (LootQuantitiesLookup quantityLookup in lootData.LootQuantitiesLookup)
            {
                if (quantitiesLookup.ContainsKey(quantityLookup.Name)) continue;
                quantitiesLookup.Add(quantityLookup.Name, quantityLookup);
            }

            Dictionary<string, AppLoot> appLootDict = new Dictionary<string, AppLoot>();
            AddToAppLootDictionary(appLootDict, lootData.LootCrate.SelectOne, AppLootContainerType.CommonChest, gameNameToAppIdLookup, quantitiesLookup);
            AddToAppLootDictionary(appLootDict, lootData.LootTable.RandomLoot, AppLootContainerType.CommonChest, gameNameToAppIdLookup, quantitiesLookup);
            // TODO derive random loot from LootContainer

            AddToAppLootDictionary(appLootDict, lootData.LootCrateEpic.SelectOne, AppLootContainerType.RareChest, gameNameToAppIdLookup, quantitiesLookup);
            AddToAppLootDictionary(appLootDict, lootData.LootTable.RandomEpicLoot, AppLootContainerType.RareChest, gameNameToAppIdLookup, quantitiesLookup);

            List<AppLoot> appLoots = new List<AppLoot>();
            foreach (string dictKey in appLootDict.Keys)
            {
                AppLoot appLoot = appLootDict[dictKey];
                Dictionary<string, AppLootChance> uniqueChances = new Dictionary<string, AppLootChance>();
                foreach (AppLootChance chance in appLoot.Chances)
                {
                    string key = $"{chance.Chance}{chance.Max}{chance.Min}{chance.Type}";
                    if (uniqueChances.ContainsKey(key)) continue;

                    uniqueChances.Add(key, chance);
                }
                List<AppLootChance> newAppChances = new List<AppLootChance>();
                foreach (string uniqueChanceKey in uniqueChances.Keys)
                {
                    newAppChances.Add(uniqueChances[uniqueChanceKey]);
                }
                appLoots.Add(new AppLoot
                {
                    AppId = appLoot.AppId,
                    Chances = newAppChances,
                });
            }

            _appDataSysRepo.WriteBackToJsonFile(appLoots, AppDataFile.Loot);
        }

        public void GenerateAttackDataFiles(List<GameItemLocalised> localisedGameItems)
        {
            AttackDataFile attackData = _attackFileSysRepo.LoadJsonFile<AttackDataFile>(DataFile.Attack);

            List<AppAttackDataFile> appAttacks = new List<AppAttackDataFile>
            {
                new AppAttackDataFile
                {
                    GameId = EnemyGuid.Sledgehammer,
                    AppId = string.Empty,
                    AttackHitChances = new List<AppAttackTypeWithHitChances>
                    {
                        AppFileAttackMapper.MapToAppAttackHitChances(AttackType.Default, attackData.Sledgehammer),
                    }
                },
                new AppAttackDataFile
                {
                    GameId = EnemyGuid.Farmbot,
                    AppId = string.Empty,
                    AttackHitChances = new List<AppAttackTypeWithHitChances>
                    {
                        AppFileAttackMapper.MapToAppAttackHitChances(AttackType.Default, attackData.FarmbotBreach),
                        AppFileAttackMapper.MapToAppAttackHitChances(AttackType.Swipe, attackData.FarmbotSwipe),
                        AppFileAttackMapper.MapToAppAttackHitChances(AttackType.Step, attackData.FarmbotStep),
                    }
                },
                new AppAttackDataFile
                {
                    GameId = EnemyGuid.Haybot,
                    AppId = string.Empty,
                    AttackHitChances = new List<AppAttackTypeWithHitChances>
                    {
                        AppFileAttackMapper.MapToAppAttackHitChances(AttackType.Default, attackData.HaybotPitchfork),
                    }
                },
                new AppAttackDataFile
                {
                    GameId = EnemyGuid.ToteBot,
                    AppId = string.Empty,
                    AttackHitChances = new List<AppAttackTypeWithHitChances>
                    {
                        AppFileAttackMapper.MapToAppAttackHitChances(AttackType.Default, attackData.ToteBotAttack),
                        AppFileAttackMapper.MapToAppAttackHitChances(AttackType.Swipe, attackData.ToteBotWhip),
                    }
                }
            };

            foreach (AppAttackDataFile appAttack in appAttacks)
            {
                string gameId = appAttack.GameId;
                foreach (GameItemLocalised localisedGameItem in localisedGameItems)
                {
                    if (!localisedGameItem.ItemId.Equals(gameId)) continue;
                    appAttack.AppId = localisedGameItem.AppId;
                    break;
                }
            }

            _appDataSysRepo.WriteBackToJsonFile(appAttacks.OrderBy(aa => aa.AppId), AppDataFile.Attack);
        }

        public void GenerateCrateRequirementFiles(List<GameItemLocalised> localisedGameItems)
        {
            List<AppRecipe> recipes = new List<AppRecipe>();
            foreach (PackingStationItem packingStationItem in PackingStationLuaFile.AllItems)
            {
                GameItemLocalised outputAppItem = localisedGameItems.FirstOrDefault(g => g.ItemId.Equals(packingStationItem.CrateGuid));
                if (outputAppItem == null) continue;

                string ingredientGameId = PackingStationLuaFile.NameToGuidDictionary[packingStationItem.projectileName];
                GameItemLocalised ingredientAppItem = localisedGameItems.FirstOrDefault(g => g.ItemId.Equals(ingredientGameId));
                if (ingredientAppItem == null) continue;

                recipes.Add(new AppRecipe
                {
                    AppId = $"packing{recipes.Count + 1}",
                    Output = new AppIngredient
                    {
                        AppId = outputAppItem.AppId,
                        Quantity = 1
                    },
                    Inputs = new List<AppIngredient>
                    {
                        new AppIngredient
                        {
                            AppId = ingredientAppItem.AppId,
                            Quantity = packingStationItem.fullAmount
                        }
                    }
                });
            }

            _appDataSysRepo.WriteBackToJsonFile(recipes.OrderBy(aa => aa.AppId), AppDataFile.PackingStation);
        }

        public void GenerateDevDetails(List<GameItemLocalised> localisedGameItems)
        {
            List<AppDevDetailFile> devDetails = new List<AppDevDetailFile>();
            foreach (GameItemLocalised localisedGameItem in localisedGameItems)
            {
                List<AppDevDetailItem> detailList = new List<AppDevDetailItem>
                {
                    new AppDevDetailItem
                    {
                        Name = "GameName",
                        Value = localisedGameItem.GameName
                    },
                    new AppDevDetailItem
                    {
                        Name = "Uuid",
                        Value = localisedGameItem.ItemId
                    },
                };

                if (!string.IsNullOrEmpty(localisedGameItem.Color))
                {
                    detailList.Add(new AppDevDetailItem
                    {
                        Name = "Color",
                        Value = localisedGameItem.Color
                    });
                }

                if (!string.IsNullOrEmpty(localisedGameItem.Tiling))
                {
                    detailList.Add(new AppDevDetailItem
                    {
                        Name = "Tiling",
                        Value = localisedGameItem.Tiling
                    });
                }

                if (!string.IsNullOrEmpty(localisedGameItem.PhysicsMaterial))
                {
                    detailList.Add(new AppDevDetailItem
                    {
                        Name = "PhysicsMaterial",
                        Value = localisedGameItem.PhysicsMaterial
                    });
                }

                if (!string.IsNullOrEmpty(localisedGameItem.StackSize.ToString()))
                {
                    detailList.Add(new AppDevDetailItem
                    {
                        Name = "StackSize",
                        Value = localisedGameItem.StackSize.ToString()
                    });
                }

                detailList.Add(new AppDevDetailItem
                {
                    Name = "IsCreative",
                    Value = (localisedGameItem.IsCreative).ToString()
                });

                detailList.Add(new AppDevDetailItem
                {
                    Name = "IsChallenge",
                    Value = (localisedGameItem.DataSourceCategory == DataSourceCategory.Challenge).ToString()
                });

                if (localisedGameItem.Density > 0)
                {
                    detailList.Add(new AppDevDetailItem
                    {
                        Name = "Density",
                        Value = localisedGameItem.Density.ToString()
                    });
                }

                if (localisedGameItem.QualityLevel > 0)
                {
                    detailList.Add(new AppDevDetailItem
                    {
                        Name = "QualityLevel",
                        Value = localisedGameItem.QualityLevel.ToString()
                    });
                }

                devDetails.Add(new AppDevDetailFile
                {
                    AppId = localisedGameItem.AppId,
                    Details = detailList,
                });
            }

            _appDataSysRepo.WriteBackToJsonFile(devDetails.OrderBy(aa => aa.AppId), AppDataFile.DevDetail);
        }


        public async Task WritePatreonFile()
        {
            Console.WriteLine("\nGenerating Patron data");
            const string patronUrl = "https://api.assistantapps.com/Patreon";
            BaseExternalApiRepository apiRepo = new BaseExternalApiRepository();

            ResultWithValue<List<PatreonViewModel>> patreonResult = await apiRepo.Get<List<PatreonViewModel>>(patronUrl);
            if (patreonResult.HasFailed)
            {
                return;
            }

            Console.WriteLine($"Writing Patron Data to {AppFile.DataPatreon}");
            _appDataSysRepo.WriteBackToJsonFile(patreonResult.Value, AppFile.DataPatreon);
        }

        public async Task WriteSteamNewsFile()
        {
            Console.WriteLine("\nGenerating Steam News data");
            const string patronUrl = "https://api.scrapassistant.com/Steam/News";
            BaseExternalApiRepository apiRepo = new BaseExternalApiRepository();

            ResultWithValue<List<SteamNewsItemViewModel>> steamNewsResult = await apiRepo.Get<List<SteamNewsItemViewModel>>(patronUrl);
            if (steamNewsResult.HasFailed)
            {
                return;
            }

            Console.WriteLine($"Writing SteamNews Data to {AppFile.DataSteamNews}");
            _appDataSysRepo.WriteBackToJsonFile(steamNewsResult.Value, AppFile.DataSteamNews);
        }

        public async Task WriteContributorsFile()
        {
            Console.WriteLine("\nGenerating Contributors data");
            const string contributorUrl = "https://raw.githubusercontent.com/AssistantSMS/App/master/contributors.json";
            BaseExternalApiRepository apiRepo = new BaseExternalApiRepository();

            ResultWithValue<List<ContributorViewModel>> contributorsResult = await apiRepo.Get<List<ContributorViewModel>>(contributorUrl);
            if (contributorsResult.HasFailed)
            {
                return;
            }

            Console.WriteLine($"Writing Patron Data to {AppFile.DataContributors}");
            _appDataSysRepo.WriteBackToJsonFile(contributorsResult.Value, AppFile.DataContributors);
        }

        public async Task WriteDonatorsFile()
        {
            Console.WriteLine("\nGenerating Donators data");
            const string donationUrl = "https://api.assistantapps.com/Donation";
            BaseExternalApiRepository apiRepo = new BaseExternalApiRepository();

            ResultWithValue<ResultWithPagination<DonationViewModel>> donationResult = await apiRepo.Get<ResultWithPagination<DonationViewModel>>(donationUrl);
            if (donationResult.HasFailed)
            {
                return;
            }

            List<DonationViewModel> allDonations = new List<DonationViewModel>();
            allDonations.AddRange(donationResult.Value.Value);
            for (int donationPage = donationResult.Value.CurrentPage; donationPage < donationResult.Value.TotalPages; donationPage++)
            {
                string pagedDonationUrl = donationUrl + $"?page={(donationPage + 1)}";
                ResultWithValue<ResultWithPagination<DonationViewModel>> pagedDonationResult = await apiRepo.Get<ResultWithPagination<DonationViewModel>>(pagedDonationUrl);
                if (donationResult.HasFailed) continue;

                allDonations.AddRange(pagedDonationResult.Value.Value);
            }

            Console.WriteLine($"Writing Patron Data to {AppFile.DataDonation}");
            _appDataSysRepo.WriteBackToJsonFile(allDonations, AppFile.DataDonation);
        }

        public async Task WriteWhatIsNewFiles(LanguageType[] availableLangs)
        {
            Console.WriteLine("\nGenerating What Is New data");

            List<Task> tasks = availableLangs.Select(WriteWhatIsNewFile).ToList();
            await Task.WhenAll(tasks);

            Console.WriteLine("\n");
        }

        public async Task WriteWhatIsNewFile(LanguageType langType)
        {
            const string versionSearchUrl = "https://api.assistantapps.com/Version/Search";
            BaseExternalApiRepository apiRepo = new BaseExternalApiRepository();

            LanguageDetail language = LanguageHelper.GetLanguageDetail(langType);
            string path = $"{AppFile.DataWhatIsNewFolder}/{language.LanguageAppFolder}.json";

            VersionSearchViewModel searchVm = new VersionSearchViewModel
            {
                AppGuid = Guid.Parse("dfe0dbc7-8df4-47fb-a5a5-49af1937c4e2"),
                LanguageCode = language.LanguageAppFolder,
                Page = 1,
                Platforms = new List<PlatformType> { PlatformType.Android, PlatformType.iOS },
            };
            ResultWithValue<string> whatIsNewResult = await apiRepo.Post(versionSearchUrl, JsonConvert.SerializeObject(searchVm));
            if (whatIsNewResult.HasFailed) return;

            try
            {
                ResultWithPagination<VersionViewModel> versionItem = JsonConvert.DeserializeObject<ResultWithPagination<VersionViewModel>>(whatIsNewResult.Value);
                Console.WriteLine($"Writing WhatIsNew Data to {AppFile.DataWhatIsNewFolder} in {language.LanguageGameFolder}");
                _appDataSysRepo.WriteBackToJsonFile(versionItem.Value, path);
            }
            catch
            {
                Console.WriteLine($"FAILED writing WhatIsNew Data to {AppFile.DataWhatIsNewFolder} in {language.LanguageGameFolder}");
            }
        }

        public async Task WriteLanguageFiles()
        {
            const string translationExportUrl = "https://api.assistantapps.com/TranslationExport/{0}/{1}";
            const string appGuid = "dfe0dbc7-8df4-47fb-a5a5-49af1937c4e2";

            BaseExternalApiRepository apiRepo = new BaseExternalApiRepository();
            ResultWithValue<List<LanguageViewModel>> langResult = await apiRepo.Get<List<LanguageViewModel>>("https://api.assistantapps.com/Language");
            if (langResult.HasFailed)
            {
                Console.WriteLine("Could not get Server Languages");
                return;
            }

            foreach (string languageFile in LangFile.LanguagesInTheApp)
            {
                string langCode = languageFile.Replace("language.", string.Empty).Replace(".json", string.Empty);
                LanguageViewModel langViewModel = langResult.Value.FirstOrDefault(l => l.LanguageCode.Equals(langCode));
                if (langViewModel == null) continue;

                ResultWithValue<Dictionary<string, string>> languageContent = await apiRepo.Get<Dictionary<string, string>>(translationExportUrl.Replace("{0}", appGuid).Replace("{1}", langViewModel.Guid.ToString()));
                if (languageContent.HasFailed) continue;

                _languageFileSysRepo.WriteBackToJsonFile(languageContent.Value, languageFile);
            }
        }

        private static void AddToAppLootDictionary(Dictionary<string, AppLoot> appLoopDict, List<LootChance> selectOne, AppLootContainerType containerType, IReadOnlyDictionary<string, string> gameNameToAppIdLookup, Dictionary<string, LootQuantitiesLookup> quantitiesLookup)
        {
            int normalTotalChance = LootCalculator.TotalChanceValue(selectOne);
            foreach (LootChance normalSelectOneLootChance in selectOne)
            {
                if (!gameNameToAppIdLookup.ContainsKey(normalSelectOneLootChance.GameName)) continue;
                string appId = gameNameToAppIdLookup[normalSelectOneLootChance.GameName];

                double percentChance = (normalSelectOneLootChance.Chance / (normalTotalChance * 1.0)) * 100;
                LootQuantitiesLookup quantityKey = quantitiesLookup[normalSelectOneLootChance.QuantityKey];
                AppLootChance newChance = new AppLootChance
                {
                    Chance = (int)Math.Round(percentChance, 0),
                    Min = quantityKey.Min,
                    Max = quantityKey.Max,
                    Type = containerType,
                };
                if (appLoopDict.ContainsKey(appId))
                {
                    AppLoot current = appLoopDict[appId];
                    //if (current.Chances.Any(c => c.Type == containerType)) continue;
                    current.Chances.Add(newChance);
                    appLoopDict[appId] = current;
                }
                else
                {
                    appLoopDict.Add(appId, new AppLoot
                    {
                        AppId = appId,
                        Chances = new List<AppLootChance>
                            { newChance }
                    });
                }
            }
        }
    }
}
