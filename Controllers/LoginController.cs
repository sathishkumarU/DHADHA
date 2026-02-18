using Microsoft.AspNetCore.Mvc;
using FirstControllerProject.Models;
namespace FirstControllerProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        // POST: Account/Login
        [HttpPost]
        public IActionResult Login(Login model)
        {
            if (ModelState.IsValid)
            {
                if (model.Username.ToLower() == "admin" && model.Password == "123")
                {
                    ViewBag.Message = "Login Successful!";
                    return RedirectToAction("ListAll","UserMaster");
                }
                else
                {
                    _logger.LogError("Invalid login attempt for username: {user}", model.Username);
                    ViewBag.Message = "Invalid username or password";
                }
            }
            return View(model);
        }
    }
}
