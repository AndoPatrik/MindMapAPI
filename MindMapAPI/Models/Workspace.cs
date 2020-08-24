using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace MindMapAPI.Models
{
    public class Workspace
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Owner { get; set; } //May be changed to GUID
        public int Team { get; set; } //May be changed to GUID
        public string CurrentState { get; set; }
        public List<int> Editors { get; set; }
        public List<int> Spectators { get; set; }
        public List<string> SavedHistory { get; set; }
        public List<Comment> Comments{ get; set; }
    }
}
