using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager
{
    public class BoughtPackage
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("customerId")]
        public int CustomerId { get; set; }
        [BsonElement("packageId")]
        public ObjectId PackageId { get; set; }
        [BsonElement("purchaseDate")]
        public DateTime PurchaseDate { get; set; }
    }
}
