using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GymManager
{
    //singleton of our database
    public sealed class MongoDatabaseSingleton
    {
        private MongoDatabaseSingleton()
        {
        }
        private static MongoDatabaseSingleton instance = null;
        public static MongoDatabaseSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MongoDatabaseSingleton();
                }
                return instance;
            }
        }
        //field containing client object
        public MongoClient client { get; set; }
        //field containing database object
        public IMongoDatabase database { get; set; }


        
    }
}

