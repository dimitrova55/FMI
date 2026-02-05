using MongoDB.Bson.Serialization.Attributes;

namespace fmi.Models
{
    public class User
    {
        [BsonId]
        // [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Username { get; set; } = string.Empty;
        public string? Password { get; set; } = null;

        public string? GoogleId { get; set; } = null;

        public string? Email { get; set; }
    }
}
