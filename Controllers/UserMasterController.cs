using FirstControllerProject.Controllers.BaseController;
using FirstControllerProject.Models;
using FirstControllerProject.Services;
using Microsoft.AspNetCore.Mvc;
using FirstControllerProject.Data;

namespace FirstControllerProject.Controllers
{
    public class UserMaster : Controller
    {
        private readonly IUserMaster userMaster;
        public UserMaster(IUserMaster _userMaster)
        {
            userMaster = _userMaster;
        }
        public IActionResult ListAll()
        {
            var Userlist = userMaster.ListAll();
            return View("ListAll",Userlist);
        }
    }
}
