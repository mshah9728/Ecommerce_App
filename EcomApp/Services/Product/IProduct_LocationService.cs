using EcomApp.Models.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcomApp.Services.Product
{
    public interface IProduct_LocationService
    {
        Task<List<Product_LocationModel>> getAllProductLocation();
        Task<Product_LocationModel> getProductLocationByID(Guid ProductLocationId);
    }
}
