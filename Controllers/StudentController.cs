using Microsoft.AspNetCore.Mvc;
using FirstControllerProject.Models;
using Mono.TextTemplating;
using FirstControllerProject.Services;
using System.Drawing.Printing;
using FirstControllerProject.Controllers.BaseController;
namespace FirstControllerProject.Controllers
{
    public class StudentController : MasterController<StudentDetails>
    {
        private readonly StudentServices _StudentDetails;
        public StudentController(ICommonServicescs<StudentDetails> studentService, ILogger<StudentController> logger, StudentServices studentBo) : base(studentService,logger)
        {
            _StudentDetails = studentBo;
        }
        public IActionResult SearchEmployee(string term)
        {
            var result = _StudentDetails.SearchEmployee(term);


            return new JsonResult(result); 
        }
    }
}
