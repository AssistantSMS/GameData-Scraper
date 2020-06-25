using System.Collections.Generic;
using System.IO;
using System.Linq;
using AssistantScrapMechanic.Domain;
using AssistantScrapMechanic.Domain.Constant;
using AssistantScrapMechanic.Domain.GameFiles;
using AssistantScrapMechanic.Domain.IntermediateFiles;
using AssistantScrapMechanic.Integration;
using AssistantScrapMechanic.Logic.Localiser;

namespace AssistantScrapMechanic.GameFilesReader.FileHandlers
{
    public class GameFilesReader
    {
        private readonly FileSystemRepository _outputFileSysRepo;
        private readonly FileSystemRepository _shapeSetsFileSysRepo;
        private readonly FileSystemRepository _legacyShapeSetsFileSysRepo;
        private readonly FileSystemRepository _survivalCraftingFileSysRepo;
        private readonly FileSystemRepository _characterFileSysRepo;
        private readonly FileSystemRepository _legacyLanguageFileSysRepo;
        private readonly FileSystemRepository _survivalLanguageFileSysRepo;

        public GameFilesReader(FileSystemRepository outputFileSysRepo,
            FileSystemRepository shapeSetsFileSysRepo, FileSystemRepository legacyShapeSetsFileSysRepo, 
            FileSystemRepository survivalCraftingFileSysRepo, FileSystemRepository characterFileSysRepo, 
            FileSystemRepository legacyLanguageFileSysRepo, FileSystemRepository survivalLanguageFileSysRepo)
        {
            _outputFileSysRepo = outputFileSysRepo;
            _legacyShapeSetsFileSysRepo = legacyShapeSetsFileSysRepo;
            _shapeSetsFileSysRepo = shapeSetsFileSysRepo;
            _survivalCraftingFileSysRepo = survivalCraftingFileSysRepo;
            _characterFileSysRepo = characterFileSysRepo;
            _legacyLanguageFileSysRepo = legacyLanguageFileSysRepo;
            _survivalLanguageFileSysRepo = survivalLanguageFileSysRepo;
        }

        private Dictionary<string, InventoryDescription> LoadItemNames()
        {
            const string language = "English";
            string survivalInvDescripFilePath = Path.Combine(language, "inventoryDescriptions.json");
            string dataInvDescripFilePath = Path.Combine(language, "InventoryItemDescriptions.json");
            string custDescripFilePath = Path.Combine(language, "CustomizationDescriptions.json");

            Dictionary<string, InventoryDescription> survivalDict = _survivalLanguageFileSysRepo.LoadJsonDictOfType<InventoryDescription>(survivalInvDescripFilePath);
            Dictionary<string, InventoryDescription> baseGameDict = _legacyLanguageFileSysRepo.LoadJsonDictOfType<InventoryDescription>(dataInvDescripFilePath);

            foreach ((string invKey, InventoryDescription invValue) in baseGameDict)
            {
                if (!survivalDict.ContainsKey(invKey))
                {
                    survivalDict.Add(invKey, invValue);
                }
            }

            Dictionary<string, InventoryDescription> customizationDict = _legacyLanguageFileSysRepo.LoadJsonDictOfType<InventoryDescription>(custDescripFilePath);

            foreach ((string invKey, InventoryDescription invValue) in customizationDict)
            {
                if (!survivalDict.ContainsKey(invKey))
                {
                    survivalDict.Add(invKey, invValue);
                }
            }

            return survivalDict;
        }

        public Dictionary<string, List<dynamic>> GetKeyValueOfAllItems(bool includeOtherItems = false)
        {
            Dictionary<string, List<dynamic>> result = new Dictionary<string, List<dynamic>>();

            Dictionary<string, List<RecipeLocalised>> recipes = GetKeyValueOfRecipItems();
            foreach ((string key, List<RecipeLocalised> value) in recipes)
            {
                if (result.ContainsKey(key))
                {
                    List<dynamic> current = result[key];
                    current.AddRange(value);
                    result[key] = current;
                }
                else
                {
                    result.Add(key, value.Select(v => v as dynamic).ToList());
                }
            }

            Dictionary<string, List<CustomisationItemLocalised>> cosmeticItems = GetKeyValueOfCosmeticItems();
            foreach ((string key, List<CustomisationItemLocalised> value) in cosmeticItems)
            {
                if (result.ContainsKey(key))
                {
                    List<dynamic> current = result[key];
                    current.AddRange(value);
                    result[key] = current;
                }
                else
                {
                    result.Add(key, value.Select(v => v as dynamic).ToList());
                }
            }

            Dictionary<string, List<dynamic>> gameItems = GetKeyValueOfGameItems(includeOtherItems);
            foreach ((string key, List<dynamic> value) in gameItems)
            {
                if (result.ContainsKey(key))
                {
                    List<dynamic> current = result[key];
                    current.AddRange(value);
                    result[key] = current;
                }
                else
                {
                    result.Add(key, value);
                }
            }

            return result;
        }

        public Dictionary<string, List<dynamic>> GetKeyValueOfGameItems(bool includeOtherItems = false)
        {
            Dictionary<string, List<dynamic>> result = new Dictionary<string, List<dynamic>>();

            List<GameItemLocalised> blocks = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Blocks);
            List<GameItemLocalised> buildings = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Building);
            List<GameItemLocalised> construction = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Construction);
            List<GameItemLocalised> consumable = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Consumable);
            List<GameItemLocalised> containers = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Containers);
            List<GameItemLocalised> craftBot = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Craftbot);
            List<GameItemLocalised> decor = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Decor);
            List<GameItemLocalised> fitting = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Fitting);
            List<GameItemLocalised> harvest = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Harvest);
            List<GameItemLocalised> industrial = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Industrial);
            List<GameItemLocalised> intera = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Interactive);
            List<GameItemLocalised> interu = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.InteractiveUpgradable);
            List<GameItemLocalised> interc = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.InteractiveContainer);
            List<GameItemLocalised> light = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Light);
            List<GameItemLocalised> manMade = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.ManMade);
            List<GameItemLocalised> outfit = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Outfit);
            List<GameItemLocalised> other = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Other);
            List<GameItemLocalised> packing = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.PackingCrate);
            List<GameItemLocalised> plant = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Plant);
            List<GameItemLocalised> power = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Power);
            List<GameItemLocalised> resource = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Resources);
            List<GameItemLocalised> robot = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Robot);
            List<GameItemLocalised> scrap = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Scrap);
            List<GameItemLocalised> spaceship = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Spaceship);
            List<GameItemLocalised> survival = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Survival);
            List<GameItemLocalised> tool = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Tool);
            List<GameItemLocalised> vehicle = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Vehicle);
            List<GameItemLocalised> warehouse = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Warehouse);

            List<GameItemLocalised> allItems = blocks
                .Concat(buildings)
                .Concat(construction)
                .Concat(consumable)
                .Concat(containers)
                .Concat(craftBot)
                .Concat(decor)
                .Concat(fitting)
                .Concat(harvest)
                .Concat(industrial)
                .Concat(intera)
                .Concat(interu)
                .Concat(interc)
                .Concat(light)
                .Concat(manMade)
                .Concat(outfit)
                .Concat(packing)
                .Concat(plant)
                .Concat(power)
                .Concat(resource)
                .Concat(robot)
                .Concat(scrap)
                .Concat(spaceship)
                .Concat(survival)
                .Concat(tool)
                .Concat(vehicle)
                .Concat(warehouse)
                .ToList();

            if (includeOtherItems)
            {
                allItems = allItems.Concat(other).ToList();
            }

            foreach (GameItemLocalised item in allItems)
            {
                if (result.ContainsKey(item.ItemId))
                {
                    List<dynamic> current = result[item.ItemId];
                    current.Add(item);
                    result[item.ItemId] = current;
                }
                else
                {
                    result.Add(item.ItemId, new List<dynamic> { item });
                }
            }

            return result;
        }

        public Dictionary<string, List<CustomisationItemLocalised>> GetKeyValueOfCosmeticItems()
        {
            Dictionary<string, List<CustomisationItemLocalised>> result = new Dictionary<string, List<CustomisationItemLocalised>>();

            List<CustomisationLocalised> customisations = _outputFileSysRepo.LoadListJsonFile<CustomisationLocalised>(OutputFile.Customization);
            foreach (CustomisationLocalised customisation in customisations)
            {
                foreach (CustomisationItemLocalised customisLoc in customisation.Items)
                {
                    if (result.ContainsKey(customisLoc.ItemId))
                    {
                        List<CustomisationItemLocalised> current = result[customisLoc.ItemId];
                        current.Add(customisLoc);
                        result[customisLoc.ItemId] = current;
                    }
                    else
                    {
                        result.Add(customisLoc.ItemId, new List<CustomisationItemLocalised> { customisLoc });
                    }
                }
            }

            return result;
        }

        public Dictionary<string, List<RecipeLocalised>> GetKeyValueOfRecipItems()
        {
            Dictionary<string, List<RecipeLocalised>> result = new Dictionary<string, List<RecipeLocalised>>();

            List<RecipeLocalised> cook = _outputFileSysRepo.LoadListJsonFile<RecipeLocalised>(OutputFile.CookBotRecipes);
            List<RecipeLocalised> craft = _outputFileSysRepo.LoadListJsonFile<RecipeLocalised>(OutputFile.CraftBotRecipes);
            List<RecipeLocalised> disp = _outputFileSysRepo.LoadListJsonFile<RecipeLocalised>(OutputFile.DispenserRecipes);
            List<RecipeLocalised> dress = _outputFileSysRepo.LoadListJsonFile<RecipeLocalised>(OutputFile.DressBotRecipes);
            List<RecipeLocalised> hideout = _outputFileSysRepo.LoadListJsonFile<RecipeLocalised>(OutputFile.HideOutRecipes);
            List<RecipeLocalised> refiner = _outputFileSysRepo.LoadListJsonFile<RecipeLocalised>(OutputFile.RefineryRecipes);
            List<RecipeLocalised> workb = _outputFileSysRepo.LoadListJsonFile<RecipeLocalised>(OutputFile.WorkbenchRecipes);

            List<RecipeLocalised> allRecipes = cook
                .Concat(craft)
                .Concat(disp)
                .Concat(dress)
                .Concat(hideout)
                .Concat(refiner)
                .Concat(workb)
                .ToList();

            foreach (RecipeLocalised recipe in allRecipes)
            {
                if (result.ContainsKey(recipe.ItemId))
                {
                    List<RecipeLocalised> current = result[recipe.ItemId];
                    current.Add(recipe);
                    result[recipe.ItemId] = current;
                }
                else
                {
                    result.Add(recipe.ItemId, new List<RecipeLocalised> { recipe });
                }
            }

            return result;
        }

        public void GenerateBlockIntermediate(Dictionary<string, InventoryDescription> itemNames)
        {
            Blocklist legacyBlocklist = _legacyShapeSetsFileSysRepo.LoadJsonFile<Blocklist>(GameFile.Blocks);
            Blocklist blocklist = _shapeSetsFileSysRepo.LoadJsonFile<Blocklist>(GameFile.Blocks);

            Dictionary<string, Blocks> distinctItems = new Dictionary<string, Blocks>();
            foreach (Blocks block in blocklist?.BlockList ?? new List<Blocks>())
            {
                if (!distinctItems.ContainsKey(block.Uuid)) distinctItems.Add(block.Uuid, block);
            }
            foreach (Blocks legacyBlock in legacyBlocklist?.BlockList ?? new List<Blocks>())
            {
                if (!distinctItems.ContainsKey(legacyBlock.Uuid)) distinctItems.Add(legacyBlock.Uuid, legacyBlock);
            }

            List<Blocks> all = distinctItems.Values.ToList();
            List<GameItemLocalised> fileData = all.Select((block, blockIndex) => block.Localise(Prefix.Block, blockIndex, itemNames)).ToList();

            _outputFileSysRepo.WriteBackToJsonFile(fileData, OutputFile.Blocks);
        }

        public void GenerateCustomizationIntermediate(Dictionary<string, InventoryDescription> itemNames)
        {
            Customization legacyCustomization = _characterFileSysRepo.LoadJsonFile<Customization>(GameFile.Customization);

            List<CustomisationLocalised> fileData = legacyCustomization.Categories.Select((customizItem, customizIndex) => customizItem.Localise(Prefix.Customisation, customizIndex, itemNames)).ToList();

            _outputFileSysRepo.WriteBackToJsonFile(fileData, OutputFile.Customization);
        }

        public void GenerateIntermediate()
        {
            Dictionary<string, InventoryDescription> itemNames = LoadItemNames();

            GenerateBlockIntermediate(itemNames);
            GenerateCustomizationIntermediate(itemNames);

            GenerateGameItemIntermediate(GameFile.Building, Prefix.Build, OutputFile.Building, itemNames);
            GenerateGameItemIntermediate(GameFile.Construction, Prefix.Construction, OutputFile.Construction, itemNames);
            GenerateGameItemIntermediate(GameFile.Consumable, Prefix.Consumable, OutputFile.Consumable, itemNames);
            GenerateGameItemIntermediate(GameFile.Containers, Prefix.Container, OutputFile.Containers, itemNames);
            GenerateGameItemIntermediate(GameFile.Craftbot, Prefix.Craftbot, OutputFile.Craftbot, itemNames);
            GenerateGameItemIntermediate(GameFile.Decor, Prefix.Decor, OutputFile.Decor, itemNames);
            GenerateGameItemIntermediate(GameFile.Fitting, Prefix.Fitting, OutputFile.Fitting, itemNames);
            GenerateGameItemIntermediate(GameFile.Harvest, Prefix.Harvest, OutputFile.Harvest, itemNames);
            GenerateGameItemIntermediate(GameFile.Industrial, Prefix.Industrial, OutputFile.Industrial, itemNames);
            GenerateGameItemIntermediate(GameFile.Interactive, Prefix.Interactive, OutputFile.Interactive, itemNames);
            GenerateGameItemIntermediate(GameFile.InteractiveUpgradable, Prefix.InteractiveUpgradable, OutputFile.InteractiveUpgradable, itemNames);
            GenerateGameItemIntermediate(GameFile.InteractiveContainer, Prefix.InteractiveContainer, OutputFile.InteractiveContainer, itemNames);
            GenerateGameItemIntermediate(GameFile.Light, Prefix.Light, OutputFile.Light, itemNames);
            GenerateGameItemIntermediate(GameFile.ManMade, Prefix.ManMade, OutputFile.ManMade, itemNames);
            GenerateGameItemIntermediate(GameFile.Outfit, Prefix.Outfit, OutputFile.Outfit, itemNames);
            GenerateGameItemIntermediate(GameFile.PackingCrate, Prefix.PackingCrate, OutputFile.PackingCrate, itemNames);
            GenerateGameItemIntermediate(GameFile.Plant, Prefix.Plant, OutputFile.Plant, itemNames);
            GenerateGameItemIntermediate(GameFile.Power, Prefix.Power, OutputFile.Power, itemNames);
            GenerateGameItemIntermediate(GameFile.Resources, Prefix.Resource, OutputFile.Resources, itemNames);
            GenerateGameItemIntermediate(GameFile.Robot, Prefix.Robot, OutputFile.Robot, itemNames);
            GenerateGameItemIntermediate(GameFile.Scrap, Prefix.Scrap, OutputFile.Scrap, itemNames);
            GenerateGameItemIntermediate(GameFile.Spaceship, Prefix.Spaceship, OutputFile.Spaceship, itemNames);
            GenerateGameItemIntermediate(GameFile.Survival, Prefix.Survival, OutputFile.Survival, itemNames);
            GenerateGameItemIntermediate(GameFile.Tool, Prefix.Tool, OutputFile.Tool, itemNames);
            GenerateGameItemIntermediate(GameFile.Vehicle, Prefix.Vehicle, OutputFile.Vehicle, itemNames);
            GenerateGameItemIntermediate(GameFile.Warehouse, Prefix.Warehouse, OutputFile.Warehouse, itemNames);

            Dictionary<string, List<dynamic>> lookup = GetKeyValueOfGameItems();

            List<NotFoundItem> notFound = new List<NotFoundItem>();
            notFound.AddRange(GenerateRecipeIntermediate(GameFile.CookBotRecipes, itemNames, Prefix.CookBot, OutputFile.CookBotRecipes, lookup, notFound.Count));
            notFound.AddRange(GenerateRecipeIntermediate(GameFile.CraftBotRecipes, itemNames, Prefix.CraftBot, OutputFile.CraftBotRecipes, lookup, notFound.Count));
            notFound.AddRange(GenerateRecipeIntermediate(GameFile.DispenserRecipes, itemNames, Prefix.Dispenser, OutputFile.DispenserRecipes, lookup, notFound.Count));
            notFound.AddRange(GenerateRecipeIntermediate(GameFile.DressBotRecipes, itemNames, Prefix.DressBot, OutputFile.DressBotRecipes, lookup, notFound.Count));
            notFound.AddRange(GenerateRecipeIntermediate(GameFile.HideOutRecipes, itemNames, Prefix.HideOut, OutputFile.HideOutRecipes, lookup, notFound.Count));
            GenerateRefinerIntermediate(GameFile.RefineryRecipes, itemNames, Prefix.Refinery, OutputFile.RefineryRecipes);
            notFound.AddRange(GenerateRecipeIntermediate(GameFile.WorkbenchRecipes, itemNames, Prefix.Workbench, OutputFile.WorkbenchRecipes, lookup, notFound.Count));

            GenerateUnknownItemIntermediate(notFound, OutputFile.Other, itemNames);
        }

        private List<NotFoundItem> GenerateRecipeIntermediate(string filename, Dictionary<string, InventoryDescription> itemNames, string prefix, string outputFilename, Dictionary<string, List<dynamic>> lookup, int currentOtherIndex)
        {
            List<Recipe> jsonContent = _survivalCraftingFileSysRepo.LoadListJsonFile<Recipe>(filename);

            int prefixCount = currentOtherIndex + 1;
            List<NotFoundItem> notFound = new List<NotFoundItem>();

            List<RecipeLocalised> fileData = new List<RecipeLocalised>();
            for (int recipeIndex = 0; recipeIndex < jsonContent.Count; recipeIndex++)
            {
                Recipe recipe = jsonContent[recipeIndex];
                RecipeLocalised localised = recipe.Localise(prefix, recipeIndex, itemNames);

                if (!lookup.ContainsKey(localised.ItemId))
                {
                    notFound.Add(new NotFoundItem
                    {
                        AppId = $"{Prefix.Other}{prefixCount}",
                        ItemId = recipe.ItemId,
                    });
                    prefixCount++;
                }
                fileData.Add(localised);
            }

            _outputFileSysRepo.WriteBackToJsonFile(fileData, outputFilename);
            return notFound;
        }

        private void GenerateRefinerIntermediate(string filename, Dictionary<string, InventoryDescription> itemNames, string prefix, string outputFilename)
        {
            Dictionary<string, RefinerRecipe> jsonContent = _survivalCraftingFileSysRepo.LoadJsonDictOfType<RefinerRecipe>(filename);

            List<RefinerRecipeLocalised> recipes = new List<RefinerRecipeLocalised>();
            for (int keyIndex = 0; keyIndex < jsonContent.Keys.Count; keyIndex++)
            {
                string jsonContentKey = jsonContent.Keys.ToList()[keyIndex];
                RefinerRecipe obj = jsonContent[jsonContentKey];
                recipes.Add(obj.Localise(jsonContentKey, prefix, keyIndex, itemNames));
            }

            _outputFileSysRepo.WriteBackToJsonFile(recipes, outputFilename);
        }

        public void GenerateGameItemIntermediate(string filename, string prefix, string outputFilename, Dictionary<string, InventoryDescription> itemNames)
        {
            GameItemlist legacyGameItemList = _legacyShapeSetsFileSysRepo.LoadJsonFile<GameItemlist>(filename);
            GameItemlist gameItemList = _shapeSetsFileSysRepo.LoadJsonFile<GameItemlist>(filename);

            Dictionary<string, GameItem> distinctItems = new Dictionary<string, GameItem>();
            foreach (GameItem gameItem in gameItemList?.GameItemList ?? new List<GameItem>())
            {
                if (!distinctItems.ContainsKey(gameItem.Uuid)) distinctItems.Add(gameItem.Uuid, gameItem);
            }
            foreach (GameItem legacyGameItem in legacyGameItemList?.GameItemList ?? new List<GameItem>())
            {
                if (!distinctItems.ContainsKey(legacyGameItem.Uuid)) distinctItems.Add(legacyGameItem.Uuid, legacyGameItem);
            }

            List<GameItem> all = distinctItems.Values.ToList();
            List<GameItemLocalised> fileData = new List<GameItemLocalised>();
            for (int gameItemIndex = 0; gameItemIndex < all.Count; gameItemIndex++)
            {
                GameItemLocalised locItem = all[gameItemIndex].Localise(prefix, gameItemIndex, itemNames);
                if (locItem.Name.Contains("obj_")) continue;
                fileData.Add(locItem);
            }

            if (fileData.Count < 1)
            {
                var k = 1 + 1;
            }

            _outputFileSysRepo.WriteBackToJsonFile(fileData, outputFilename);
        }

        public void GenerateUnknownItemIntermediate(List<NotFoundItem> notfound, string outputFilename, Dictionary<string, InventoryDescription> itemNames)
        {
            List<GameItemLocalised> fileData = new List<GameItemLocalised>();
            for (int notFoundIndex = 0; notFoundIndex < notfound.Count; notFoundIndex++)
            {
                GameItem gameItem = new GameItem
                {
                    Uuid = notfound[notFoundIndex].ItemId,
                };
                fileData.Add(gameItem.Localise(Prefix.Other, notFoundIndex, itemNames));
            }

            _outputFileSysRepo.WriteBackToJsonFile(fileData, outputFilename);
        }
    }
}
