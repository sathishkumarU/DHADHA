using FirstControllerProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;
namespace FirstControllerProject.Controllers.BaseController
{
    public class MasterController<TEntity> : Controller where TEntity : class , new()
    {
        private readonly ICommonServicescs<TEntity> _CommonService;
        private readonly ILogger _logger;
        public MasterController(ICommonServicescs<TEntity> EntityService, ILogger logger)
        {
            _CommonService = EntityService;
            _logger = logger;
        }
        public IActionResult Index(string search, int pageSize = 5, int pageNumber = 1)
        {
            var CommonService = _CommonService.GetAll();
            string[] words = { "eat", "tea", "tan", "ate", "nat", "bat" };
            GroupAnagrams(words);
            DateTime DD = new DateTime(1998, 6, 1);
            ExtensionsMethods.getAge(DD);
            var studentId = GetEnumAttributes(typeof(StudentsTypes), StudentsTypes.StudentA).value;

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

        public IActionResult Details(int Id)
        {
            var Student = _CommonService.GetById(Id);
            if (Student == null) return NotFound();
            return View(Student);
        }
        public IActionResult AddEdit()
        {
            var model = new List<TEntity>() { new TEntity()}; // creates a new instance of TEntity
            return View("AddEdit", model);
        }
        [HttpPost]
        public IActionResult Create([FromBody] List<TEntity> entity)
        {
            var allService = _CommonService.Add(entity);
            return RedirectToAction("Index");
        }
        public IActionResult EditData(int id)
        {
            var allService = _CommonService.Edit(id);
            return View("Edit", allService);
        }
        [HttpPost]
        public IActionResult UpdateData(TEntity entity)
        {
            var updateService = _CommonService.Update(entity);
            return RedirectToAction("Index", updateService);
        }
        public IActionResult DeleteRecord(int id)
        {
            var allService= _CommonService.Delete(id);
            if (allService)
                return RedirectToAction("Index");
            return null;
        }
        public static IList<IList<string>> GroupAnagrams(string[] strs)
        {
            var map = new Dictionary<string, List<string>>();

            foreach (string word in strs)
            {
                int[] count = new int[26]; // assume lowercase a-z
                foreach (char c in word)
                    count[c - 'a']++;

                // create a unique key based on letter counts
                string key = string.Join("#", count);

                if (!map.ContainsKey(key))
                    map[key] = new List<string>();

                map[key].Add(word);
            }

            return map.Values.ToList<IList<string>>();
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
    public enum StudentsTypes
    {
        [EnumMember(Value = "Student A")]
        StudentA = 1,
        [EnumMember(Value = "Student B")]
        StudentB = 2
    }
    [Flags]
    public enum Permissions
    {
        Create = 1,
        Update = 2,
        View = 3,
        Delete = 4,
    }
    [Obsolete ("Use StudentsTypes instead of OldStudentsTypes",true)]
    public enum OldStudentsTypes
    {
        [EnumMember(Value = "Student A")]
        StudentA = 1,
        [EnumMember(Value = "Student B")]
        StudentB = 2
    }
    public static class ExtensionsMethods
    {
        public static int getAge(this DateTime Birthdate)
        {
            var today = DateTime.Today;
            int age = Birthdate.Year - today.Year;
            var DD = today.AddYears(-age);
            return age; 
        }
    }

}
