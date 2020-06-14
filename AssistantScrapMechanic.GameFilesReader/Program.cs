using System;
using AssistantScrapMechanic.GameFilesReader.FileHandlers;
using AssistantScrapMechanic.Integration;

namespace AssistantScrapMechanic.GameFilesReader
{
    class Program
    {
        private static string _baseDirectory = @"E:\Steam\steamapps\common\Scrap Mechanic";
        private static string _recipesDirectory = $@"{_baseDirectory}\Survival\CraftingRecipes";
        private static string _shapeSetsDirectory = $@"{_baseDirectory}\Survival\Objects\Database\ShapeSets";
        private static string _outputDirectory = @"C:\Development\Projects\ScrapMechanic\AssistantScrapMechanic.Data\AssistantScrapMechanic.GameFilesReader\output";
        private static string _appFilesDirectory = @"C:\Development\Projects\ScrapMechanic\AssistantScrapMechanic.App\assets\json";

        private static int Main(string[] args)
        {
            FileSystemRepository fileSysRepo = new FileSystemRepository(_baseDirectory);
            FileSystemRepository shapeSetsFileSysRepo = new FileSystemRepository(_shapeSetsDirectory);
            FileSystemRepository outputFileSysRepo = new FileSystemRepository(_outputDirectory);
            FileSystemRepository appFilesSysRepo = new FileSystemRepository(_appFilesDirectory);

            while (true)
            {
                Console.WriteLine("Please select an option:");
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. Generate Intermediate Files");
                Console.WriteLine("2. Create App Files");

                string input = Console.ReadLine();
                if (!int.TryParse(input, out int numberInput)) return 0;

                switch (numberInput)
                {
                    case 1:
                        FileHandlers.GameFilesReader gameFilesReader = new FileHandlers.GameFilesReader(fileSysRepo, outputFileSysRepo, shapeSetsFileSysRepo);
                        gameFilesReader.GenerateBlockIntermediate();
                        gameFilesReader.GenerateIntermediate();
                        break;
                    case 2:
                        AppFilesHandler appFilesHandler = new AppFilesHandler(outputFileSysRepo, appFilesSysRepo);
                        appFilesHandler.GenerateAppFiles();
                        break;
                    default:
                        return 0;
                }
                Console.WriteLine("- - - - - - - - - - - -");
            }
        }
    }
}
