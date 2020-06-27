using AssistantScrapMechanic.Domain.Entity;
using AssistantScrapMechanic.Domain.Enum;

namespace AssistantScrapMechanic.Logic
{
    public static class LanguageHelper
    {
        public static LanguageDetail GetLanguageDetail(LanguageType selectedLangType)
        {
            switch (selectedLangType)
            {
                case LanguageType.Brazilian: return new LanguageDetail(LanguageType.Brazilian, "Brazilian", "pt-br");
                case LanguageType.Chinese: return new LanguageDetail(LanguageType.English, "Chinese", "zh-hans");
                case LanguageType.French: return new LanguageDetail(LanguageType.French, "French", "fr");
                case LanguageType.German: return new LanguageDetail(LanguageType.German, "German", "de");
                case LanguageType.Italian: return new LanguageDetail(LanguageType.Italian, "Italian", "it");
                case LanguageType.Japanese: return new LanguageDetail(LanguageType.Japanese, "Japanese", "ja");
                case LanguageType.Korean: return new LanguageDetail(LanguageType.Korean, "Korean", "ko");
                case LanguageType.Polish: return new LanguageDetail(LanguageType.Polish, "Polish", "po");
                case LanguageType.Russian: return new LanguageDetail(LanguageType.Russian, "Russian", "ru");
                case LanguageType.Spanish: return new LanguageDetail(LanguageType.Spanish, "Spanish", "es");
                default: return new LanguageDetail(LanguageType.English, "English", "en");
            }
        }
    }
}
