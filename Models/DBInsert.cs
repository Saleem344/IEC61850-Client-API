using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Models;


namespace MongoDBWebAPI.Models
{
    public class Submission
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public dynamic Content { get; set; }
    }
}