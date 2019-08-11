using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManager
{
    public class Customer
    {
        [BsonId]
        public int Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("surname")]
        public string Surname { get; set; }
        [BsonElement("gender")]
        public string Gender { get; set; }
        [BsonElement("phone")]
        public string Phone { get; set; }
        [BsonElement("cardNumber")]
        public string CardNumber { get; set; }  //number on RFID card
        //place, where customer lives
        [BsonElement("street")]
        public string Street { get; set; }
        [BsonElement("streetNumber")]
        public string StreetNumber { get; set; }
        [BsonElement("zipCode")]
        public string ZipCode { get; set; }
        [BsonElement("city")]
        public string City { get; set; }
        [BsonElement("birthday")]
        public DateTime Birthday { get; set; }
        [BsonElement("joinDate")]
        public DateTime JoinDate { get; set; }  //when customer joined the gym
        [BsonElement("trainingDiscount")]
        public double TrainingDiscount { get; set; } //decimal value [0...1]

        public void add()
        {
            //name and surname required
            if (this.Name == "")
                throw new Exception("Imię nie może być puste!");
            if (this.Surname == "")
                throw new Exception("Nazwisko nie może być puste!");

            //get customers collection
            IMongoCollection<Customer> collection = MongoDatabaseSingleton.Instance.database.GetCollection<Customer>("Customers");

            //get Customer with maximal Id
            Customer maxId = collection.Find(_ => true).Sort("{_id: -1}").ToList().ElementAt(0);

            //set id of customer
            this.Id = ++(maxId.Id);

            //add customer to collection
            collection.InsertOne(this);
        }

        //get all bought packages by "this" customer
        public List<BoughtPackage> getBoughtPackages()
        {
            IMongoCollection<Purchase> purchaseCollection = MongoDatabaseSingleton.Instance.database.GetCollection<Purchase>("Purchases");
            List<Purchase> customersPurchases = purchaseCollection.Find(bp => bp.CustomerId == this.Id).ToList();

            List<BoughtPackage> customersPackages = new List<BoughtPackage>();
            customersPurchases.ForEach(cp => {
                customersPackages.Add(cp.convertToBoughtPackage());
            });
            return customersPackages;
        }

        //get all bought packages by "this" customer in Package objects
        public List<Package> getPackages()
        {
            List<BoughtPackage> customersPackages = this.getBoughtPackages();
            List<Package> packages = new List<Package>();
            IMongoCollection<Package> packagesCollection = MongoDatabaseSingleton.Instance.database.GetCollection<Package>("Packages");
            customersPackages.ForEach(cp =>
            {
                packages.Add(cp.convertToPackage());
            });
            return packages;
        }

        //get all bought packages by "this" customer in boughtPackagesToShow objects
        public List<boughtPackagesToShow> getBoughtPackagesToShow()
        {
            //find bought packages bought by customer
            List<BoughtPackage> customersPackages = this.getBoughtPackages();
            //get list of package objects that customer bought
            List<Package> packages = this.getPackages();
            //make list of boughtPackagesToShow objects for displaying data abiut bought packages
            List<boughtPackagesToShow> bptss = new List<boughtPackagesToShow>();
            customersPackages.ForEach(cp =>
            {
                Package p = packages.ElementAt(customersPackages.IndexOf(cp));
                boughtPackagesToShow bpts = new boughtPackagesToShow
                {
                    Name = p.Name,
                    Price = p.Price,
                    TimeLimit = p.TimeLimit,
                    EntrancesLimit = p.EntrancesLimit,
                    PurchaseDate = cp.PurchaseDate
                };
                bptss.Add(bpts);
            });
            return bptss;
        }

        //get bought package with given name
        public BoughtPackage getBoughtPackageByName(string name)
        {
            //get package selected by user
            Package package = Package.getPackageByName(name);

            //get bought package selected by user
            BoughtPackage boughtPackage = new BoughtPackage();
            this.getBoughtPackages().ForEach(bp =>
            {
                if (bp.PackageId == package.Id)
                    boughtPackage = bp;
            });

            return boughtPackage;
        }

        public void update()
        {

            //defining what to update and with what data
            UpdateDefinition<Customer> update = Builders<Customer>.Update
            .Set(c => c.Name, this.Name)
            .Set(c => c.Surname, this.Surname)
            .Set(c => c.CardNumber, this.CardNumber)
            .Set(c => c.Gender, this.Gender)
            .Set(c => c.Phone, this.Phone)
            .Set(c => c.Street, this.Street)
            .Set(c => c.StreetNumber, this.StreetNumber)
            .Set(c => c.ZipCode, this.ZipCode)
            .Set(c => c.City, this.City)
            .Set(c => c.Birthday, this.Birthday)
            .Set(c => c.JoinDate, this.JoinDate);

            //get customers collection
            IMongoCollection<Customer> collection = MongoDatabaseSingleton.Instance.database.GetCollection<Customer>("Customers");

            //update customer
            collection.FindOneAndUpdate(c => c.Id == this.Id, update);
        }

        public void delete()
        {
            //get customers collection
            IMongoCollection<Customer> collection = MongoDatabaseSingleton.Instance.database.GetCollection<Customer>("Customers");
            //deleting
            collection.FindOneAndDelete(c => c.Id == this.Id);
        }

        private List<Relationship> getRelationships()
        {
            IMongoCollection<Relationship> collection = MongoDatabaseSingleton.Instance.database.GetCollection<Relationship>("Relationships");
            List<Relationship> relationships = collection.Find(c => c.FirstCustomer == this.Id || c.SecondCustomer == this.Id).ToList();
            return relationships;
        }

        public List<Customer> getFamilyWithoutCustomer()
        {
            List<Relationship> relationships = this.getRelationships();
            List<Customer> customers = new List<Customer>();
            relationships.ForEach(r => {
                customers.Add(r.getCustomer(this));
            });
            return customers;
        }

        public List<Customer> getFamily()
        {
            List<Customer> customers = this.getFamilyWithoutCustomer();
            customers.Add(this);
            return customers;
        }

        public List<Customer> getChildren()
        {
            List<Relationship> relationships = this.getRelationships();
            List<Customer> customers = new List<Customer>();
            relationships.ForEach(r => {
                if(r.Type == "parent-child" && this.Id == r.SecondCustomer)
                    customers.Add(r.getCustomer(this));
            });
            return customers;
        }

        public Customer getPartner()
        {
            List<Relationship> relationships = this.getRelationships();
            Customer partner = null;
            relationships.ForEach(r => {
                if (r.Type == "wife-husband")
                    partner = r.getCustomer(this);
            });
            return partner;
        }

        public List<Customer> getRelatives()
        {
            List<Relationship> relationships = this.getRelationships();
            List<Customer> customers = new List<Customer>();
            relationships.ForEach(r => {
                if (r.Type == "relatives")
                    customers.Add(r.getCustomer(this));
            });
            return customers;
        }

        public Customer getMother()
        {
            List<Relationship> relationships = this.getRelationships();
            Customer partner = null;
            relationships.ForEach(r => {
                if (r.Type == "parent-child" && r.FirstCustomer == this.Id && r.getCustomer(this).Gender == "Kobieta")
                    partner = r.getCustomer(this);
            });
            return partner;
        }

        public Customer getFather()
        {
            List<Relationship> relationships = this.getRelationships();
            Customer partner = null;
            relationships.ForEach(r => {
                if (r.Type == "parent-child" && r.FirstCustomer == this.Id && r.getCustomer(this).Gender == "Mężczyzna")
                    partner = r.getCustomer(this);
            });
            return partner;
        }

        public bool hasWife()
        {
            if (this.Gender == "Mężczyzna")
            {
                List<Relationship> relationships = this.getRelationships();
                bool has = false;
                relationships.ForEach(r => {
                    if(r.Type == "wife-husband")
                    {
                        has = true;
                    }
                });
                return has;
            }
            else
            {
                return false;
            }
        }

        public bool hasHusband()
        {
            if (this.Gender == "Kobieta")
            {
                List<Relationship> relationships = this.getRelationships();
                bool has = false;
                relationships.ForEach(r => {
                    if (r.Type == "wife-husband")
                    {
                        has = true;
                    }
                });
                return has;
            }
            else
            {
                return false;
            }
        }

        public bool hasFather()
        {
            List<Relationship> relationships = this.getRelationships();
            bool has = false;
            relationships.ForEach(r => {
                if (r.Type == "parent-child")
                {
                    if(r.FirstCustomer == this.Id)
                    {
                        if(r.getCustomer(this).Gender == "Mężczyzna")
                        {
                            has = true;
                        }
                    }
                }
            });
            return has;
        }

        public bool hasMother()
        {
            List<Relationship> relationships = this.getRelationships();
            bool has = false;
            relationships.ForEach(r => {
                if (r.Type == "parent-child")
                {
                    if (r.FirstCustomer == this.Id)
                    {
                        if (r.getCustomer(this).Gender == "Kobieta")
                        {
                            has = true;
                        }
                    }
                }
            });
            return has;
        }

        public bool isInFamily(Customer c)
        {
            bool has = false;
            List<Customer> family = this.getFamily();
            family.ForEach(f =>
            {
                if(f.Id == c.Id)
                {
                    has = true;
                }
            });
            return has;
        }

        public void addFamilyMember(Customer c, string type)
        {
            Console.WriteLine("elo");
            if (c == null)
                return;
            if (this.isInFamily(c))
                return;
            Relationship relation = new Relationship();
            if(type == "Ojciec")
            {
                if (c.Gender == "Kobieta")
                    throw new Exception("Nie może być ojcem kobieta!");
                if (!(this.hasFather()))
                {
                    relation.FirstCustomer = this.Id;
                    relation.SecondCustomer = c.Id;
                    relation.Type = "parent-child";
                    relation.add();

                    List<Customer> thisRelatives = c.getChildren();
                    thisRelatives.ForEach(r => {
                        this.addFamilyMember(r, "Siostra");
                    });

                    Customer mother = c.getPartner();
                    this.addFamilyMember(mother, "Matka");

                    List<Customer> cChildren = this.getRelatives();
                    cChildren.ForEach(r => {
                        c.addFamilyMember(r, "Syn");
                    });
                }
                else
                {
                    throw new Exception("Dany klient posiada już ojca!");
                }
            }
            if (type == "Matka")
            {
                if (c.Gender == "Mężczyzna")
                    throw new Exception("Nie może być matką mężczyzna!");
                if (!(this.hasMother()))
                {
                    relation.FirstCustomer = this.Id;
                    relation.SecondCustomer = c.Id;
                    relation.Type = "parent-child";
                    relation.add();

                    List<Customer> thisRelatives = c.getChildren();
                    thisRelatives.ForEach(r => {
                        this.addFamilyMember(r, "Brat");
                    });

                    Customer father = c.getPartner();
                    this.addFamilyMember(father, "Ojciec");

                    List<Customer> cChildren = this.getRelatives();
                    cChildren.ForEach(r => {
                        c.addFamilyMember(r, "Córka");
                    });

                }
                else
                {
                    throw new Exception("Dany klient posiada już matkę!");
                }
            }
            if (type == "Syn" || type == "Córka")
            {
                relation.FirstCustomer = c.Id;
                relation.SecondCustomer = this.Id;
                relation.Type = "parent-child";
                relation.add();

                List<Customer> thisChildren = c.getRelatives();
                thisChildren.ForEach(r => {
                    this.addFamilyMember(r, "Syn");
                });

                List<Customer> cRelatives = this.getChildren();
                cRelatives.ForEach(r => {
                    c.addFamilyMember(r, "Siostra");
                });
            }
            if (type == "Żona")
            {
                if (c.Gender == "Mężczyzna")
                    throw new Exception("Nie może być żoną mężczyzna!");
                if (!(this.hasWife()))
                {
                    relation.FirstCustomer = this.Id;
                    relation.SecondCustomer = c.Id;
                    relation.Type = "wife-husband";
                    relation.add();

                    List<Customer> thisChildren = c.getChildren();
                    thisChildren.ForEach(r => {
                        this.addFamilyMember(r, "Córka");
                    });

                    List<Customer> cChildren = this.getChildren();
                    cChildren.ForEach(r => {
                        c.addFamilyMember(r, "Syn");
                    });
                }
                else
                {
                    throw new Exception("Dany klient posiada już żonę!");
                }
            }
            if (type == "Mąż")
            {
                if (c.Gender == "Kobieta")
                    throw new Exception("Nie może być mężem kobieta!");
                if (!(this.hasHusband()))
                {
                    relation.FirstCustomer = c.Id;
                    relation.SecondCustomer = this.Id;
                    relation.Type = "wife-husband";
                    relation.add();

                    List<Customer> thisChildren = c.getChildren();
                    thisChildren.ForEach(r =>
                    {
                        this.addFamilyMember(r, "Syn");
                    });

                    List<Customer> cChildren = this.getChildren();
                    cChildren.ForEach(r =>
                    {
                        c.addFamilyMember(r, "Córka");
                    });
                }
                else
                {
                    throw new Exception("Dana klientka posiada już męża!");
                }
            }
            if (type == "Siostra" || type == "Brat")
            {
                relation.FirstCustomer = c.Id;
                relation.SecondCustomer = this.Id;
                relation.Type = "relatives";
                relation.add();

                List<Customer> thisRelatives = c.getRelatives();
                thisRelatives.ForEach(r => {
                    this.addFamilyMember(r, "Brat");
                });

                Customer father = c.getFather();
                this.addFamilyMember(father, "Ojciec");

                Customer mother = c.getMother();
                this.addFamilyMember(mother, "Matka");

                List<Customer> cRelatives = this.getRelatives();
                cRelatives.ForEach(r => {
                    c.addFamilyMember(r, "Siostra");
                });

                Customer cfather = this.getFather();
                c.addFamilyMember(cfather, "Ojciec");
                
                Customer cmother = this.getMother();
                c.addFamilyMember(cmother, "Matka");

            }
        }
    }
}
