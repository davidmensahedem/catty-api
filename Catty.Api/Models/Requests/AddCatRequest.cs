using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Catty.Api.Models.Requests
{
    public class AddCatRequest
    {
        [JsonProperty("name")]
        [Required]
        public string? Name { get; set; }

        [JsonProperty("age")]
        [Required]
        public int Age { get; set; }

        [JsonProperty("gender")]
        [Required]
        public string? Gender { get; set; }

        [JsonProperty("type")]
        [Required]
        public string? Type { get; set; }

        [JsonProperty("specialities")]
        [Required]
        public List<string>? Specialities { get; set; }
    }
}
