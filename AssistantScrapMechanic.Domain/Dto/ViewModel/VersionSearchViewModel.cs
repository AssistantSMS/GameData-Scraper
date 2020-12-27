using System;
using System.Collections.Generic;
using AssistantScrapMechanic.Domain.Dto.Enum;

namespace AssistantScrapMechanic.Domain.Dto.ViewModel
{
    public class VersionSearchViewModel
    {
        public Guid AppGuid { get; set; }
        public List<PlatformType> Platforms { get; set; }
        public string LanguageCode { get; set; }
        public int Page { get; set; }
    }
}
