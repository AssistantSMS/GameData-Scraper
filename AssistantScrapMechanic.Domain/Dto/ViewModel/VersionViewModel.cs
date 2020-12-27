using System;
using System.Collections.Generic;
using AssistantScrapMechanic.Domain.Dto.Enum;
using Newtonsoft.Json;

namespace AssistantScrapMechanic.Domain.Dto.ViewModel
{
    public class VersionViewModel
    {
        [JsonProperty("guid")]
        public Guid Guid { get; set; }

        [JsonProperty("appGuid")]
        public Guid AppGuid { get; set; }

        [JsonProperty("markdown")]
        public string Markdown { get; set; }

        [JsonProperty("buildName")]
        public string BuildName { get; set; }

        [JsonProperty("buildNumber")]
        public int BuildNumber { get; set; }

        [JsonProperty("platforms")]
        public List<PlatformType> Platforms { get; set; }

        [JsonProperty("activeDate")]
        public DateTime ActiveDate { get; set; }
    }
}
