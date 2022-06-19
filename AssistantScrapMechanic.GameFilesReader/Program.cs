﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssistantScrapMechanic.Domain.Dto.ViewModel;
using AssistantScrapMechanic.Domain.Entity;
using AssistantScrapMechanic.Domain.Enum;
using AssistantScrapMechanic.Domain.IntermediateFiles;
using AssistantScrapMechanic.Domain.Result;
using AssistantScrapMechanic.GameFilesReader.FileHandlers;
using AssistantScrapMechanic.Integration;
using AssistantScrapMechanic.Integration.Repository;
using AssistantScrapMechanic.Logic;

namespace AssistantScrapMechanic.GameFilesReader
{
    class Program
    {
        private const string BaseDirectory = @"C:\Steam\steamapps\common\Scrap Mechanic";
        private static readonly string SurvivalGuiDirectory = $@"{BaseDirectory}\Survival\Gui";
        private static readonly string SurvivalLanguageDirectory = $@"{BaseDirectory}\Survival\Gui\Language";
        private static readonly string SurvivalCraftingDirectory = $@"{BaseDirectory}\Survival\CraftingRecipes";
        private static readonly string ShapeSetsDirectory = $@"{BaseDirectory}\Survival\Objects\Database\ShapeSets";

        private static readonly string DataGuiDirectory = $@"{BaseDirectory}\Data\Gui";
        private static readonly string AttackDataDirectory = $@"{BaseDirectory}\Data\Melee";
        private static readonly string CharacterDirectory = $@"{BaseDirectory}\Data\Character";
        private static readonly string LegacyLanguageDirectory = $@"{BaseDirectory}\Data\Gui\Language";
        private static readonly string LegacyShapeSetsDirectory = $@"{BaseDirectory}\Data\Objects\Database\ShapeSets";

        private static readonly string ChallengeGuiDirectory = $@"{BaseDirectory}\ChallengeData\Gui";
        private static readonly string ChallengeLanguageDirectory = $@"{BaseDirectory}\ChallengeData\Gui\Language";
        private static readonly string ChallengeShapeSetsDirectory = $@"{BaseDirectory}\ChallengeData\Objects\Database\ShapeSets";

        private const string AppFilesDirectory = @"C:\Development\Projects\AssistantSMS\ScrapMechanic.App\assets";
        private const string ConsoleAppDirectory = @"C:\Development\Projects\AssistantSMS\ScrapMechanic.Data\AssistantScrapMechanic.GameFilesReader";
        private static readonly string OutputDirectory = $@"{ConsoleAppDirectory}\output";
        private static readonly string InputDirectory = $@"{ConsoleAppDirectory}\input";
        private static readonly string AppJsonFilesDirectory = $@"{AppFilesDirectory}\json";
        private static readonly string AppImagesDirectory = $@"{AppFilesDirectory}\img";
        private static readonly string AppDataDirectory = $@"{AppFilesDirectory}\data";
        private static readonly string AppLangDirectory = $@"{AppFilesDirectory}\lang";
        
        private static readonly LanguageType[] AvailableLangs = (LanguageType[])Enum.GetValues(typeof(LanguageType));

        private static async Task<int> Main(string[] args)
        {
            FileSystemRepository shapeSetsFileSysRepo = new FileSystemRepository(ShapeSetsDirectory);
            FileSystemRepository legacyShapeSetsFileSysRepo = new FileSystemRepository(LegacyShapeSetsDirectory);
            FileSystemRepository challengeShapeSetsFileSysRepo = new FileSystemRepository(ChallengeShapeSetsDirectory);
            FileSystemRepository survivalCraftingFileSysRepo = new FileSystemRepository(SurvivalCraftingDirectory);
            FileSystemRepository characterFileSysRepo = new FileSystemRepository(CharacterDirectory);
            FileSystemRepository legacyLanguageFileSysRepo = new FileSystemRepository(LegacyLanguageDirectory);
            FileSystemRepository survivalLanguageFileSysRepo = new FileSystemRepository(SurvivalLanguageDirectory);
            FileSystemRepository attackFileSysRepo = new FileSystemRepository(AttackDataDirectory);
            FileSystemRepository challengeLanguageFileSysRepo = new FileSystemRepository(ChallengeLanguageDirectory);

            FileSystemRepository outputFileSysRepo = new FileSystemRepository(OutputDirectory);
            FileSystemRepository inputFileSysRepo = new FileSystemRepository(InputDirectory);
            FileSystemRepository appFilesSysRepo = new FileSystemRepository(AppJsonFilesDirectory);
            FileSystemRepository appImagesRepo = new FileSystemRepository(AppImagesDirectory);
            FileSystemRepository appDataSysRepo = new FileSystemRepository(AppDataDirectory);
            FileSystemRepository appLangSysRepo = new FileSystemRepository(AppLangDirectory);

            LanguageDetail language = new LanguageDetail(LanguageType.English, "English", "en");

            Console.WriteLine("Hit Enter");
            Console.ReadLine();
            int langCount = 0;
            Console.WriteLine("Please select an option");
            foreach (LanguageType langType in AvailableLangs)
            {
                if (langType == LanguageType.NotSpecified) continue;
                langCount++;
                Console.WriteLine($"{langCount}. Localise Files to {langType}");
            }
            Console.WriteLine($"{AvailableLangs.Length}. Generate All Files for All Languages");

            string langInput = Console.ReadLine();
            if (!int.TryParse(langInput, out int langNumberInput)) return 0;
            if (langNumberInput < 0 || langNumberInput > AvailableLangs.Length) return 0;

            if (langNumberInput != 0 && langNumberInput < AvailableLangs.Length)
            {
                LanguageType selectedLangType = AvailableLangs[langNumberInput];
                language = LanguageHelper.GetLanguageDetail(selectedLangType);
            }

            FileHandlers.GameFilesReader gameFilesReader = new FileHandlers.GameFilesReader(outputFileSysRepo,
                shapeSetsFileSysRepo, legacyShapeSetsFileSysRepo, challengeShapeSetsFileSysRepo,
                survivalCraftingFileSysRepo, characterFileSysRepo,
                legacyLanguageFileSysRepo, survivalLanguageFileSysRepo, challengeLanguageFileSysRepo);

            if (langNumberInput != 0 && langNumberInput == AvailableLangs.Length)
            {
                List<string> completedFolders = new List<string>();
                foreach (LanguageType langType in AvailableLangs)
                {
                    language = LanguageHelper.GetLanguageDetail(langType);
                    if (completedFolders.Contains(language.LanguageAppFolder)) continue;

                    GenerateAppFiles(gameFilesReader, outputFileSysRepo, appFilesSysRepo, appImagesRepo, language);
                    completedFolders.Add(language.LanguageAppFolder);
                }

                return 0;
            }

            while (true)
            {
                Console.WriteLine("Please select an option:");
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. Generate Intermediate Files");
                Console.WriteLine($"2. Create App Files in {language.LanguageGameFolder}");
                Console.WriteLine("3. Cut images from sprite map");
                Console.WriteLine("4. Generate App Data files");
                Console.WriteLine("5. Write server data to app files");
                Console.WriteLine("6. Add item to Language Pack");

                string input = Console.ReadLine();
                if (!int.TryParse(input, out int numberInput)) return 0;

                DataFileHandler dataFileHandler = new DataFileHandler(inputFileSysRepo, appDataSysRepo, attackFileSysRepo, appLangSysRepo);

                switch (numberInput)
                {
                    case 1:
                        gameFilesReader.GenerateIntermediate();
                        break;
                    case 2:
                        GenerateAppFiles(gameFilesReader, outputFileSysRepo, appFilesSysRepo, appImagesRepo, language);
                        break;
                    case 3:
                        Dictionary<string, List<ILocalised>> keyValueOfGameItems = gameFilesReader.GetKeyValueOfAllItems(includeOtherItems: true);

                        ImageCutter imageCutter = new ImageCutter(DataGuiDirectory, SurvivalGuiDirectory, ChallengeGuiDirectory, OutputDirectory);
                        imageCutter.CutOutImages(keyValueOfGameItems);
                        break;
                    case 4:
                        List<GameItemLocalised> gameItemsList = gameFilesReader.GetAllLocalisedGameItems(includeOtherItems: true);
                        dataFileHandler.GenerateDataFiles(gameItemsList);
                        break;
                    case 5:
                        await WriteServerDataToAppFiles(dataFileHandler);
                        break;
                    case 6:
                        AddItemToLanguagePacks();
                        break;
                    default:
                        return 0;
                }
                Console.WriteLine("- - - - - - - - - - - -");
            }
        }

        private static void GenerateAppFiles(FileHandlers.GameFilesReader gameFilesReader, FileSystemRepository outputFileSysRepo, FileSystemRepository appFilesSysRepo, FileSystemRepository appImagesRepo, LanguageDetail language)
        {
            Dictionary<string, List<ILocalised>> lookup = gameFilesReader.GetKeyValueOfGameItems(includeOtherItems: true);
            AppFilesHandler appFilesHandler = new AppFilesHandler(outputFileSysRepo, appFilesSysRepo, appImagesRepo);

            Dictionary<string, InventoryDescription> itemNames = gameFilesReader.LoadItemNames(language.LanguageGameFolder);
            appFilesHandler.GenerateAppFiles(language, itemNames, lookup);
        }

        private static async Task WriteServerDataToAppFiles(DataFileHandler dataFileHandler)
        {
            await dataFileHandler.WritePatreonFile();
            await dataFileHandler.WriteDonatorsFile();
            await dataFileHandler.WriteLanguageFiles();
            await dataFileHandler.WriteSteamNewsFile();
            await dataFileHandler.WriteContributorsFile();
            await dataFileHandler.WriteWhatIsNewFiles(AvailableLangs);

            await LangUpToDateAudit(true);
        }

        private static async Task<List<string>> LangUpToDateAudit(bool persist = false)
        {
            const string translationExportUrl = "https://api.assistantapps.com/TranslationExport/{0}/{1}";
            const string appGuid = "dfe0dbc7-8df4-47fb-a5a5-49af1937c4e2";
            List<string> consoleOutput = new List<string>();

            BaseExternalApiRepository apiRepo = new BaseExternalApiRepository();
            ResultWithValue<List<LanguageViewModel>> langResult = await apiRepo.Get<List<LanguageViewModel>>("https://api.assistantapps.com/Language");
            if (langResult.HasFailed)
            {
                consoleOutput.Add("Could not get Server Languages");
                return consoleOutput;
            }

            FileSystemRepository appLangRepo = new FileSystemRepository(AppLangDirectory);
            foreach (LanguageType langType in AvailableLangs)
            {
                LanguageDetail language = LanguageHelper.GetLanguageDetail(langType);

                string languageFile = $"language.{language.LanguageAppFolder}.json";
                Dictionary<string, dynamic> langJson = appLangRepo.LoadJsonDict<dynamic>(languageFile);
                langJson.TryGetValue("hashCode", out dynamic localHashCode);

                LanguageViewModel langViewModel = langResult.Value.FirstOrDefault(l => l.LanguageCode.Equals(language.LanguageAppFolder));
                if (langViewModel == null) continue;

                ResultWithValue<Dictionary<string, string>> languageContent = await apiRepo.Get<Dictionary<string, string>>(translationExportUrl.Replace("{0}", appGuid).Replace("{1}", langViewModel.Guid.ToString()));
                if (languageContent.HasFailed) continue;
                languageContent.Value.TryGetValue("hashCode", out string serverHashCode);

                bool hashCodeMatches = serverHashCode != null && localHashCode != null && localHashCode.Equals(serverHashCode);
                if (hashCodeMatches) continue;

                if (persist)
                {
                    appLangRepo.WriteBackToJsonFile(languageContent.Value, languageFile);
                }

                consoleOutput.Add($"{languageFile} Language Hashcode Audit has failed");
            }

            return consoleOutput;
        }

        private static void AddItemToLanguagePacks()
        {
            Console.WriteLine("New Key");
            string newKey = Console.ReadLine();
            Console.WriteLine("New Value");
            string newValue = Console.ReadLine();
            if (string.IsNullOrEmpty(newKey)) return;

            FileSystemRepository appLangRepo = new FileSystemRepository(AppLangDirectory);
            List<string> completedFolders = new List<string>();
            foreach (LanguageType langType in AvailableLangs)
            {
                LanguageDetail language = LanguageHelper.GetLanguageDetail(langType);
                if (completedFolders.Contains(language.LanguageAppFolder)) continue;

                string languageFile = $"language.{language.LanguageAppFolder}.json";
                Dictionary<string, dynamic> langJson = appLangRepo.LoadJsonDict<dynamic>(languageFile);
                if (langJson.ContainsKey(newKey)) continue;

                langJson.Add(newKey, newValue);
                appLangRepo.WriteBackToJsonFile(langJson, languageFile);

                completedFolders.Add(language.LanguageAppFolder);
            }
        }
    }
}
