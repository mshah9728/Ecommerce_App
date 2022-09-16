using EcomApp.Configuration.CacheMemory;
using EcomApp.Configuration.JsonConfig;
using EcomApp.Models.Users;
using EcomApp.Services.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using IAuthUserService = EcomApp.Services.Authorization.IAuthUserService;

namespace EcomApp.Controllers.Cache
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly ICacheMemory _cacheMemory;
        private readonly Ijson _json;
        private readonly IAuthUserService _authuserservice;
        public CacheController(ICacheMemory cacheMemory, Ijson json, IAuthUserService authuserservice)
        {
            _cacheMemory = cacheMemory;
            _json = json;
            _authuserservice = authuserservice;
        }
        [Route("api/[controller]/{version}/getcachebykey/{Key}")]
        [HttpGet]
        public IActionResult GetCache(string Key, string version)
        {
            if (Key == null && version != "1.0")
            {
                return BadRequest(new { Message = "Failed", Status = StatusCodes.Status400BadRequest, Result = "Key is null Please Provide Cache Key" });
            }
            else
            {
               var authorizeduser = _authuserservice.storeuserid();
                if (!string.IsNullOrEmpty(authorizeduser))
                {
                    var getdata = _cacheMemory.getDatafromKey(Key);

                    if (getdata != null)
                    {
                        return Ok(new { Status = StatusCodes.Status200OK, Message = "Success", Result = getdata.Result });
                    }
                    else
                    {
                        return NotFound(new { Status = StatusCodes.Status404NotFound, Message = "Failed", Result = "Key not Found Please Try Again." });
                    }
                }
                else
                    return StatusCode(StatusCodes.Status401Unauthorized, new { Status = StatusCodes.Status401Unauthorized, Message = "Failed", Result = "Something went wrong" });
            }
        }
        [HttpGet]
        [Route("api/[controller]/{version}/getAllkeysfromcache")]

        public IActionResult getaLLkeysfromcache(string version)
        {
            
            if (version == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { Message = "Failed", Status = StatusCodes.Status403Forbidden, Result = "Please Contact to your Administrator" });
            }
            else
            {
                var authuser = _authuserservice.storeuserid();
                if (authuser != null)
                {
                    var keyslist = _cacheMemory.getAllcachekeyfromDB();
                    if (keyslist != null)
                    {
                        return Ok(new { Message = "Success", Status = StatusCodes.Status200OK, Result = keyslist });
                    }
                    else
                    {
                        return NotFound(new
                        {
                            Message = "Failed",
                            Status = StatusCodes.Status404NotFound,
                            Result = "No Keys in Cache Please try again"
                        });
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized,
                        new
                        {
                            Status = StatusCodes.Status401Unauthorized,
                            Message = "Failed",
                            Result = "You are not Authorized to access"
                        });
                }
               
            }
        }
        [HttpDelete]
        [Route("api/[controller]/{version}/removekeyfromCache/{Key}")]
        public IActionResult removekeyfromCache(string Key, string version)
        {
            if (version == null && Key == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { Status = StatusCodes.Status404NotFound, Message = "Failed", Result = "Parameters not passed correctly" });
            }
            else
            {
                var authuser = _authuserservice.storeuserid();
                if (authuser != null)
                {
                    var keyremoved = _cacheMemory.RemoveDatafromCache(Key);
                    if (keyremoved.IsCompleted == true)
                    {
                        return StatusCode(StatusCodes.Status200OK, new { Status = StatusCodes.Status200OK, Message = "Success", Result = "Cache Removed Successfully" });
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new { Status = StatusCodes.Status400BadRequest, Message = " Failed", Result = "Something went wrong please Contact your Administrator.!" });

                    }
                }
                else
                    return StatusCode(StatusCodes.Status401Unauthorized, new { Status = StatusCodes.Status401Unauthorized, Message ="Failed", Result ="You dont have to permission to access this resource" });
            }
        }
        [HttpPost]
        [Route("")]
        public IActionResult RefreshCache(string key, string version)
        {
            if(key !=null && version != "1.0")
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { Status = StatusCodes.Status403Forbidden, Message = "Failed", Result = "Something Went wrong Please Contact your Administrator.!" });
            }
            else
            {
                var authuser = _authuserservice.storeuserid();
                if(authuser != null)
                {
                    var sendrequestforrefresh = _cacheMemory.RefreshCache(key);
                    if(sendrequestforrefresh.IsCompletedSuccessfully != false)
                    {
                        return StatusCode(StatusCodes.Status200OK, new { Status = StatusCodes.Status200OK, Message = "Success", Result = "Cache is refreshed against the key" });
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new { Status = StatusCodes.Status400BadRequest, Message ="Failed", Result ="Key not found in cache" });
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, new { Status = StatusCodes.Status401Unauthorized, Message = "Failed", Result = "Something Went wrong Please Contact your Administrator.!" });
                }
            }
        }
    }
}
