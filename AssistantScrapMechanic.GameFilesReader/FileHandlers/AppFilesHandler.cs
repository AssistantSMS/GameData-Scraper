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

        public AppFilesHandler(FileSystemRepository outputFileSysRepo, FileSystemRepository appFileSysRepo)
        {
            _outputFileSysRepo = outputFileSysRepo;
            _appFileSysRepo = appFileSysRepo;
        }

        public void GenerateAppFiles()
        {
            List<BlockLocalised> blocks = _outputFileSysRepo.LoadListJsonFile<BlockLocalised>(GameFile.Blocks);

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

            WriteAppFile(OutputFile.Blocks, blocks);

            WriteAppFile(OutputFile.CookBot, cook, all, blocks);
            WriteAppFile(OutputFile.CraftBot, craft, all, blocks);
            WriteAppFile(OutputFile.Dispenser, disp, all, blocks);
            WriteAppFile(OutputFile.DressBot, dress, all, blocks);
            WriteAppFile(OutputFile.Refinery, refiner, all, blocks);
            WriteAppFile(OutputFile.Workbench, workb, all, blocks);
        }

        private List<RecipeLocalised> LoadIntermediateFile(string filename)
        {
            List<RecipeLocalised> englishIntermediate = _outputFileSysRepo.LoadListJsonFile<RecipeLocalised>(filename);
            // TODO other languages
            return englishIntermediate;
        }

        private void WriteAppFile(string outputFileName, IEnumerable<BlockLocalised> localisedData)
        {
            List<AppBlock> appBlock = new List<AppBlock>();
            foreach (BlockLocalised blockLocalised in localisedData)
            {
                appBlock.Add(AppFileBlockMapper.ToAppFile(blockLocalised));
            }

            _appFileSysRepo.WriteBackToJsonFile(appBlock, outputFileName);
        }

        private void WriteAppFile(string outputFileName, IEnumerable<RecipeLocalised> localisedData, List<RecipeLocalised> all, List<BlockLocalised> blocks)
        {
            List<AppRecipe> appRecipe = new List<AppRecipe>();
            foreach (RecipeLocalised recipeLocalised in localisedData)
            {
                appRecipe.Add(AppFileReciperMapper.ToAppFile(recipeLocalised, all, blocks));
            }

            _appFileSysRepo.WriteBackToJsonFile(appRecipe, outputFileName);
        }
    }
}
