using EcomApp.Models.Supplier;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcomApp.Models.Product
{
    public class product_Model
    {
        public product_Model()
        {
            Location = new List<Product_LocationModel>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Product_id { get; set; }
        [StringLength(100)]
        public string Product_UPC_Code { get; set; }
        [StringLength(100)]
        public string Product_Name { get; set; }
        [StringLength(100)]
        public string product_image_URL { get; set; }
        
        public decimal price { get; set; }
       
        public decimal discounted_percentage { get; set; }
        
        public bool item_in_stock { get; set; }
        
        public long quantity { get; set; }
        [StringLength(1000)]
        public string description { get; set; }
        public DateTime? createdTimeDate { get; set; }
        public Guid Createduserby { get; set; }
        public DateTime? modificationDateTime { get; set; }
        public Guid Modifyuserby { get; set; }
        public DateTime? DeleteTimeDate { get; set; }
        public Guid DeleteUserby { get; set; }
        //Suplier  many to One relationship
        public Guid SupplierID { get; set; }
        public Supplier_Model Supplier { get; set; }

        //Product Category many to one relationship
        public Guid CategoryID { get; set; }
        public  Product_CategoryModel Category { get; set; }

        //Product with Location one to many relationships
        public virtual List<Product_LocationModel> Location { get; set; }
    }
}
