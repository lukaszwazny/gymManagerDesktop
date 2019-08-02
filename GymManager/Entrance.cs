using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager
{
    public class Entrance
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("customerId")]
        public int CustomerId { get; set; }
        [BsonElement("boughtPackageId")]
        public ObjectId BoughtPackageId { get; set; }
        [BsonElement("date")]
        public DateTime Date { get; set; }
    }
}
