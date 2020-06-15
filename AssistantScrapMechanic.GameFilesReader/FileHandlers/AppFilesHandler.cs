using System.Collections.Generic;
using System.Linq;
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
            WriteAppFile(AppFile.Blocks, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Blocks));
            WriteAppFile(AppFile.Building, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Building));
            WriteAppFile(AppFile.Construction, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Construction));
            WriteAppFile(AppFile.Consumable, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Consumable));
            WriteAppFile(AppFile.Containers, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Containers));
            WriteAppFile(AppFile.Craftbot, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Craftbot));
            WriteAppFile(AppFile.Decor, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Decor));
            WriteAppFile(AppFile.Fitting, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Fitting));
            WriteAppFile(AppFile.Harvest, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Harvest));
            WriteAppFile(AppFile.Industrial, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Industrial));
            WriteAppFile(AppFile.Interactive, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Interactive));
            WriteAppFile(AppFile.InteractiveUpgradable, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.InteractiveUpgradable));
            WriteAppFile(AppFile.InteractiveContainer, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.InteractiveContainer));
            WriteAppFile(AppFile.Light, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Light));
            WriteAppFile(AppFile.ManMade, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.ManMade));
            WriteAppFile(AppFile.Outfit, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Outfit));
            WriteAppFile(AppFile.PackingCrate, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.PackingCrate));
            WriteAppFile(AppFile.Plant, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Plant));
            WriteAppFile(AppFile.Power, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Power));
            WriteAppFile(AppFile.Resources, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Resources));
            WriteAppFile(AppFile.Robot, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Robot));
            WriteAppFile(AppFile.Survival, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Survival));
            WriteAppFile(AppFile.Tool, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Tool));
            WriteAppFile(AppFile.Vehicle, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Vehicle));
            WriteAppFile(AppFile.Warehouse, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Warehouse));

            Dictionary<string, List<dynamic>> lookup = GetKeyValueOfGameItems();
            
            WriteAppFile(AppFile.CookBotRecipes, LoadIntermediateFile(OutputFile.CookBotRecipes), lookup);
            WriteAppFile(AppFile.CraftBotRecipes, LoadIntermediateFile(OutputFile.CraftBotRecipes), lookup);
            WriteAppFile(AppFile.DispenserRecipes, LoadIntermediateFile(OutputFile.DispenserRecipes), lookup);
            WriteAppFile(AppFile.DressBotRecipes, LoadIntermediateFile(OutputFile.DressBotRecipes), lookup);
            WriteAppFile(AppFile.RefineryRecipes, LoadIntermediateFile(OutputFile.RefineryRecipes), lookup);
            WriteAppFile(AppFile.WorkbenchRecipes, LoadIntermediateFile(OutputFile.WorkbenchRecipes), lookup);
        }

        private List<RecipeLocalised> LoadIntermediateFile(string filename)
        {
            List<RecipeLocalised> englishIntermediate = _outputFileSysRepo.LoadListJsonFile<RecipeLocalised>(filename);
            // TODO other languages
            return englishIntermediate;
        }

        private void WriteAppFile(string outputFileName, IEnumerable<GameItemLocalised> localisedData)
        {
            List<AppGameItemLang> appBlock = new List<AppGameItemLang>();
            List<AppGameItemBase> appBlockBaseItems = new List<AppGameItemBase>();
            foreach (GameItemLocalised blockLocalised in localisedData)
            {
                AppGameItem full = AppFileBlockMapper.ToAppFile(blockLocalised);
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

            List<RecipeLocalised> cook = LoadIntermediateFile(OutputFile.CookBotRecipes);
            List<RecipeLocalised> craft = LoadIntermediateFile(OutputFile.CraftBotRecipes);
            List<RecipeLocalised> disp = LoadIntermediateFile(OutputFile.DispenserRecipes);
            List<RecipeLocalised> dress = LoadIntermediateFile(OutputFile.DressBotRecipes);
            List<RecipeLocalised> refiner = LoadIntermediateFile(OutputFile.RefineryRecipes);
            List<RecipeLocalised> workb = LoadIntermediateFile(OutputFile.WorkbenchRecipes);

            List<RecipeLocalised> allRecipes = cook
                .Concat(craft)
                .Concat(disp)
                .Concat(dress)
                .Concat(refiner)
                .Concat(workb)
                .ToList();

            foreach (RecipeLocalised recipe in allRecipes)
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
            List<GameItemLocalised> packing = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.PackingCrate);
            List<GameItemLocalised> plant = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Plant);
            List<GameItemLocalised> power = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Power);
            List<GameItemLocalised> resource = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Resources);
            List<GameItemLocalised> robot = _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Robot);
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
                .Concat(survival)
                .Concat(tool)
                .Concat(vehicle)
                .Concat(warehouse)
                .ToList();

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
    }
}
