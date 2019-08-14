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
    public class Entrance
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("customerId")]
        public int CustomerId { get; set; }
        [BsonElement("boughtPackageId")]
        public ObjectId BoughtPackageId { get; set; }
        [BsonElement("trainingId")]
        public ObjectId TrainingId { get; set; }
        [BsonElement("date")]
        public DateTime Date { get; set; }

        public void add()
        {

            //get entrances collection
            IMongoCollection<Entrance> collection = MongoDatabaseSingleton.Instance.database.GetCollection<Entrance>("Entrances");

            //add to collection
            collection.InsertOne(this);
        }

        public static List<Entrance> getEntrances()
        {
            //get entrances collection
            IMongoCollection<Entrance> collection = MongoDatabaseSingleton.Instance.database.GetCollection<Entrance>("Entrances");

            //get all entrances to list
            return collection.Find(_ => true).ToList();
        }

        public Customer getCustomer()
        {
            IMongoCollection<Customer> customersCollection = MongoDatabaseSingleton.Instance.database.GetCollection<Customer>("Customers");
            List<Customer> c = customersCollection.Find(cus => cus.Id == this.CustomerId).ToList();
            return c.ElementAt(0);
        }

        public BoughtPackage getBoughtPackage()
        {
            IMongoCollection<BoughtPackage> boughtPackagesCollection = MongoDatabaseSingleton.Instance.database.GetCollection<BoughtPackage>("BoughtPackages");
            List<BoughtPackage> bp = boughtPackagesCollection.Find(pac => pac.Id == this.BoughtPackageId).ToList();
            return bp.ElementAt(0);
        }

        public Package getPackage()
        {
            return this.getBoughtPackage().convertToPackage();
        }

        public static List<EntrancesToShow> getEntrancesToShow()
        {

            //get all entrances to list
            List<Entrance> entrances = Entrance.getEntrances();

            //get list of customers that are in each entrance
            List<Customer> customers = new List<Customer>();
            entrances.ForEach(e => {
                customers.Add(e.getCustomer());
            });

            //get list of bought packages that are in each entrance
            List<Package> packages = new List<Package>();
            entrances.ForEach(e => {
                packages.Add(e.getPackage());
            });

            //make list of EntrancesToShow objects for displaying data about entrances
            List<EntrancesToShow> etss = new List<EntrancesToShow>();
            customers.ForEach(c =>
            {
                Package p = packages.ElementAt(customers.IndexOf(c));
                Entrance e = entrances.ElementAt(customers.IndexOf(c));
                EntrancesToShow ets = new EntrancesToShow
                {
                    Name = c.Name,
                    Surname = c.Surname,
                    packageName = p.Name,
                    entranceDate = e.Date
                };
                etss.Add(ets);
            });

            return etss;
        }
    }
}
