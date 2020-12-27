using System;
using Newtonsoft.Json;

namespace AssistantScrapMechanic.Domain.Dto.ViewModel
{
    public class DonationViewModel
    {
        [JsonProperty("guid")]
        public string Guid { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }
}
