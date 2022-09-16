using EcomApp.Configuration.SQLConfiguration;
using EcomApp.DBConfigurations.MongoConfiguration;
using EcomApp.Models.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace EcomApp.Services.User
{
    public class UserService: IUserService
    {
        private readonly MongoDBContext _mDbcontext;
        public UserService(MongoDBContext dBContext)
        {
            _mDbcontext = dBContext;
        }
        public  async Task<List<ApplicationUser>> getalluser()
        {
            return await _mDbcontext.users.Find(_ => true).ToListAsync();
        }
        public async Task<ApplicationUser> getuserByID(Guid useriD)
        {
            return await _mDbcontext.users.Find(s => s.Id == useriD).FirstOrDefaultAsync();
        }
        public async void storeuser(ApplicationUser model)
        {
            await _mDbcontext.users.InsertOneAsync(model);
        }
        public async Task<bool> updateuser(Guid Userid, ApplicationUser usermobject)
        {
            if(Userid.Equals(Guid.Empty) && usermobject == null)
            {
                throw new Exception("Userid and user data is not entered");
            }
            else
            {
                var result =await _mDbcontext.users.ReplaceOneAsync(s => s.Id == Userid, usermobject);
                if(result.ModifiedCount > 0)
                {
                    return true;
                }
                else
                {
                    throw new Exception("user data is not modified");
                }
            }
        }
        public async Task<bool> deleteuser(Guid Userid)
        {
            var userdata = getuserByID(Userid);
            if(userdata != null)
            {
                var result = await _mDbcontext.users.DeleteOneAsync(s => s.Id == Userid);
                return true;
            }
            else
            {
                throw new Exception("User id not valid ");
            }
        }
    }
}
