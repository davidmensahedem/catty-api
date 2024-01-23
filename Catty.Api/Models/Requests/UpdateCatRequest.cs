using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Catty.Api.Models.Requests
{
    public class UpdateCatRequest
    {
        [JsonProperty("id")]
        [Required]
        public string? Id { get; set; }

        [JsonProperty("specialities")]
        [Required]
        public List<string>? Specialities { get; set; }
    }
}
