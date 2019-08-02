using MongoDB.Bson.Serialization.Attributes;
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
    }
}
