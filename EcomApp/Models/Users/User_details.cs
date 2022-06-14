using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcomApp.Models.Users
{
    public class User_details 
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime? dOB { get; set; }
        public string address_line1 { get; set; }
        public string address_line2 { get; set; }
        public string city { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
    }
}
