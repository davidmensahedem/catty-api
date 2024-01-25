namespace Catty.Api.Interfaces
{
    public interface IMongoDbDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }
    }
}
