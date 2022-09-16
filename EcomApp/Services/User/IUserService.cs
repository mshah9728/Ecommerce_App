using EcomApp.Models.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcomApp.Services.User
{
    public interface IUserService
    {
        Task<List<ApplicationUser>> getalluser();
        Task<ApplicationUser> getuserByID(Guid useriD);
        void storeuser(ApplicationUser model);
        Task<bool> updateuser(Guid Userid, ApplicationUser usermobject);
        Task<bool> deleteuser(Guid Userid);
    }
}
