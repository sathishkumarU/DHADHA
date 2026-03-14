using FirstControllerProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;
namespace FirstControllerProject.Controllers.BaseController
{
    public class MasterController<TEntity,veEntity> : Controller where TEntity : class , new() where veEntity : class ,new()
    {
        private readonly ICommonServicescs<TEntity,veEntity> _CommonService;
        private readonly ILogger _logger;
        public MasterController(ICommonServicescs<TEntity,veEntity> EntityService, ILogger logger)
        {
            _CommonService = EntityService;
            _logger = logger;
        }
        public virtual IActionResult Index(string search, int pageSize = 5, int pageNumber = 1)
        {
            var CommonService = _CommonService.GetAll();
            
            var curService = _CommonService.GetPageSized(pageSize, pageNumber);
            if (!string.IsNullOrWhiteSpace(search))
            {
                CommonService = CommonService.Where(x =>
                    x.GetType()
                     .GetProperties()
                     .Any(p =>
                     {
                         var value = p.GetValue(x)?.ToString();
                         return !string.IsNullOrEmpty(value) &&
                                value.Contains(search, StringComparison.OrdinalIgnoreCase);
                     })
                ).ToList();
                curService = CommonService;
            }
            ViewBag.CurrentPage = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling(CommonService.Count() / (double)pageSize);
            ViewBag.Search = search;
            return View(curService);
        }

        public virtual IActionResult Details(int Id)
        {
            var Student = _CommonService.GetById(Id);
            if (Student == null) return NotFound();
            return View(Student);
        }
        public virtual IActionResult AddEdit()
        {
            veEntity Model = new veEntity();
            ViewBag.Mode = "Add";
            return View("AddEdit",Model);
        }
        [HttpPost]
        public virtual IActionResult Create([FromBody] TEntity entity)
        {
            var allService = _CommonService._createUpdate(entity);
            return RedirectToAction("ListAll");
         }
        // public virtual IActionResult EditData(int id)
        // {
        //     var allService = _CommonService.Edit(id);
        //     return View("Edit", allService);
        // }
        // [HttpPost]
        // public virtual IActionResult UpdateData(TEntity entity)
        // {
        //     var updateService = _CommonService.Update(entity);
        //     return RedirectToAction("Index", updateService);
        // }
        public virtual IActionResult DeleteRecord(int id)
        {
            var allService= _CommonService.Delete(id);
            if (allService)
                return RedirectToAction("Index");
            return null;
        }
        /// <summary>
        /// To Get Enum Attributes Value....
        /// </summary>
        /// <param name="t"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public (int value,string text) GetEnumAttributes(Type t, object p)
        {
            string enumAttribute = t.GetMember(p.ToString()).FirstOrDefault()
                .GetCustomAttributes(false).OfType<EnumMemberAttribute>()
            .FirstOrDefault().Value;
            int value = (int)Enum.Parse(t, p.ToString());
            return (value, enumAttribute);
        }
    }
}
