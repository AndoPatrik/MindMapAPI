using System;
using System.Collections.Generic;
using System.Linq;
using MindMapAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MindMapAPI.Util
{
    public class MongoConnection
    {
        private IMongoDatabase db;

        public MongoConnection(string database)
        {
            //TODO: Read from env var
            var client = new MongoClient("mongodb+srv://admin:Cookiemonsta88@mindmap-bsvon.azure.mongodb.net/MindMapDb?retryWrites=true&w=majority");
            db = client.GetDatabase(database);
        }

        public List<T> LoadRecords<T>(string table) 
        {
            var collection = db.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();
        }

        public T LoadRecordById<T>(string collection, ObjectId id)
        {
            var coll = db.GetCollection<T>(collection);
            var filter = Builders<T>.Filter.Eq("_id", id);
            return coll.Find(filter).First();
        }

        public User LoadUserByName(string name) 
        {
            try
            {
                var coll = db.GetCollection<User>("Customer");
                var filter = Builders<User>.Filter.Eq("name", name);
                return coll.Find(filter).First();

            }
            catch (Exception)
            {
                return null;
            }
         }

        public void InsertRecord<T>(string table, T record) 
        {
            var collection = db.GetCollection<T>(table);
            collection.InsertOne(record);
        }

        public User LoadUser(ObjectId id) 
        {
            var coll = db.GetCollection<User>("Users");
            return coll.Find(u => u.Id == id).First();
        }
    }
}
