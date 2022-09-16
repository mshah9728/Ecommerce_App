using EcomApp.Configuration.CacheMemory;
using EcomApp.Configuration.JsonConfig;
using EcomApp.Models.ResponseModel;
using EcomApp.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EcomApp.Services.Authorization
{
    public class AuthUserService : IAuthUserService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICacheMemory _cacheMemory;
        private readonly Ijson _json;
        private readonly IConfiguration _configuration;
        public static JsonResponse_Model dresult;
        public AuthUserService(IHttpContextAccessor httpContext, UserManager<ApplicationUser> userManager, Ijson ijson, ICacheMemory cacheMemory, IConfiguration configuration)
        {
            _httpContext = httpContext;
            _userManager = userManager;
            _cacheMemory = cacheMemory;
            _json = ijson;
            _configuration = configuration;
        }
        public async Task<bool> getidfromtoken()
        {
            var useridfromtoken = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (useridfromtoken == null)
            {
                throw new ArgumentNullException(nameof(useridfromtoken));
            }
            else
            {
                var get_authorizeduser = await _userManager.FindByIdAsync(useridfromtoken);
                if (get_authorizeduser == null)
                {
                    return false;
                }
                else
                {
                    if (useridfromtoken == get_authorizeduser.Id.ToString())
                    {

                        return Task.CompletedTask.IsCompletedSuccessfully;
                    }
                    else
                    {
                        return Task.CompletedTask.IsCanceled;
                    }
                }
            }
        }
        public string storeuserid()
        {
            var result = getidfromtoken();
            if (result.Result == false)
            {
                throw new Exception("User is not valid");
            }
            else
            {
                var authorizeduserid = string.Empty;
                dynamic usersdata = _cacheMemory.getDatafromKey(_configuration["CacheKey:User"]);
                if (usersdata == null)
                {
                    throw new Exception("No user data found in cache");
                }
                else
                {
                    var useridfromtoken = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    var userlist = usersdata.Result;
                    if (useridfromtoken != null && usersdata != null)
                    {
                        foreach(var user in userlist)
                        {
                            if (useridfromtoken == user.Id.ToString())
                            {
                                authorizeduserid = user.Id;
                                break;
                            }
                            
                        }
                        return authorizeduserid;
                    }
                    else
                    {
                        throw new Exception("User not found or not registered");
                    }
                }
            }
        }
    }
}
