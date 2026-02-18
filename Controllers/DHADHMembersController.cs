// using FirstControllerProject.Controllers.BaseController;
// using FirstControllerProject.Models;
// using FirstControllerProject.Services;
// using Microsoft.AspNetCore.Mvc;
// using FirstControllerProject.Data;

// namespace FirstControllerProject.Controllers
// {
//     public class DHADHMembers : Controller
//     {
//         private readonly DHADHAMemberBO _DhaDhaBo;

//         public DHADHMembers(DHADHAMemberBO DhaDhaBo)
//         {
//             _DhaDhaBo = DhaDhaBo;
//         }
//         public IActionResult Index()
//         {
//             var Record = _DhaDhaBo.GetAll();
//             return View("Index", Record);
//         }
//         public IActionResult AddEdit()
//         {
//             var model = new List<DHADHMembersMaster> { new DHADHMembersMaster() }; // start with 1 empty row
//             return View("AddEdit", model);
//         }
//         public IActionResult Create([FromBody] List<DHADHMembersMaster> rec)
//         {
//           string Dataresult =  _DhaDhaBo._createUpdate(rec);
//           return Json(new { result = Dataresult });
//         }
//         public IActionResult EditData(int id)
//         {
//             var Editdata = _DhaDhaBo.Edit(id);
//             return View("Edit", Editdata);
//         }
//         public IActionResult Update(DHADHMembersMaster data)
//         {
//             var Editdata = _DhaDhaBo.Update(data);
//             return View("Index", Editdata);
//         }
//     }
// }