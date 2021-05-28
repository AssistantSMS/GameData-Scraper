using System.Collections.Generic;

namespace AssistantScrapMechanic.Domain.Constant
{
    public static class LangFile
    {
        public static string German = @"language.de.json";
        public static string English = @"language.en.json";
        public static string Spanish = @"language.es.json";
        public static string French = @"language.fr.json";
        public static string Italian = @"language.it.json";
        public static string Dutch = @"language.nl.json";
        public static string Polish = @"language.pl.json";
        public static string BrazilianPortuguese = @"language.pt-br.json";
        public static string Portuguese = @"language.pt.json";
        public static string Russian = @"language.ru.json";
        public static string SimplifiedChinese = @"language.zh-hans.json";
        public static string TraditionalChinese = @"language.zh-hant.json";

        public static List<string> LanguagesInTheApp = new List<string> {
            German,
            English,
            Spanish,
            French,
            Italian,
            Dutch,
            Polish,
            BrazilianPortuguese,
            Portuguese,
            Russian,
            SimplifiedChinese,
            TraditionalChinese,
        };
    }
}
