using System;
using System.Collections.Generic;
using System.Linq;
using AssistantScrapMechanic.Domain.AppFiles;
using AssistantScrapMechanic.Domain.Constant;
using AssistantScrapMechanic.Domain.Entity;
using AssistantScrapMechanic.Domain.Enum;
using AssistantScrapMechanic.Domain.IntermediateFiles;
using AssistantScrapMechanic.Integration;
using AssistantScrapMechanic.Logic.Localiser;
using AssistantScrapMechanic.Logic.Mapper.AppMapper;

namespace AssistantScrapMechanic.GameFilesReader.FileHandlers
{
    public class AppFilesHandler
    {
        private readonly FileSystemRepository _outputFileSysRepo;
        private readonly FileSystemRepository _appFileSysRepo;
        private readonly FileSystemRepository _appImagesRepo;

        public AppFilesHandler(FileSystemRepository outputFileSysRepo, FileSystemRepository appFileSysRepo, FileSystemRepository appImagesRepo)
        {
            _outputFileSysRepo = outputFileSysRepo;
            _appFileSysRepo = appFileSysRepo;
            _appImagesRepo = appImagesRepo;
        }

        public void GenerateAppFiles(LanguageDetail language, Dictionary<string, InventoryDescription> itemNames, Dictionary<string, List<ILocalised>> lookup)
        {
            WriteAppFile(AppFile.Customisation, _outputFileSysRepo.LoadListJsonFile<CustomisationLocalised>(OutputFile.Customization), language, itemNames);

            WriteAppFile(AppFile.Ammo, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Ammo), lookup, language, itemNames);
            WriteAppFile(AppFile.Blocks, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Blocks), lookup, language, itemNames);
            WriteAppFile(AppFile.Bucket, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Bucket), lookup, language, itemNames);
            WriteAppFile(AppFile.Building, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Building), lookup, language, itemNames);
            WriteAppFile(AppFile.CharObject, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.CharObject), lookup, language, itemNames);
            WriteAppFile(AppFile.Component, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Component), lookup, language, itemNames);
            WriteAppFile(AppFile.Construction, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Construction), lookup, language, itemNames);
            WriteAppFile(AppFile.Consumable, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Consumable), lookup, language, itemNames);
            WriteAppFile(AppFile.ConsumableShared, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.ConsumableShared), lookup, language, itemNames);
            WriteAppFile(AppFile.Containers, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Containers), lookup, language, itemNames);
            WriteAppFile(AppFile.Craftbot, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Craftbot), lookup, language, itemNames);
            WriteAppFile(AppFile.Decor, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Decor), lookup, language, itemNames);
            WriteAppFile(AppFile.Fitting, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Fitting), lookup, language, itemNames);
            WriteAppFile(AppFile.Fuel, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Fuel), lookup, language, itemNames);
            WriteAppFile(AppFile.Harvest, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Harvest), lookup, language, itemNames);
            WriteAppFile(AppFile.Industrial, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Industrial), lookup, language, itemNames);
            WriteAppFile(AppFile.Interactive, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Interactive), lookup, language, itemNames);
            WriteAppFile(AppFile.InteractiveShared, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.InteractiveShared), lookup, language, itemNames);
            WriteAppFile(AppFile.InteractiveUpgradable, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.InteractiveUpgradable), lookup, language, itemNames);
            WriteAppFile(AppFile.InteractiveContainer, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.InteractiveContainer), lookup, language, itemNames);
            WriteAppFile(AppFile.Light, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Light), lookup, language, itemNames);
            WriteAppFile(AppFile.ManMade, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.ManMade), lookup, language, itemNames);
            WriteAppFile(AppFile.Outfit, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Outfit), lookup, language, itemNames);
            WriteAppFile(AppFile.PackingCrate, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.PackingCrate), lookup, language, itemNames);
            WriteAppFile(AppFile.Plant, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Plant), lookup, language, itemNames);
            WriteAppFile(AppFile.Power, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Power), lookup, language, itemNames);
            WriteAppFile(AppFile.Resources, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Resources), lookup, language, itemNames);
            WriteAppFile(AppFile.Robot, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Robot), lookup, language, itemNames);
            WriteAppFile(AppFile.Scrap, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Scrap), lookup, language, itemNames);
            WriteAppFile(AppFile.Spaceship, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Spaceship), lookup, language, itemNames);
            WriteAppFile(AppFile.Survival, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Survival), lookup, language, itemNames);
            WriteAppFile(AppFile.Tool, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Tool), lookup, language, itemNames);
            WriteAppFile(AppFile.Vehicle, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Vehicle), lookup, language, itemNames);
            WriteAppFile(AppFile.Warehouse, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Warehouse), lookup, language, itemNames);

            WriteAppFile(AppFile.Other, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Other), lookup, language, itemNames);

            WriteAppFile(AppFile.CookBotRecipes, _outputFileSysRepo.LoadListJsonFile<RecipeLocalised>(OutputFile.CookBotRecipes), lookup, language, itemNames);
            WriteAppFile(AppFile.CraftBotRecipes, _outputFileSysRepo.LoadListJsonFile<RecipeLocalised>(OutputFile.CraftBotRecipes), lookup, language, itemNames);
            WriteAppFile(AppFile.DispenserRecipes, _outputFileSysRepo.LoadListJsonFile<RecipeLocalised>(OutputFile.DispenserRecipes), lookup, language, itemNames);
            WriteAppFile(AppFile.DressBotRecipes, _outputFileSysRepo.LoadListJsonFile<RecipeLocalised>(OutputFile.DressBotRecipes), lookup, language, itemNames);
            WriteAppFile(AppFile.HideOutRecipes, _outputFileSysRepo.LoadListJsonFile<RecipeLocalised>(OutputFile.HideOutRecipes), lookup, language, itemNames);
            WriteAppFile(AppFile.RefineryRecipes, _outputFileSysRepo.LoadListJsonFile<RecipeLocalised>(OutputFile.RefineryRecipes), lookup, language, itemNames);
            WriteAppFile(AppFile.WorkbenchRecipes, _outputFileSysRepo.LoadListJsonFile<RecipeLocalised>(OutputFile.WorkbenchRecipes), lookup, language, itemNames);
        }
        
        private void WriteAppFile(string outputFileName, IEnumerable<GameItemLocalised> localisedData, Dictionary<string, List<ILocalised>> lookup, LanguageDetail language, Dictionary<string, InventoryDescription> itemNames)
        {
            List<AppGameItemLang> appBlock = new List<AppGameItemLang>();
            List<AppGameItemBase> appBlockBaseItems = new List<AppGameItemBase>();
            foreach (GameItemLocalised blockLocalised in localisedData)
            {
                if (GuidExclusion.All.Any(a => a.Equals(blockLocalised.ItemId))) continue;

                GameItemLocalised relocalised = (language.LanguageType == LanguageType.English)
                    ? blockLocalised
                    : blockLocalised.ReLocalise(itemNames);

                GameItemFeatures defaultFeatures = new GameItemFeatures { Uuid = blockLocalised.ItemId, Features = null };
                GameItemFeatures gameItemFeatures = FeaturesList.Features.FirstOrDefault(u => u.Uuid.Equals(blockLocalised.ItemId, StringComparison.InvariantCultureIgnoreCase)) ?? defaultFeatures;
                Upgrade gameItemUpgrades = UpgradeList.Upgrades.FirstOrDefault(u => u.Uuid.Equals(blockLocalised.ItemId, StringComparison.InvariantCultureIgnoreCase));

                if (gameItemUpgrades != null && lookup.ContainsKey(gameItemUpgrades.TargetUuid))
                {
                    List<ILocalised> list = lookup[gameItemUpgrades.TargetUuid];
                    if (list != null && list.Count > 0)
                    {
                        if (list[0] is GameItemLocalised)
                        {
                            gameItemUpgrades.TargetId = list[0].AppId;
                        }
                        else
                        {
                            gameItemUpgrades = null;
                        }
                    }
                }

                AppGameItem full = AppFileBlockMapper.ToAppFile(relocalised, gameItemFeatures.Features, gameItemUpgrades);
                appBlock.Add(full.ToLang());
                string image = GetItemImage(full.AppId);
                appBlockBaseItems.Add(full.ToBase(image));
            }

            _appFileSysRepo.WriteBackToJsonFile(appBlockBaseItems, outputFileName);
            _appFileSysRepo.WriteBackToJsonFile(appBlock, GetJsonLang(language.LanguageAppFolder, outputFileName));
        }

        private void WriteAppFile(string outputFileName, IEnumerable<RecipeLocalised> localisedData, Dictionary<string, List<ILocalised>> lookup, LanguageDetail language, Dictionary<string, InventoryDescription> itemNames)
        {
            List<AppRecipeLang> appRecipe = new List<AppRecipeLang>();
            List<AppRecipeBase> appRecipeBaseItems = new List<AppRecipeBase>();
            foreach (RecipeLocalised recipeLocalised in localisedData)
            {
                if (GuidExclusion.All.Any(a => a.Equals(recipeLocalised.ItemId))) continue;

                RecipeLocalised relocalised = (language.LanguageType == LanguageType.English)
                    ? recipeLocalised
                    : recipeLocalised.ReLocalise(itemNames);

                AppRecipe full = AppFileReciperMapper.ToAppFile(relocalised, lookup);
                appRecipe.Add(full.ToLang());

                string image = GetItemImage(full.AppId);
                appRecipeBaseItems.Add(full.ToBase(image));
            }

            _appFileSysRepo.WriteBackToJsonFile(appRecipeBaseItems, outputFileName);
            _appFileSysRepo.WriteBackToJsonFile(appRecipe, GetJsonLang(language.LanguageAppFolder, outputFileName));
        }
        
        private void WriteAppFile(string outputFileName, IEnumerable<CustomisationLocalised> localisedData, LanguageDetail language, Dictionary<string, InventoryDescription> itemNames)
        {
            List<AppGameItemLang> appBlock = new List<AppGameItemLang>();
            List<AppGameItemBase> appBlockBaseItems = new List<AppGameItemBase>();

            foreach (CustomisationLocalised blockLocalised in localisedData)
            {
                foreach (CustomisationItemLocalised blockLoc in blockLocalised.Items)
                {
                    if (GuidExclusion.All.Any(a => a.Equals(blockLoc.ItemId.Replace("_male", string.Empty).Replace("_female", string.Empty)))) continue;

                    CustomisationItemLocalised relocalised = (language.LanguageType == LanguageType.English)
                        ? blockLoc
                        : blockLoc.ReLocaliseItem(itemNames);

                    AppGameItem full = AppFileBlockMapper.ToAppFile(relocalised, blockLocalised.Category);
                    if (!_appImagesRepo.FileExists("items", full.AppId)) continue;

                    appBlock.Add(full.ToLang());
                    string image = GetItemImage(full.AppId);
                    appBlockBaseItems.Add(full.ToBase(image));
                }
            }

            _appFileSysRepo.WriteBackToJsonFile(appBlockBaseItems, outputFileName);
            _appFileSysRepo.WriteBackToJsonFile(appBlock, GetJsonLang(language.LanguageAppFolder, outputFileName));
        }
        
        //private void WriteAppFile(string outputFileName, IEnumerable<NotFoundItem> otherUnlocalised)
        //{

        //}

        private static string GetJsonLang(string folder, string fileName)
        {
            return $@"{folder}\{fileName.Replace(".json", ".lang.json")}";
        }

        private string GetItemImage(string appId)
        {
            bool exists = _appImagesRepo.FileExists("items", appId);
            if (!exists) return "unknown.png";

            return $"items/{appId}.png";
        }
    }
}
