
using EcomApp.Configuration.CacheMemory;
using EcomApp.Configuration.SQLConfiguration;
using EcomApp.Models.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace EcomApp.Services.Product
{
    public class Product_LocationService : IProduct_LocationService
    {
        private readonly ICacheMemory _cacheMemory;
        private readonly PosAppDB _dbcontext;
        public IConfiguration configuration { get; }
        public Product_LocationService(ICacheMemory cacheMemory, PosAppDB dbcontext)
        {
            _cacheMemory = cacheMemory;
            _dbcontext = dbcontext;
        }
        public async Task<List<Product_LocationModel>> getAllProductLocation()
        {
            var authorizeduser = _cacheMemory.getDatafromKey(configuration["CacheKey:User"]);
            if(authorizeduser == null)
            {
                return null;
            }
            else
            {
                var locationlist = await _dbcontext.Product_Locations.ToListAsync();
                if (locationlist == null)
                {
                    return null;
                }
                else
                {
                    return locationlist;
                }
            }
        }
        public async Task<Product_LocationModel> getProductLocationByID(Guid ProductLocationId)
        {
            var authorizeduser = _cacheMemory.getDatafromKey(configuration["CacheKey:User"]);
            if(authorizeduser == null)
            {
                return null;
            }
            else
            {
                var productlocation = await _dbcontext.Product_Locations.FindAsync(ProductLocationId);
                if(productlocation == null) { return null; }
                else
                {
                    return productlocation;
                }
            }
        }
    }
}
