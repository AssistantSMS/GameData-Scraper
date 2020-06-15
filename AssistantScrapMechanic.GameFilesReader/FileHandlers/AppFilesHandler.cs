using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AssistantScrapMechanic.Domain.AppFiles;
using AssistantScrapMechanic.Domain.Constant;
using AssistantScrapMechanic.Domain.IntermediateFiles;
using AssistantScrapMechanic.Integration;
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

        public void GenerateAppFiles()
        {
            List<BlockLocalised> blocks = _outputFileSysRepo.LoadListJsonFile<BlockLocalised>(GameFile.Blocks);
            WriteAppFile(OutputFile.Blocks, blocks);

            Dictionary<string, List<dynamic>> lookup = GetKeyValueOfGameItems();
            
            WriteAppFile(OutputFile.CookBot, LoadIntermediateFile(GameFile.CookBot), lookup);
            WriteAppFile(OutputFile.CraftBot, LoadIntermediateFile(GameFile.CraftBot), lookup);
            WriteAppFile(OutputFile.Dispenser, LoadIntermediateFile(GameFile.Dispenser), lookup);
            WriteAppFile(OutputFile.DressBot, LoadIntermediateFile(GameFile.DressBot), lookup);
            WriteAppFile(OutputFile.Refinery, LoadIntermediateFile(GameFile.Refinery), lookup);
            WriteAppFile(OutputFile.Workbench, LoadIntermediateFile(GameFile.Workbench), lookup);
        }

        private List<RecipeLocalised> LoadIntermediateFile(string filename)
        {
            List<RecipeLocalised> englishIntermediate = _outputFileSysRepo.LoadListJsonFile<RecipeLocalised>(filename);
            // TODO other languages
            return englishIntermediate;
        }

        private void WriteAppFile(string outputFileName, IEnumerable<BlockLocalised> localisedData)
        {
            List<AppBlockLang> appBlock = new List<AppBlockLang>();
            List<AppBlockBase> appBlockBaseItems = new List<AppBlockBase>();
            foreach (BlockLocalised blockLocalised in localisedData)
            {
                AppBlock full = AppFileBlockMapper.ToAppFile(blockLocalised);
                appBlock.Add(full.ToLang());
                string image = GetItemImage(full.AppId);
                appBlockBaseItems.Add(full.ToBase(image));
            }

            _appFileSysRepo.WriteBackToJsonFile(appBlockBaseItems, outputFileName);
            _appFileSysRepo.WriteBackToJsonFile(appBlock, GetJsonLang("en", outputFileName));
        }

        private void WriteAppFile(string outputFileName, IEnumerable<RecipeLocalised> localisedData, Dictionary<string, List<dynamic>> lookup)
        {
            List<AppRecipeLang> appRecipe = new List<AppRecipeLang>();
            List<AppRecipeBase> appRecipeBaseItems = new List<AppRecipeBase>();
            foreach (RecipeLocalised recipeLocalised in localisedData)
            {
                AppRecipe full = AppFileReciperMapper.ToAppFile(recipeLocalised, lookup);
                appRecipe.Add(full.ToLang());
                string image = GetItemImage(full.AppId);
                appRecipeBaseItems.Add(full.ToBase(image));
            }

            _appFileSysRepo.WriteBackToJsonFile(appRecipeBaseItems, outputFileName);
            _appFileSysRepo.WriteBackToJsonFile(appRecipe, GetJsonLang("en", outputFileName));
        }

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

        public Dictionary<string, List<dynamic>> GetKeyValueOfGameItems()
        {
            Dictionary<string, List<dynamic>> result = new Dictionary<string, List<dynamic>>();

            List<RecipeLocalised> cook = LoadIntermediateFile(GameFile.CookBot);
            List<RecipeLocalised> craft = LoadIntermediateFile(GameFile.CraftBot);
            List<RecipeLocalised> disp = LoadIntermediateFile(GameFile.Dispenser);
            List<RecipeLocalised> dress = LoadIntermediateFile(GameFile.DressBot);
            List<RecipeLocalised> refiner = LoadIntermediateFile(GameFile.Refinery);
            List<RecipeLocalised> workb = LoadIntermediateFile(GameFile.Workbench);

            List<RecipeLocalised> all = cook
                .Concat(craft)
                .Concat(disp)
                .Concat(dress)
                .Concat(refiner)
                .Concat(workb)
                .ToList();

            foreach (RecipeLocalised recipe in all)
            {
                if (result.ContainsKey(recipe.ItemId))
                {
                    List<dynamic> current = result[recipe.ItemId];
                    current.Add(recipe);
                    result[recipe.ItemId] = current;
                }
                else
                {
                    result.Add(recipe.ItemId, new List<dynamic> { recipe });
                }
            }

            List<BlockLocalised> blocks = _outputFileSysRepo.LoadListJsonFile<BlockLocalised>(GameFile.Blocks);
            foreach (BlockLocalised block in blocks)
            {
                if (result.ContainsKey(block.ItemId))
                {
                    List<dynamic> current = result[block.ItemId];
                    current.Add(block);
                    result[block.ItemId] = current;
                }
                else
                {
                    result.Add(block.ItemId, new List<dynamic> { block });
                }
            }

            return result;
        }
    }
}
