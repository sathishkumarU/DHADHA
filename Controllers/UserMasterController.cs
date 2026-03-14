using FirstControllerProject.Controllers.BaseController;
using FirstControllerProject.Models;
using FirstControllerProject.Services;
using Microsoft.AspNetCore.Mvc;
using FirstControllerProject.Data;

namespace FirstControllerProject.Controllers
{
    public class UserMasterController : MasterController<UserMaster,veUserMaster>
    {
        private readonly  ICommonServicescs<UserMaster,veUserMaster> _userMaster;
        private readonly ILogger<UserMasterController> _logger; 

        public UserMasterController(
            ICommonServicescs<UserMaster,veUserMaster>  userMaster,
            ILogger<UserMasterController> logger
        ) : base(userMaster, logger)
        {
            _userMaster = userMaster;
            _logger = logger;
        }
        public IActionResult Edit(int Id)
        {
            var user = _userMaster.GetById(Id);
            return View("AddEdit",user) ;
        }
        public IActionResult View(int Id)
        {
            var user = _userMaster.GetById(Id);
            return View("AddEdit",user) ;
        }
        public IActionResult CreateUpdate(UserMaster Um)
        {
            var UserInsert = _userMaster._createUpdate(Um);
            return View("ListAll");
        }
        public IActionResult ListAll()
        {
            var Userlist = _userMaster.GetAll();
            return View("ListAll",Userlist);
        }
    }
}
