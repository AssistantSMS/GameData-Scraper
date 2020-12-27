using System.Collections.Generic;
using Newtonsoft.Json;

namespace AssistantScrapMechanic.Domain.Result
{
    public class ResultWithPagination<T>
    {
        [JsonProperty("currentPage")]
        public int CurrentPage { get; set; }

        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }

        [JsonProperty("totalRows")]
        public int TotalRows { get; set; }

        [JsonProperty("value")]
        public List<T> Value { get; set; }

        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty("hasFailed")]
        public bool HasFailed { get; set; }

        [JsonProperty("exceptionMessage")]
        public string ExceptionMessage { get; set; }
    }
}
