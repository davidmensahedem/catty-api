namespace Catty.Api.Models.Requests
{
    public class UpdateCatRequest
    {
        [JsonProperty("specialities")]
        [Required]
        public List<string>? Specialities { get; set; }
    }
}
