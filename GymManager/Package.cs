using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager
{
    public class Package
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("price")]
        public double Price { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("timeLimit")]
        public int TimeLimit { get; set; }       //unit - day; 0 means unlimited
        [BsonElement("entrancesLimit")]
        public int EntrancesLimit { get; set; }  //0 means unlimited
    }
}
