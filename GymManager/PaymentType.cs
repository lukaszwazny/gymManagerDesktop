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

        public static List<string> getPaymentTypes()
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
    }
}
