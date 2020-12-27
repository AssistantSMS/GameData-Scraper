using Newtonsoft.Json;

namespace AssistantScrapMechanic.Domain.Dto.ViewModel
{
    public class PatreonViewModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("thumbnailUrl")]
        public string ThumbnailUrl { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
