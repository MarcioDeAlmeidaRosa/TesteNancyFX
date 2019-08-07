using NancyFXAPI.Domain;

namespace NancyFXAPI.Repository.Contracts
{
    public interface IRepository<T> where T : class
    {
        T[] GetAll();
        T GetById(long id);
        void SaveOrUpdate(long id, T data);
        void Delete(long id);
    }
}
