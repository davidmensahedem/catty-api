using Catty.Api.Interfaces;
using MongoDB.Bson;

namespace Catty.Api.Models
{
    public abstract class MongoDbDocument : IMongoDbDocument
    {
        public ObjectId Id { get; set; }
    }
}
