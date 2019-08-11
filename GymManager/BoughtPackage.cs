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
    public class BoughtPackage
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("packageId")]
        public ObjectId PackageId { get; set; }
        [BsonElement("purchaseDate")]
        public DateTime PurchaseDate { get; set; }

        //for converting from BoughtPackage to Package type
        public Package convertToPackage()
        {
            IMongoCollection<Package> packagesCollection = MongoDatabaseSingleton.Instance.database.GetCollection<Package>("Packages");
            List<Package> p = packagesCollection.Find(pac => pac.Id == this.PackageId).Limit(1).ToList();
            return p.ElementAt(0);
        }

        //get name of bought package
        public string getName()
        {
            Package p = this.convertToPackage();
            return p.Name;
        }

        public void add(Customer customer)
        {
            //get bought packages collection
            IMongoCollection<BoughtPackage> collection = MongoDatabaseSingleton.Instance.database.GetCollection<BoughtPackage>("BoughtPackages");
            collection.InsertOne(this);
            List<Customer> customers = customer.getFamily();
            customers.ForEach( c => {
                Purchase p = new Purchase
                {
                    CustomerId = c.Id,
                    BoughtPackageId = collection.Find(bp => bp.PackageId == this.PackageId && bp.PurchaseDate == this.PurchaseDate).ToList().ElementAt(0).Id
                };
                p.add();
            });
        }
    }
}
