using Microsoft.AspNetCore.Mvc;
using FirstControllerProject.Models;
using FirstControllerProject.Services;
using Microsoft.CodeAnalysis.Elfie.Serialization;
namespace FirstControllerProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _IloginService;
        private readonly ILogger<LoginController> _logger;
        public LoginController(ILogger<LoginController> logger,ILoginService loginService)
        {
            _logger = logger;
            _IloginService = loginService;
        }
        public IActionResult Index()
        {
            return View();
        }
        // POST: Account/Login
        [HttpPost]
        public IActionResult Login(LoginCheck model)
        {
            if (ModelState.IsValid)
            {
               var isValidUser = _IloginService.ValidUser(model.UserId,model.Password);

                if (isValidUser != null)
                {

                    ViewBag.Message = "Login Successful!";
                    return RedirectToAction("ListAll","UserMaster");
                }
                else
                {
                    _logger.LogError("Invalid login attempt for username: {user}", model.UserId);
                    ViewBag.Message = "Invalid username or password";
                }
            }
            return View(model);
        }
    }
}
