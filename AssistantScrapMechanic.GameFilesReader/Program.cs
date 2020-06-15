﻿using System;
using System.Collections.Generic;
using AssistantScrapMechanic.GameFilesReader.FileHandlers;
using AssistantScrapMechanic.Integration;

namespace AssistantScrapMechanic.GameFilesReader
{
    class Program
    {
        private static string _baseDirectory = @"E:\Steam\steamapps\common\Scrap Mechanic";
        private static string _recipesDirectory = $@"{_baseDirectory}\Survival\CraftingRecipes";
        private static string _dataGuiDirectory = $@"{_baseDirectory}\Data\Gui";
        private static string _survivalGuiDirectory = $@"{_baseDirectory}\Survival\Gui";
        private static string _shapeSetsDirectory = $@"{_baseDirectory}\Survival\Objects\Database\ShapeSets";
        private static string _outputDirectory = @"C:\Development\Projects\ScrapMechanic\AssistantScrapMechanic.Data\AssistantScrapMechanic.GameFilesReader\output";
        private static string _appFilesDirectory = @"C:\Development\Projects\ScrapMechanic\AssistantScrapMechanic.App\assets\json";
        private static string _appImagesDirectory = @"C:\Development\Projects\ScrapMechanic\AssistantScrapMechanic.App\assets\img";

        private static int Main(string[] args)
        {
            FileSystemRepository fileSysRepo = new FileSystemRepository(_baseDirectory);
            FileSystemRepository shapeSetsFileSysRepo = new FileSystemRepository(_shapeSetsDirectory);
            FileSystemRepository outputFileSysRepo = new FileSystemRepository(_outputDirectory);
            FileSystemRepository appFilesSysRepo = new FileSystemRepository(_appFilesDirectory);
            FileSystemRepository appImagesRepo = new FileSystemRepository(_appImagesDirectory);

            while (true)
            {
                Console.WriteLine("Please select an option:");
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. Generate Intermediate Files");
                Console.WriteLine("2. Create App Files");
                Console.WriteLine("3. Cut images from sprite map");

                string input = Console.ReadLine();
                if (!int.TryParse(input, out int numberInput)) return 0;

                switch (numberInput)
                {
                    case 1:
                        FileHandlers.GameFilesReader gameFilesReader = new FileHandlers.GameFilesReader(fileSysRepo, outputFileSysRepo, shapeSetsFileSysRepo);
                        gameFilesReader.GenerateIntermediate();
                        break;
                    case 2:
                        AppFilesHandler appFilesHandler = new AppFilesHandler(outputFileSysRepo, appFilesSysRepo, appImagesRepo);
                        appFilesHandler.GenerateAppFiles();
                        break;
                    case 3:
                        AppFilesHandler appFilesHandlerForImages = new AppFilesHandler(outputFileSysRepo, appFilesSysRepo, appImagesRepo);
                        Dictionary<string, List<dynamic>> keyValueOfGameItems = appFilesHandlerForImages.GetKeyValueOfGameItems();

                        ImageCutter imageCutter = new ImageCutter(_dataGuiDirectory, _survivalGuiDirectory, _outputDirectory);
                        imageCutter.CutOutImages(keyValueOfGameItems);
                        break;
                    default:
                        return 0;
                }
                Console.WriteLine("- - - - - - - - - - - -");
            }
        }
    }
}
