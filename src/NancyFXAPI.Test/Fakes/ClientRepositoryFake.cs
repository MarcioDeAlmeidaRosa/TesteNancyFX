using FizzWare.NBuilder;
using NancyFXAPI.Domain;
using NancyFXAPI.Repository.Contracts;
using System;
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
                    Name = Faker.Name.FullName(),
                    Age = Faker.Number.Even(1, 70)
                })
           .Build();

            clients.Select((value, index) => value.ID = index + 1);
        }

        public void Delete(long id)
        {
            var cliente = clients.FirstOrDefault(c => c.ID == id);
            if (cliente == null)
            {
                throw new Exception("Não encontrado registro");
            }
            clients.Remove(cliente);
        }

        public Client[] GetAll()
        {
            return clients.ToArray();
        }

        public Client GetById(long id)
        {
            return clients.FirstOrDefault(c => c.ID == id) ??
                throw new Exception("Não encontrado registro");
        }

        public void SaveOrUpdate(long id, Client data)
        {
            var cliente = id > 0 ? clients.FirstOrDefault(c => c.ID == id) : null;
            if (cliente != null)
            {
                cliente.Name = data.Name;
                cliente.Age = data.Age;
                return;
            }
            data.ID = clients.Max(c => c.ID) + 1;
            clients.Add(data);
        }
    }
}
