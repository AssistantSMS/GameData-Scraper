using AssistantScrapMechanic.Domain.Enum;

namespace AssistantScrapMechanic.Domain.Entity
{
    public class LanguageDetail
    {
        public LanguageType LanguageType { get; set; }
        public string LanguageGameFolder { get; set; }
        public string LanguageAppFolder { get; set; }

        public LanguageDetail(LanguageType languageType, string languageGameFolder, string languageAppFolder)
        {
            LanguageType = languageType;
            LanguageGameFolder = languageGameFolder;
            LanguageAppFolder = languageAppFolder;
        }
    }
}
