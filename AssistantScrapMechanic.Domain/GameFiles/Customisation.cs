using System.Collections.Generic;
using Newtonsoft.Json;

namespace AssistantScrapMechanic.Domain.GameFiles
{
    public class Customization
    {
        [JsonProperty("categoryList")]
        public List<CustomizationListItem> Categories { get; set; }
    }

    public class CustomizationListItem
    {
        public string Name { get; set; }
        public List<Option> Options { get; set; }
    }

    public class Option
    {
        public string Female { get; set; }
        public long FemaleCode { get; set; }
        public string Male { get; set; }
        public long MaleCode { get; set; }
        public string Uuid { get; set; }
        public string Tier { get; set; }
        public bool AllowHair { get; set; }
    }

}
