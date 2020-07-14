
using System.Collections.Generic;
using AssistantScrapMechanic.Domain.Enum;
using Newtonsoft.Json;

namespace AssistantScrapMechanic.Domain.AppFiles
{
    public class AppLoot
    {
        [JsonProperty("appId")]
        public string AppId { get; set; }

        [JsonProperty("chances")]
        public List<AppLootChance> Chances { get; set; }
    }

    public class AppLootChance
    {
        [JsonProperty("min")]
        public int Min { get; set; }

        [JsonProperty("max")]
        public int Max { get; set; }

        [JsonProperty("chance")]
        public int Chance { get; set; }

        [JsonProperty("type")]
        public AppLootContainerType Type { get; set; }
    }
}
