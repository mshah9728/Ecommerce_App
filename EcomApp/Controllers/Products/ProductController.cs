using EcomApp.Configuration.SQLConfiguration;
using EcomApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EcomApp.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDBContext _productDB;
        public ProductController(ProductDBContext productDBContext)
        {
           _productDB = productDBContext;
        }
        [HttpGet]
        public IActionResult getAllProducts()
        {
            var product_list = _productDB.Products.ToList();
            if(product_list != null)
            {
                return Ok(product_list);
            }
            else
            {
                return BadRequest();
            }
            
            
        }
    }
}
