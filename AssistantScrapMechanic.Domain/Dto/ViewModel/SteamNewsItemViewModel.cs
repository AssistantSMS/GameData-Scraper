using System;
using Newtonsoft.Json;

namespace AssistantScrapMechanic.Domain.Dto.ViewModel
{
    public class SteamNewsItemViewModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("shortDescription")]
        public string ShortDescription { get; set; }

        [JsonProperty("videoLink")]
        public string VideoLink { get; set; }

        [JsonProperty("upVotes")]
        public int UpVotes { get; set; }

        [JsonProperty("downVotes")]
        public int DownVotes { get; set; }

        [JsonProperty("commentCount")]
        public int CommentCount { get; set; }
    }
}
