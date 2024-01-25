namespace Catty.Api.Models.Responses
{
    public class CatResponse
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? Type { get; set; }
        public List<string> Specialities { get; set; } = new();
    }
}
