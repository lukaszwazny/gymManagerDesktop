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
    class PaymentType
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }

        public static List<string> getPaymentTypesString()
        {
            IMongoCollection<PaymentType> collection = MongoDatabaseSingleton.Instance.database.GetCollection<PaymentType>("PaymentTypes");
            //get list of all packages
            List<PaymentType> paymentTypes = collection.Find(_ => true).ToList();
            List<string> names = new List<string>();
            paymentTypes.ForEach(p => {
                names.Add(p.Name);
            });
            return names;
        }

        public static List<PaymentType> getPaymentTypes()
        {
            IMongoCollection<PaymentType> collection = MongoDatabaseSingleton.Instance.database.GetCollection<PaymentType>("PaymentTypes");
            //get list of all packages
            List<PaymentType> paymentTypes = collection.Find(_ => true).ToList();
            return paymentTypes;
        }

        public void add()
        {
            //name and surname required
            if (this.Name == "")
                throw new Exception("Nazwa nie może być pusta!");

            //get customers collection
            IMongoCollection<PaymentType> collection = MongoDatabaseSingleton.Instance.database.GetCollection<PaymentType>("PaymentTypes");

            //add customer to collection
            collection.InsertOne(this);
        }
    }
}
