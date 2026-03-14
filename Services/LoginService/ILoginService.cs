using System;
using FirstControllerProject.Models;
namespace FirstControllerProject.Services
{
    public interface ILoginService
    {
        LoginCheck ValidUser(string userName,string password);
    }   
}