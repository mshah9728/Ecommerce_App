using System;
using System.ComponentModel.DataAnnotations;

namespace EcomApp.Models
{
    public class product_Model
    {
        [Key]
        public Guid id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public Int64 quantity { get; set; }
        public string description { get; set; }
        public DateTime? createdTimeDate { get; set; }
        public Guid Createduserby { get; set; }
        public DateTime? modificationDateTime { get; set; }
        public Guid Modifyuserby { get; set; }
        public DateTime? DeleteTimeDate { get; set; }
        public Guid DeleteUserby { get; set; }
    }
}
