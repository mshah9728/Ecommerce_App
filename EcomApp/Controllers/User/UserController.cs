using Abp.Runtime.Security;
using EcomApp.Configuration.CacheMemory;
using EcomApp.Configuration.Json;
using EcomApp.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EcomApp.Controllers.User
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICacheMemory _cacheMemory;
        private readonly IConfiguration _configuration;
        private static string DefaultPassPhrase = "Encrypt Token Strongly";
        private readonly Ijson _json;
        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ICacheMemory cacheMemory, IConfiguration configuration, Ijson ijson)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _cacheMemory = cacheMemory;
            _configuration = configuration;
            _json = ijson;
        }
        [HttpPost]
        [Route("api/[controller]/{version}/account/register")]
        public async Task<IActionResult> UserRegistration([FromBody] UserRegistrationModel userRegistration_model, string version)
        {
            if(userRegistration_model == null && version == "1.0")
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { Status = StatusCodes.Status400BadRequest, Message = "Failed", entity = "null" });
            }
            else
            {
                var user_Exist = await _userManager.FindByEmailAsync(userRegistration_model.Email);
                if (user_Exist != null)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new { status = "Error", Message = "User Already Registered" });
                }
                else
                {
                    ApplicationUser user = new ApplicationUser()
                    {
                        
                        Email = userRegistration_model.Email,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        UserName = userRegistration_model.username,
                        PhoneNumber = "00000",
                        LockoutEnabled = true,
                        isActive = true,
                        isLocked = false,
                        isDeleted = false,
                        ActivationCode = Guid.NewGuid(),
                        PasswordResetCode = Guid.NewGuid(),
                        IsPasswordConfirmed = false,
                        CreationDateTime = DateTime.Now,
                        RegistrationDateTime = DateTime.Now,
                        CreatorUserId = Guid.Empty,
                        DeletionDateTime = null,
                        DeletionUserId = Guid.Empty,
                        Lastlogin = null,
                        LastmodifiorUserId = Guid.Empty,
                        LastModificationDateTime = null,
                        Version = 1,
                        userdetails = userRegistration_model.userdetails
                       
                    };
                    var usercreationresult = await _userManager.CreateAsync(user, userRegistration_model.Password);
                    if(usercreationresult.Succeeded != false)
                    {
                        var jsonuserdata = JsonConvert.SerializeObject(user);
                        return Ok(new { Status = StatusCodes.Status201Created, Message = "Success", Response = usercreationresult, Users = usercreationresult});
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new { status = "Failed", Message = "User Creation Failed", Error = usercreationresult.Errors });
                    }
                }
            }
        }
        [HttpPost]
        [Route("api/[controller]/{version}/account/authentication")]
        public async Task<IActionResult> Authenticate([FromBody] userLogin_Model login_Model, string version)
        {
            if (ModelState.IsValid && version == "1.0")
            {
                var username = await _userManager.FindByEmailAsync(login_Model.UserEmail);
                var password = await _userManager.CheckPasswordAsync(username, login_Model.Password);
                if (User != null && password != false)
                {
                    if (username.isActive != false)
                    {
                        var userRoles = await _userManager.GetRolesAsync(username);
                        var authclaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, username.Id.ToString()),
                    new Claim(ClaimTypes.Name, username.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.MobilePhone, username.PhoneNumber),
                    new Claim(ClaimTypes.Email,username.Email),
                    new Claim(ClaimTypes.Actor, _configuration["JWT:ValidAudience"])
                };
                        foreach (var roles in userRoles)
                        {
                            authclaims.Add(new Claim(ClaimTypes.Role, roles));
                        }

                        var authsigingkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secert"]));
                        var token = new JwtSecurityToken(
                            issuer: _configuration["JWT:ValidIssuer"],
                            audience: _configuration["JWT:ValidAudience"],
                            expires: DateTime.Now.AddDays(1),
                            claims: authclaims,
                            signingCredentials: new SigningCredentials(authsigingkey, SecurityAlgorithms.HmacSha256Signature)
                            );
                        var stringtoken = token.ToString();
                        var Payload = JsonConvert.SerializeObject(new { Payload = login_Model });
                        var url = HttpContext.Request.GetDisplayUrl();
                        
                        //var json = JsonConvert.SerializeObject(await _dataCoreService.ipaddress(ClientIPAddr));
                        if (username.Lastlogin == null || username.Lastlogin != null)
                        {
                            username.Lastlogin = DateTime.Now;
                            await _userManager.UpdateAsync(username);
                        }

                        var generateToken = new JwtSecurityTokenHandler().WriteToken(token);
                        var encrptytoken = GetEncryptedAccessToken(stringtoken);
                        var LoggedinData = _json.JsonSerializer(username);
                        _cacheMemory.SavedCache("userLoggedinData", LoggedinData);
                        //await _activityLogsService.storeactivityinDB(5, username.Id, UserEnums.User_Logged_In.ToString(), url, Payload, methodType, "", json, ClientIPAddr, "Authentication");
                        //_logger.LogInfo($"User loggedin  with Email {login_Model.User_Email}, Success, Post Request");
                        return Ok(
                            new
                            {
                                Message = "Success",
                                status = StatusCodes.Status200OK,
                                entity = new
                                {
                                    userid = username.Id,
                                    user = username,
                                    token_type = "Bearer",
                                    token = generateToken.ToString(),
                                    Encrypted_token = encrptytoken.ToString()
                                }

                            });
                    }
                    else
                        return StatusCode(StatusCodes.Status403Forbidden, new { Message = "Failed", Status = StatusCodes.Status403Forbidden, Error = "User Deactivated or Deleted...! Please Contact to your Administrator" });
                }

                return Unauthorized(new { Message = "Failed", status = StatusCodes.Status401Unauthorized, Error = "Email and Password is Invalid..! Enter Valid Crendentials" });
            }
            return BadRequest(new { Message = "Failed", status = StatusCodes.Status400BadRequest, Error = "Email and Password is Invalid..! Enter Valid Crendentials" });
        }
        private string GetEncryptedAccessToken(string accessToken)
        {
            return SimpleStringCipher.Instance.Encrypt(accessToken, DefaultPassPhrase);
        }
    }
}
