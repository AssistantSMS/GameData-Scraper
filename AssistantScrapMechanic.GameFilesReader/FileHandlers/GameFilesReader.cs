using System.Collections.Generic;
using System.IO;
using System.Linq;
using AssistantScrapMechanic.Domain.Constant;
using AssistantScrapMechanic.Domain.GameFiles;
using AssistantScrapMechanic.Domain.IntermediateFiles;
using AssistantScrapMechanic.Integration;
using AssistantScrapMechanic.Logic.Localiser;

namespace AssistantScrapMechanic.GameFilesReader.FileHandlers
{
    public class GameFilesReader
    {
        private readonly FileSystemRepository _fileSysRepo;
        private readonly FileSystemRepository _outputFileSysRepo;
        private readonly FileSystemRepository _shapeSetsFileSysRepo;
        private readonly FileSystemRepository _legacyShapeSetsFileSysRepo;

        public GameFilesReader(FileSystemRepository fileSysRepo, FileSystemRepository outputFileSysRepo, FileSystemRepository legacyShapeSetsFileSysRepo, FileSystemRepository shapeSetsFileSysRepo)
        {
            _fileSysRepo = fileSysRepo;
            _outputFileSysRepo = outputFileSysRepo;
            _legacyShapeSetsFileSysRepo = legacyShapeSetsFileSysRepo;
            _shapeSetsFileSysRepo = shapeSetsFileSysRepo;
        }

        private Dictionary<string, InventoryDescription> LoadItemNames()
        {
            Dictionary<string, InventoryDescription> survivalDict = _fileSysRepo.LoadJsonDictOfType<InventoryDescription>(@"Survival\Gui\Language\English\inventoryDescriptions.json");
            Dictionary<string, InventoryDescription> baseGameDict = _fileSysRepo.LoadJsonDictOfType<InventoryDescription>(@"Data\Gui\Language\English\InventoryItemDescriptions.json");

            foreach (KeyValuePair<string, InventoryDescription> inventoryDescription in baseGameDict)
            {
                if (!survivalDict.ContainsKey(inventoryDescription.Key))
                {
                    survivalDict.Add(inventoryDescription.Key, inventoryDescription.Value);
                }
            }
            return survivalDict;
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

        public void GenerateIntermediate()
        {
            Dictionary<string, InventoryDescription> itemNames = LoadItemNames();

            GenerateBlockIntermediate(itemNames);
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
            
            GenerateRecipeIntermediate(GameFile.CookBotRecipes, itemNames, Prefix.CookBot, OutputFile.CookBotRecipes);
            GenerateRecipeIntermediate(GameFile.CraftBotRecipes, itemNames, Prefix.CraftBot, OutputFile.CraftBotRecipes);
            GenerateRecipeIntermediate(GameFile.DispenserRecipes, itemNames, Prefix.Dispenser, OutputFile.DispenserRecipes);
            GenerateRecipeIntermediate(GameFile.DressBotRecipes, itemNames, Prefix.DressBot, OutputFile.DressBotRecipes);
            GenerateRefinerIntermediate(GameFile.RefineryRecipes, itemNames, Prefix.Refinery, OutputFile.RefineryRecipes);
            GenerateRecipeIntermediate(GameFile.WorkbenchRecipes, itemNames, Prefix.Workbench, OutputFile.WorkbenchRecipes);
        }

        private void GenerateRecipeIntermediate(string filename, Dictionary<string, InventoryDescription> itemNames, string prefix, string outputFilename)
        {
            string filePath = Path.Combine("Survival", "CraftingRecipes", filename);
            List<Recipe> jsonContent = _fileSysRepo.LoadListJsonFile<Recipe>(filePath);

            List<RecipeLocalised> fileData = jsonContent.Select((recipe, recipeIndex) => recipe.Localise(prefix, recipeIndex, itemNames)).ToList();

            _outputFileSysRepo.WriteBackToJsonFile(fileData, outputFilename);
        }

        private void GenerateRefinerIntermediate(string filename, Dictionary<string, InventoryDescription> itemNames, string prefix, string outputFilename)
        {
            string filePath = Path.Combine("Survival", "CraftingRecipes", filename);
            Dictionary<string, RefinerRecipe> jsonContent = _fileSysRepo.LoadJsonDictOfType<RefinerRecipe>(filePath);

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
            List<GameItemLocalised> fileData = all.Select((gameItem, gameItemIndex) => gameItem.Localise(prefix, gameItemIndex, itemNames)).ToList();

            if (fileData.Count < 1)
            {
                var k = 1 + 1;
            }

            _outputFileSysRepo.WriteBackToJsonFile(fileData, outputFilename);
        }
    }
}
