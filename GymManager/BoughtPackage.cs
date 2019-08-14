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
        [BsonElement("entrancesLeft")]
        public int EntrancesLeft { get; set; }

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

        public void add(Customer customer, Purchase pur)
        {
            //get bought packages collection
            IMongoCollection<BoughtPackage> collection = MongoDatabaseSingleton.Instance.database.GetCollection<BoughtPackage>("BoughtPackages");
            //add bought package
            collection.InsertOne(this);
            //add purchase
            pur.BoughtPackageId = collection.Find(bp => bp.PackageId == this.PackageId && bp.PurchaseDate == this.PurchaseDate).ToList().ElementAt(0).Id;
            pur.add();
            //for customers family
            if (this.convertToPackage().ForFamily == true)
            {
                List<Customer> customers = customer.getFamilyWithoutCustomer();
                customers.ForEach(c => {
                    Purchase p = pur;
                    p.Amount = 0;
                    p.add();
                });
            }
            
        }
    }
}
