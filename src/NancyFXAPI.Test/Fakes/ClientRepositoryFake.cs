using FizzWare.NBuilder;
using NancyFXAPI.Domain;
using NancyFXAPI.Repository.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace NancyFXAPI.Test.Fakes
{
    public class ClientRepositoryFake : IRepository<Client>
    {
        private readonly IList<Client> clients;

        public ClientRepositoryFake()
        {
            clients = Builder<Client>
                .CreateListOfSize(100)
                .All()
                .WithConstructor(() => new Client()
                {
                    ID = Faker.Number.Even(1, 100),
                    Name = Faker.Name.FullName(),
                    Age = Faker.Number.Even(1, 70)
                })
           .Build();
        }

        public void Delete(long id)
        {
            throw new System.NotImplementedException();
        }

        public Client GetById(long id)
        {
            return clients.FirstOrDefault(c => c.ID == id);
        }

        public void SaveOrUpdate(Client data)
        {
            throw new System.NotImplementedException();
        }
    }
}
