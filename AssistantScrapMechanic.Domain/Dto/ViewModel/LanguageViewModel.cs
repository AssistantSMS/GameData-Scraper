using System;

namespace AssistantScrapMechanic.Domain.Dto.ViewModel
{
    public class LanguageViewModel
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string LanguageCode { get; set; }
        public string CountryCode { get; set; }
        public int SortOrder { get; set; }
        public bool IsVisible { get; set; }
    }
}
