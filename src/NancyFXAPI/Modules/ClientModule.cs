using Nancy;
using Nancy.ModelBinding;
using NancyFXAPI.Domain;
using NancyFXAPI.Repository.Contracts;

namespace NancyFXAPI.Modules
{
    public class ClientModule : BaseModule
    {
        private readonly IRepository<Client> _repository;

        public ClientModule(IRepository<Client> repository) : this()
        {
            _repository = repository;
        }

        internal ClientModule() : base("/client")
        {
            Get("/", _ => _repository.GetAll());

            Get("/{id}", _ => _repository.GetById(_.id));

            Put("/{id}", _ =>
            {
                var client = this.Bind<Client>();
                _repository.SaveOrUpdate(_.id, client);
                client.ID = _.id;
                return client;
            });

            Post("", _ =>
            {
                var client = this.Bind<Client>();
                _repository.SaveOrUpdate(0, client);
                return client;
            });

            Delete("/{id}", _ =>
            {
                _repository.Delete(_.id);
                return HttpStatusCode.Accepted;
            });
        }
    }
}
