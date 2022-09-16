using EcomApp.Models.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcomApp.Models.Supplier
{
    public class Supplier_Model
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Supplier_ID { get; set; }
        [StringLength(100)]
        public string Supplier_Name { get; set; }
        [StringLength(100)]
        public string Supplier_Email { get;set; }
        [StringLength(100)]
        public string Supplier_Phone { get; set; }
        [StringLength(1000)]
        public string Address { get;set;}
        [StringLength(100)]
        public string City { get; set; }
        [StringLength(100)]
        public string State { get; set; }
        [StringLength(100)]
        public string Country { get; set; }
        public List<product_Model> Product { get; set; }
        public Guid Createdby_userID { get; set; }
        public DateTime? creationDateTime { get; set; }
        public Guid ModifiedBy_userID { get; set; }
        public DateTime? modificationDateTime { get; set; }
        public Guid DeletedBy_UserID { get; set; }
        public DateTime DeletionDateTime { get; set; } 
    }
}
