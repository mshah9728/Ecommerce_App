using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcomApp.Models.Product
{
    public class Product_CategoryModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid P_Category_Id { get; set; }
        [StringLength(100)]
        public string P_Category_Name { get; set; }
        [StringLength(100)]
        public string P_Category_description { get; set; }
        public DateTime? createdTimeDate { get; set; }
        public Guid Createdby_Userid { get; set; }
        public DateTime? modificationDateTime { get; set; }
        public Guid Modifyby_userid { get; set; }
        public DateTime? DeleteTimeDate { get; set; }
        public Guid Deleteby_userid { get; set; }

        public IList<product_Model> Product { get; set; }

    }
}
