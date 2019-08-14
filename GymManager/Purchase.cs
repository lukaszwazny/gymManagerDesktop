using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager
{
    class Purchase
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("customerId")]
        public int CustomerId { get; set; }
        [BsonElement("boughtPackageId")]
        public ObjectId BoughtPackageId { get; set; }
        [BsonElement("amount")]
        public double Amount { get; set; }
        [BsonElement("date")]
        public DateTime Date { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }       //type of payment (gotówka, przelew, karta, non)

        public BoughtPackage convertToBoughtPackage()
        {
            IMongoCollection<BoughtPackage> packagesCollection = MongoDatabaseSingleton.Instance.database.GetCollection<BoughtPackage>("BoughtPackages");
            List<BoughtPackage> p = packagesCollection.Find(pac => pac.Id == this.BoughtPackageId).Limit(1).ToList();
            return p.ElementAt(0);
        }

        public Package convertToPackage()
        {
            BoughtPackage bp = this.convertToBoughtPackage();
            Package p = bp.convertToPackage();
            return p;
        }

        public void add()
        {
            IMongoCollection<Purchase> collection = MongoDatabaseSingleton.Instance.database.GetCollection<Purchase>("Purchases");
            collection.InsertOne(this);
        }
    }
}
