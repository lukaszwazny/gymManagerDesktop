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
    class Relationship
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("firstCustomer")]
        public int FirstCustomer { get; set; }  //in relations parent-child - this is younger one,
                                                //in relations wife-husband - this is male
                                                //in relations with relatives - no difference
        [BsonElement("secondCustomer")]
        public int SecondCustomer { get; set; } //opposite as above
        [BsonElement("type")]
        public string Type { get; set; }        //type of relation (parent-child, wife-husband, relatives)

        //get customer being in relation with customer c
        public Customer getCustomer(Customer c)
        {
            IMongoCollection<Customer> collection = MongoDatabaseSingleton.Instance.database.GetCollection<Customer>("Customers");
            List<Customer> customers;
            if (c.Id == this.FirstCustomer)
                customers = collection.Find(cus => cus.Id == this.SecondCustomer).ToList();
            else
                customers = collection.Find(cus => cus.Id == this.FirstCustomer).ToList();
            return customers.ElementAt(0);
        }

        public void add()
        {
            IMongoCollection<Relationship> collection = MongoDatabaseSingleton.Instance.database.GetCollection<Relationship>("Relationships");
            collection.InsertOne(this);
        }
        public void delete()
        {
            IMongoCollection<Relationship> collection = MongoDatabaseSingleton.Instance.database.GetCollection<Relationship>("Relationships");
            collection.FindOneAndDelete(c => c.Id == this.Id);
        }

        //who is Customer c in this relationship
        public string getRole(Customer c)
        {
            if (this.Type == "parent-child")
            {
                //is child
                if(this.FirstCustomer == c.Id)
                {
                    if (c.Gender == "Kobieta")
                        return "Córka";
                    else
                        return "Syn";
                }
                //is parent
                else
                {
                    if (c.Gender == "Kobieta")
                        return "Matka";
                    else
                        return "Ojciec";
                }
            }
            else if (this.Type == "wife-husband")
            {
                if (c.Gender == "Kobieta")
                    return "Żona";
                else
                    return "Mąż";
            }
            else
            {
                if (c.Gender == "Kobieta")
                    return "Siostra";
                else
                    return "Brat";
            }
        } 
    }
}
