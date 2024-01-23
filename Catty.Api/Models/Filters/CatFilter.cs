using Newtonsoft.Json;

namespace Catty.Api.Models.Filters
{
    public class CatFilter
    {
        [JsonProperty("type")]
        public string? Type { get; set; } = null;

        [JsonProperty("name")]
        public string? Name { get; set; } = null;
    }
}
