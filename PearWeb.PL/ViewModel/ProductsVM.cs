using Pear.DAL.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PearWeb.PL.ViewModel
{
    public class ProductsVM
    {
        public int ProductID { get; set; }
        [Required(ErrorMessage ="Name Is Required !")]
        public string ProductName { get; set; }
        [Required]
        public string QuantityPerUnit { get; set; }
        [Required(ErrorMessage ="ReorderLevel is Required !")]
        public int ReorderLevel { get; set; }
        
        public Nullable<int> SupplierID { get; set; }
        [Required(ErrorMessage ="Unit Price Is Required !")]
        public decimal UnitPrice { get; set; }
        [Required(ErrorMessage ="Unit In Stock Is Required !")]
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
    }
}