using EcomApp.Configuration.CacheMemory;
using EcomApp.Configuration.SQLConfiguration;
using EcomApp.Models;
using EcomApp.Models.Product;
using EcomApp.Services.Product;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcomApp.Controllers.Products
{
    [ApiController]
    [EnableCors("Cors")]
    public class ProductController : ControllerBase
    {
        private readonly PosAppDB _dbcontext;
        private readonly IProduct_LocationService _product_Location;
        private readonly Cachememory _cache;
        public ProductController(PosAppDB DBContext, IProduct_LocationService product_Location, Cachememory cache)
        {
            _dbcontext = DBContext;
            _product_Location = product_Location;
            _cache = cache;
        }
        [HttpGet]
        [Route("api/[controller]/getallproducts")]
        public IActionResult getAllProducts()
        {
            var product_list = _dbcontext.Products.ToList();
            if(product_list != null)
            {
                return Ok(new { Status=StatusCodes.Status200OK, Message="Success", Result = product_list });
            }
            else
            {
                return BadRequest( new {Status = StatusCodes.Status400BadRequest, Message = "Failed", Result = "Product List not avaialable"});
            }
        }
        [HttpGet]
        [Route("api/[controller]/getproductbyID")]
        public IActionResult getproductByID( Guid Product_ID)
        {
            if(Product_ID == Guid.Empty)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { Status = StatusCodes.Status400BadRequest, Message ="Failed", Result = "Please Provide Product ID." });
            }
            else
            {
                var getproduct = _dbcontext.Products.Find(Product_ID);
                if(getproduct == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Status = StatusCodes.Status404NotFound, Message = "Failed", Result = "Please Provide Product ID." });
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, new { Status = StatusCodes.Status200OK, Message = " Success", Result = getproduct });
                }
            }
        }
        [HttpPost]
        [Route("api/[controller]/addproduct")]
        public IActionResult addproduct([FromBody]Product_View_Model model)
        {
            if (ModelState.IsValid)
            {
                var supplierName = _dbcontext.Supplier.Find(model.SupplierID);
                var categoryName = _dbcontext.Product_Categories.Find(model.CategoryID);
                product_Model product_Model = new product_Model()
                {
                    Product_UPC_Code = model.Product_UPC_Code,
                    Product_Name = model.Product_Name,
                    product_image_URL = model.product_image_URL,
                    price = model.price,
                    quantity = model.quantity,
                    discounted_percentage = model.discounted_percentage,
                    item_in_stock = model.item_in_stock,
                    description = model.description,
                    createdTimeDate = System.DateTime.Now,
                    Supplier = supplierName,
                    Category = categoryName,
                    Createduserby = new System.Guid(),
                    modificationDateTime = null,
                    Modifyuserby = System.Guid.Empty,
                    DeleteTimeDate = null,
                    DeleteUserby = System.Guid.Empty
                };
                _dbcontext.Products.Add(product_Model);
                var dbstored = _dbcontext.SaveChanges();
                if (dbstored > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, new { Status = StatusCodes.Status200OK, Message = " Success", Result = " Data Stored Successfully!." });
                }
                return StatusCode(StatusCodes.Status400BadRequest, new { Status = StatusCodes.Status400BadRequest, Message = "Failed", Result = "Something went wrong Please Contact to Administrator" });
               
            }
            else
            {
                return StatusCode(StatusCodes.Status405MethodNotAllowed, new { Status = StatusCodes.Status405MethodNotAllowed, Message = "Failed", Result = "Enter the values in Fields" });
            }
        }
        [HttpPut]
        [Route("api/[controller]/{version}/updateItem")]
        public IActionResult UpdateProduct(Guid Product_ID, string Version,[FromBody] Product_View_Model product )
        {
            if(Product_ID == Guid.Empty && !ModelState.IsValid && Version == "1.0")
            {
                return StatusCode(StatusCodes.Status404NotFound, new { Status = StatusCodes.Status404NotFound, Message ="Failed", Result = "Parameter not passed Correctly. Please Try again!." });
            }
            else
            {
                var recordFound = _dbcontext.Products.Find(Product_ID);
                if(recordFound == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Status = StatusCodes.Status400BadRequest, Message =" Failed", Result ="Record Mismatched Please try again..!" });
                }
                else
                {
                    recordFound.Product_UPC_Code = product.Product_UPC_Code;
                    recordFound.Product_Name = product.Product_Name;
                    recordFound.price = product.price;
                    recordFound.quantity = product.quantity;
                    recordFound.Modifyuserby = Guid.NewGuid();
                    recordFound.modificationDateTime = DateTime.Now;
                    recordFound.item_in_stock = product.item_in_stock;
                    recordFound.description = product.description;
                    recordFound.discounted_percentage = product.discounted_percentage;
                    recordFound.SupplierID = product.SupplierID;
                    recordFound.CategoryID = product.CategoryID;

                    _dbcontext.Products.Update(recordFound);
                    var updateproduct = _dbcontext.SaveChanges();
                    if(updateproduct > 0)
                    {
                        return StatusCode(StatusCodes.Status200OK, new { Status =  StatusCodes.Status200OK, Message ="Failed", Result = "Record updated Successfully" });
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new { Status = StatusCodes.Status500InternalServerError, Message = "Failed", Result = "Something went wrong Please try again" });
                    }
                }
            }
        }
        [HttpDelete]
        [Route("api/[controller]/{version}/DeleteItem")]
        public IActionResult DeleteItem(Guid Product_ID, string version)
        {
            if(Product_ID == Guid.Empty && version != "1.0")
            {
                return StatusCode(StatusCodes.Status405MethodNotAllowed, new { Status = StatusCodes.Status405MethodNotAllowed, Message ="Failed", Result =" Product ID is not correct Please Send Correct ID" });
            }
            else
            {
                var checkrecord = _dbcontext.Products.Find(Product_ID);
                if(checkrecord == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { Message = "Failed", Status = StatusCodes.Status404NotFound, Result ="Product ID is not found"});
                }
                else
                {
                    _dbcontext.Products.Remove(checkrecord);
                    var deleterecord = _dbcontext.SaveChanges();
                    if(deleterecord > 0)
                    {
                        return StatusCode(StatusCodes.Status200OK, new { Status = StatusCodes.Status200OK, Message ="Success", result = $"Record with {checkrecord.Product_id} is Deleted" });
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new { Status = StatusCodes.Status500InternalServerError, Message = "Failed", Result = "Something went wrong Please Contact with Administrator" });
                    }
                }
            }
        }
        [HttpGet]
        [Route("api/[controller]/getallCategories")]
        public IActionResult getAllCategories()
        {
            var category_list = _dbcontext.Product_Categories.ToList();
            if (category_list != null)
            {
                return Ok(new { Status = StatusCodes.Status200OK, Message = "Success", Result = category_list });
            }
            else
            {
                return BadRequest(new { Status = StatusCodes.Status400BadRequest, Message = "Failed", Result = "Category List not avaialable" });
            }
        }
        [HttpGet]
        [Route("api/[controller]/{version}/getcategoryByID")]
        public IActionResult getCategoryByID(Guid Category_ID, string version)
        {
            if(Category_ID == Guid.Empty && version == "1.0")
            {
                return StatusCode(StatusCodes.Status405MethodNotAllowed, new { Status = StatusCodes.Status405MethodNotAllowed, Message = "Failed", Result = "" });
            }
            else
            {
                var getproductlocation =_product_Location.getProductLocationByID(Category_ID);
                if (getproductlocation != null)
                {
                    return StatusCode(StatusCodes.Status200OK, new { Status = StatusCodes.Status200OK, Message = "Success", Result = getproductlocation });
                }
                else
                    return StatusCode(StatusCodes.Status404NotFound, new { Status = StatusCodes.Status404NotFound, Message = "Failed", Result = "Product ID Not Found" });
            }
        }
        [HttpGet]
        [Route("api/[controller]/getallLocations")]
        public IActionResult getAllLocations()
        {
            var locations_list = _dbcontext.Product_Locations.ToList();
            if(locations_list != null)
            {
                return Ok(new { Status = StatusCodes.Status200OK, Message = "Success", Result = locations_list });
            }
            else
            {
                return BadRequest(new { Status = StatusCodes.Status400BadRequest, Message = "Failed", Result = "Category List not avaialable" });
            }
        }
    }
}
