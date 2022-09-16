using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcomApp.Models.Customer
{
    public class Customer_Billing_Detail_Model
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid C_BillingDetail_ID { get; set; }
        [StringLength(100)]
        public string C_BillingDetail_Name { get; set; }
        [StringLength(100)]
        public string C_billingDetail_Phone { get; set; }
        [StringLength(1000)]
        public string Address { get; set; }
        [StringLength(100)]
        public string City { get; set; }
        [StringLength(100)]
        public string State { get; set; }
        public string ZipCode { get; set; }
        [StringLength(100)]
        public string Country { get; set; }
   
        public DateTime? CreatedDateTime { get; set; }
        public Guid CreateBy_userID { get; set; }
        public DateTime? ModificationDateTime { get; set; }
        public Guid ModifiedBy_userID { get; set; }

        public Guid CustomerID { get; set; }
        public virtual Customer_Model Customer { get; set; }

    }
}
