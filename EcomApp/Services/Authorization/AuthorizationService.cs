using EcomApp.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EcomApp.Services.Authorization
{
    public class AuthorizationService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthorizationService(IHttpContextAccessor httpContext, UserManager<ApplicationUser> userManager)
        {
            _httpContext = httpContext;
            _userManager = userManager;
        }
        public async Task<bool> getidfromtoken()
        {
            var useridfromtoken = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(useridfromtoken == null)
            {
               throw new ArgumentNullException(nameof(useridfromtoken));
            }
            else
            {
                var get_authorizeduser = await _userManager.FindByIdAsync(useridfromtoken);
                
                if(get_authorizeduser == null)
                {
                    return false;
                }
                else
                {
                    if(useridfromtoken == get_authorizeduser.Id.ToString())
                    {
                        return Task.CompletedTask.IsCompletedSuccessfully;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}
