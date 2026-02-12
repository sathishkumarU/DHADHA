using FirstControllerProject.Models;

namespace FirstControllerProject.Services.BaseBo
{
    public class CommonBaseBO<TEntity> : ICommonServicescs<TEntity>
    {
        private readonly ILogger  _logger;
        protected List<TEntity> _allData;

        public CommonBaseBO(ILogger logger, List<TEntity> allData)
        {
            _logger = logger;
            _allData = allData;
        }
        public virtual IEnumerable<TEntity> GetAll() 
        {
            return _allData;
        }
        public virtual TEntity GetById(int id)
        {
            return _allData.Where(x => {
                var prop = x.GetType().GetProperty("Id");
                if (prop != null)
                {
                    var value = prop.GetValue(x); // get the actual property value
                    return value is int && (int)value == id;
                }
                return false;
            }).FirstOrDefault();
        }
        public virtual IEnumerable<TEntity> Add(IEnumerable<TEntity> Addentity)
        {
            return Addentity;
        }
        public virtual TEntity Edit(int id)
        {
            return GetById(id);
        }
        public virtual TEntity Update(TEntity Entityupdate)
        { 
            return Entityupdate;
        }
        public virtual bool Delete(int id)
        {
            var student = GetById(id);
            if (student != null)
            {
                _allData.Remove(student);
                return true; // deleted successfully
            }
            return false; // not found
        }
        public IEnumerable<TEntity> GetPageSized(int pageSize = 5, int pageCount = 1)
        {
            List<TEntity> curStudnet = new List<TEntity>();
            curStudnet = _allData.OrderBy(x => {
                var prop = x.GetType().GetProperty("Id");
                return prop != null ? (int)prop.GetValue(x) : 0;
            }).Skip((pageCount - 1) * pageSize).Take(pageSize).ToList();

            return curStudnet;
        }
    }
}
