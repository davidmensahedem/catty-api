using Catty.Api.Models;
using MongoDB.Bson;

namespace MongoDB.Cats.Api.Models
{
    public class Cat:MongoDbDocument
    {
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? Type { get; set; }
        public List<string> Specialities { get; set; } = new();
    }
}
