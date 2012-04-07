using System.Collections.Generic;

namespace GiveCRM.Admin.BusinessLogic
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        T Save(T item);
        bool Delete(T item);
        bool DeleteById(int id);
    }
}
