using System.Collections.Generic;
using System.Linq;
using AssistantScrapMechanic.Domain;
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

        public void GenerateAppFiles(Dictionary<string, List<dynamic>> lookup)
        {
            WriteAppFile(AppFile.Customisation, _outputFileSysRepo.LoadListJsonFile<CustomisationLocalised>(OutputFile.Customization));

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
            WriteAppFile(AppFile.Scrap, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Scrap));
            WriteAppFile(AppFile.Spaceship, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Spaceship));
            WriteAppFile(AppFile.Survival, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Survival));
            WriteAppFile(AppFile.Tool, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Tool));
            WriteAppFile(AppFile.Vehicle, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Vehicle));
            WriteAppFile(AppFile.Warehouse, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Warehouse));

            WriteAppFile(AppFile.Other, _outputFileSysRepo.LoadListJsonFile<GameItemLocalised>(OutputFile.Other));

            WriteAppFile(AppFile.CookBotRecipes, _outputFileSysRepo.LoadListJsonFile<RecipeLocalised>(OutputFile.CookBotRecipes), lookup);
            WriteAppFile(AppFile.CraftBotRecipes, _outputFileSysRepo.LoadListJsonFile<RecipeLocalised>(OutputFile.CraftBotRecipes), lookup);
            WriteAppFile(AppFile.DispenserRecipes, _outputFileSysRepo.LoadListJsonFile<RecipeLocalised>(OutputFile.DispenserRecipes), lookup);
            WriteAppFile(AppFile.DressBotRecipes, _outputFileSysRepo.LoadListJsonFile<RecipeLocalised>(OutputFile.DressBotRecipes), lookup);
            WriteAppFile(AppFile.HideOutRecipes, _outputFileSysRepo.LoadListJsonFile<RecipeLocalised>(OutputFile.HideOutRecipes), lookup);
            WriteAppFile(AppFile.RefineryRecipes, _outputFileSysRepo.LoadListJsonFile<RecipeLocalised>(OutputFile.RefineryRecipes), lookup);
            WriteAppFile(AppFile.WorkbenchRecipes, _outputFileSysRepo.LoadListJsonFile<RecipeLocalised>(OutputFile.WorkbenchRecipes), lookup);
        }
        
        private void WriteAppFile(string outputFileName, IEnumerable<GameItemLocalised> localisedData)
        {
            List<AppGameItemLang> appBlock = new List<AppGameItemLang>();
            List<AppGameItemBase> appBlockBaseItems = new List<AppGameItemBase>();
            foreach (GameItemLocalised blockLocalised in localisedData)
            {
                if (GuidExclusion.All.Any(a => a.Equals(blockLocalised.ItemId))) continue;

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
                if (GuidExclusion.All.Any(a => a.Equals(recipeLocalised.ItemId))) continue;

                AppRecipe full = AppFileReciperMapper.ToAppFile(recipeLocalised, lookup);
                appRecipe.Add(full.ToLang());

                string image = GetItemImage(full.AppId);
                appRecipeBaseItems.Add(full.ToBase(image));
            }

            _appFileSysRepo.WriteBackToJsonFile(appRecipeBaseItems, outputFileName);
            _appFileSysRepo.WriteBackToJsonFile(appRecipe, GetJsonLang("en", outputFileName));
        }
        
        private void WriteAppFile(string outputFileName, IEnumerable<CustomisationLocalised> localisedData)
        {
            List<AppGameItemLang> appBlock = new List<AppGameItemLang>();
            List<AppGameItemBase> appBlockBaseItems = new List<AppGameItemBase>();

            foreach (CustomisationLocalised blockLocalised in localisedData)
            {
                foreach (CustomisationItemLocalised blockLoc in blockLocalised.Items)
                {
                    if (GuidExclusion.All.Any(a => a.Equals(blockLoc.ItemId.Replace("_male", string.Empty).Replace("_female", string.Empty)))) continue;

                    AppGameItem full = AppFileBlockMapper.ToAppFile(blockLoc, blockLocalised.Category);
                    if (!_appImagesRepo.FileExists("items", full.AppId)) continue;

                    appBlock.Add(full.ToLang());
                    string image = GetItemImage(full.AppId);
                    appBlockBaseItems.Add(full.ToBase(image));
                }
            }

            _appFileSysRepo.WriteBackToJsonFile(appBlockBaseItems, outputFileName);
            _appFileSysRepo.WriteBackToJsonFile(appBlock, GetJsonLang("en", outputFileName));
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
