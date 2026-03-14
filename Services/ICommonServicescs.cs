using System.Collections.Generic;
using FirstControllerProject.Models;

namespace FirstControllerProject.Services
{
    public interface ICommonServicescs<Entity,veEntity>
    {
        List<veEntity> GetAll();
        veEntity GetById(int id);
        string _createUpdate(Entity entity);
        bool Delete(int Id);
        IEnumerable<veEntity> GetPageSized(int pageSize, int pageCount);
    }
}
