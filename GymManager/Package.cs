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

        public static Package getPackageByName(string name)
        {
            //get whole collection
            IMongoCollection<Package> packagesCollection = MongoDatabaseSingleton.Instance.database.GetCollection<Package>("Packages");
            //get list of matching packages
            List<Package> packages = packagesCollection.Find(p => p.Name == name).ToList();
            //return package
            return packages.ElementAt(0);
        }

        public void add()
        {
            //get packages collection
            IMongoCollection<Package> collection = MongoDatabaseSingleton.Instance.database.GetCollection<Package>("Packages");
            collection.InsertOne(this);
        }

        //get all packages from collection
        public static List<Package> getPackages()
        {
            IMongoCollection<Package> packagesCollection = MongoDatabaseSingleton.Instance.database.GetCollection<Package>("Packages");
            //get list of all packages
            return packagesCollection.Find(_ => true).ToList();
        }

        public void update()
        {

            //defining what to update and with what data
            UpdateDefinition<Package> update = Builders<Package>.Update
            .Set(c => c.Name, this.Name)
            .Set(c => c.Price, this.Price)
            .Set(c => c.Description, this.Description)
            .Set(c => c.TimeLimit, this.TimeLimit)
            .Set(c => c.EntrancesLimit, this.EntrancesLimit);

            //get customers collection
            IMongoCollection<Package> collection = MongoDatabaseSingleton.Instance.database.GetCollection<Package>("Packages");

            //update customer
            collection.FindOneAndUpdate(c => c.Id == this.Id, update);
        }

        public void delete()
        {
            //get customers collection
            IMongoCollection<Package> collection = MongoDatabaseSingleton.Instance.database.GetCollection<Package>("Packages");
            //deleting
            collection.FindOneAndDelete(c => c.Id == this.Id);
        }
    }
}
