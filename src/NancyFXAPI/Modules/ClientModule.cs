using Nancy;
using NancyFXAPI.Domain;
using NancyFXAPI.Repository.Contracts;

namespace NancyFXAPI.Modules
{
    public class ClientModule : NancyModule
    {
        private readonly IRepository<Client> _repository;

        public ClientModule(IRepository<Client> repository) : this()
        {
            _repository = repository;
        }

        internal ClientModule() : base("/client")
        {
            Get("/{id}", parans =>
            {
                long _id = parans.id;

                var result = _repository.GetById(_id);

                return Response.AsJson(result);
            });
        }
    }
}
