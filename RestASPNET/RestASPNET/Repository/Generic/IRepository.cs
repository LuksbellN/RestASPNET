using RestASPNET.Model;
using RestASPNET.Model.Base;

namespace RestASPNET.Repository.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);
        T FindById(int id);
        Task<List<T>> GetAllAsync();
        T Update(T item);
        void Delete(int id);
        bool Exists(int id);
    }
}
