using System;
using System.ComponentModel.DataAnnotations;

namespace EcomApp.Models.Product
{
    public class Product_View_Model
    {
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
        public Guid CategoryID { get; set; }
        public string CategoryName { get; set; }
        public Guid SupplierID { get; set; }
        public String SupplierName { get; set; }
    }
}
