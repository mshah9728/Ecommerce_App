using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcomApp.Models.Product
{
    public class Product_LocationModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid P_L_ID { get; set; }
        [StringLength(100)]
        public string Product_location { get; set; }
        [StringLength(100)]
        public string Description { get;set; }
        public DateTime? CreatedDateTime { get; set; }
        public Guid CreateBy_userID { get; set; }
        public DateTime? ModificationDateTime { get; set; }
        public Guid ModifiedBy_userID { get; set; }
        public DateTime? DeletionDateTime { get; set; }
        public Guid DeleteBy_userID { get; set; }


        public Guid ProductID { get; set; }
        public product_Model Product { get; set; }
    }
}
