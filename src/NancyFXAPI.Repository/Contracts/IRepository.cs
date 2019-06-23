using NancyFXAPI.Domain;

namespace NancyFXAPI.Repository.Contracts
{
    public interface IRepository<T> where T : class
    {
        T GetById(long id);
        void SaveOrUpdate(T data);
        void Delete(long id);
    }
}
