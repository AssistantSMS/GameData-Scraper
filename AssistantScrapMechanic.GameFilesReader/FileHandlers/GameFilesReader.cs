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

        public GameFilesReader(FileSystemRepository fileSysRepo, FileSystemRepository outputFileSysRepo, FileSystemRepository shapeSetsFileSysRepo)
        {
            _fileSysRepo = fileSysRepo;
            _outputFileSysRepo = outputFileSysRepo;
            _shapeSetsFileSysRepo = shapeSetsFileSysRepo;
        }

        private Dictionary<string, InventoryDescription> LoadItemNames()
        {
            Dictionary<string, InventoryDescription> survivalDict = _fileSysRepo.LoadJsonInventoryDescriptionDict(@"Survival\Gui\Language\English\inventoryDescriptions.json");
            Dictionary<string, InventoryDescription> baseGameDict = _fileSysRepo.LoadJsonInventoryDescriptionDict(@"Data\Gui\Language\English\InventoryItemDescriptions.json");

            foreach (KeyValuePair<string, InventoryDescription> inventoryDescription in baseGameDict)
            {
                if (!survivalDict.ContainsKey(inventoryDescription.Key))
                {
                    survivalDict.Add(inventoryDescription.Key, inventoryDescription.Value);
                }
            }
            return survivalDict;
        }

        public void GenerateBlockIntermediate()
        {
            Blocklist blocklist = _shapeSetsFileSysRepo.LoadJsonFile<Blocklist>(GameFile.Blocks);

            List<BlockLocalised> fileData = blocklist.BlockList.Select((block, blockIndex) => block.Localise(Prefix.Block, blockIndex)).ToList();

            _outputFileSysRepo.WriteBackToJsonFile(fileData, OutputFile.Blocks);
        }

        public void GenerateIntermediate()
        {
            Dictionary<string, InventoryDescription> itemNames = LoadItemNames();

            LoadItemRecipes(GameFile.CookBot, itemNames, Prefix.CookBot, OutputFile.CookBot);
            LoadItemRecipes(GameFile.CraftBot, itemNames, Prefix.CraftBot, OutputFile.CraftBot);
            LoadItemRecipes(GameFile.Dispenser, itemNames, Prefix.Dispenser, OutputFile.Dispenser);
            LoadItemRecipes(GameFile.DressBot, itemNames, Prefix.DressBot, OutputFile.DressBot);
            LoadItemRecipes(GameFile.Refinery, itemNames, Prefix.Refinery, OutputFile.Refinery);
            LoadItemRecipes(GameFile.Workbench, itemNames, Prefix.Workbench, OutputFile.Workbench);
        }

        private void LoadItemRecipes(string filename, Dictionary<string, InventoryDescription> itemNames, string prefix, string outputFilename)
        {
            string filePath = Path.Combine("Survival", "CraftingRecipes", filename);
            List<Recipe> jsonContent = _fileSysRepo.LoadListJsonFile<Recipe>(filePath);

            List<RecipeLocalised> fileData = jsonContent.Select((recipe, recipeIndex) => recipe.Localise(prefix, recipeIndex, itemNames)).ToList();

            _outputFileSysRepo.WriteBackToJsonFile(fileData, outputFilename);
        }
    }
}
