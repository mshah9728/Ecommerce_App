using EcomApp.Models.ResponseModel;
using System;
using System.Threading.Tasks;

namespace EcomApp.Services.Authorization
{
    public interface IAuthUserService
    {
        Task<bool> getidfromtoken();
        string storeuserid();
    }
}
