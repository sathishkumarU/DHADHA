using System.Collections.Generic;
using FirstControllerProject.Models;

namespace FirstControllerProject.Services
{
    public interface ICommonServicescs<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        IEnumerable<T> Add(IEnumerable<T> entity);
        T Update(T entity);
        T Edit(int Id);
        bool Delete(int Id);
        IEnumerable<T> GetPageSized(int pageSize, int pageCount);
    }
}
