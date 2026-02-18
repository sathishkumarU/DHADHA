using System;
using FirstControllerProject.Models;
namespace FirstControllerProject.Services
{
    public interface IUserMaster
    {
        List<UserMaster> ListAll();
    }   
}