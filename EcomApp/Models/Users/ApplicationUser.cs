using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using System;
using System.Collections.Generic;

namespace EcomApp.Models.Users
{
    [CollectionName("Users")]
    public class ApplicationUser : MongoIdentityUser<Guid>
    {
        public bool isAdmin { get; set; }
        public bool isActive { get; set; }
        public bool isLocked { get; set; }
        public bool isDeleted { get; set; }
        public Guid ActivationCode { get; set; }
        public DateTime? RegistrationDateTime { get; set; }
        public DateTime? Lastlogin { get;set; }
        public Guid PasswordResetCode { get; set; }
        public bool IsPasswordConfirmed { get; set; }
        public IList<User_details> userdetails { get; set; }
        public Guid CreatorUserId { get; set; }
        public DateTime? CreationDateTime { get; set; }
        public Guid? DeletionUserId { get; set; }
        public DateTime? DeletionDateTime { get; set; }
        public Guid? LastmodifiorUserId { get; set; }
        public DateTime? LastModificationDateTime { get; set; }

    }
    public enum UserEnums
    {
        New_User_Registered = 1,
        Update_User = 2,
        Delete_User = 3,
        Confirmed_User_Email = 4,
        User_Logged_In = 5,
        User_Logged_out = 6,
        Forget_Password = 7,
        Reset_Password = 8,
        Locked_Account = 9,
        Unlocked_Account = 10,
    }
}
