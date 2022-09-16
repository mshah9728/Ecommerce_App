using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcomApp.Models.Customer
{
    public class Customer_Model
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Customer_ID { get; set; }
        [StringLength(100)]
        public string Customer_Name { get; set; }
        [StringLength(100)]
        public string Customer_email { get;set; }
        [StringLength(100)]
        public string Customer_Phone { get; set; }
        [StringLength(1000)]
        public string Address { get; set; }
        [StringLength(100)]
        public string City { get; set; }
        [StringLength(100)]
        public string State { get; set; }
        [StringLength(100)]
        public string Country { get; set; }
        [StringLength(100)]
        public string ZipCode { get; set; }
        public Guid Createdby_userID { get; set; }
        public DateTime? creationDateTime { get; set; }
        public Guid ModifiedBy_userID { get; set; }
        public DateTime? modificationDateTime { get; set; }
        public Guid DeletedBy_UserID { get; set; }
        public DateTime DeletionDateTime { get; set; }
        public virtual Customer_Billing_Detail_Model Billing_Detail { get; set; }
    }
}