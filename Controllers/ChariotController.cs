using FirstControllerProject.Models;
using FirstControllerProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace FirstControllerProject.Controllers
{
    public class ChariotController : Controller
    {
        private readonly ICommonServicescs<Chariot> _chariotServiceService;

        public ChariotController(ICommonServicescs<Chariot> chariotService)
        {
            _chariotServiceService = chariotService;
        }
        public IActionResult Index(string search, int pageSize = 5, int pageNumber = 1)
        {
            var chariots = _chariotServiceService.GetAll();
            var curchariots = _chariotServiceService.GetPageSized(pageSize, pageNumber);
            if (!string.IsNullOrWhiteSpace(search))
            {
                chariots = chariots.Where(x => x.MemberName.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            ViewBag.CurrentPage = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling(chariots.Count() / (double)pageSize);
            ViewBag.Search = search;
            return View(curchariots);
        }

        public IActionResult Details(int Id)
        {
            var Chariot = _chariotServiceService.GetById(Id);
            if (Chariot == null) return NotFound();
            return View(Chariot);
        }
        public IActionResult AddEdit()
        {
            var model = new List<Chariot> { new Chariot() }; // start with 1 empty row
            return View("AddEdit", model);
        }
        [HttpPost]
        public IActionResult Create([FromBody] List<Chariot> Chariot)
        {
            var allChariot = _chariotServiceService.Add(Chariot);
            return RedirectToAction("Index", allChariot);
        }
        public IActionResult EditData(int id)
        {
            var allChariot = _chariotServiceService.Edit(id);
            return View("Edit", allChariot);
        }
        [HttpPost]
        public IActionResult UpdateData(Chariot Chariot)
        {
            var allChariot = _chariotServiceService.Update(Chariot);
            return RedirectToAction("Index", allChariot);
        }
        public IActionResult DeleteRecord(int id)
        {
            var allChariot = _chariotServiceService.Delete(id);
            if (allChariot)
                return RedirectToAction("Index");
            return null;
        }
    }
}
