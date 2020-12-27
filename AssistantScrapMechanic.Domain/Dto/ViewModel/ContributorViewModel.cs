using Newtonsoft.Json;

namespace AssistantScrapMechanic.Domain.Dto.ViewModel
{
    public class ContributorViewModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
