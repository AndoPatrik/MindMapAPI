using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MindMapAPI.Models
{
    public class Project
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("boxes")]
        public List<object> Boxes { get; set; }
        [BsonElement("connections")]
        public List<object> Connections { get; set; }
    }
}
