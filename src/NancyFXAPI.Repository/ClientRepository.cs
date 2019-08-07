using NancyFXAPI.Domain;
using NancyFXAPI.Repository.Contracts;
using NancyFXAPI.Repository.Infrastructure.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace NancyFXAPI.Repository
{
    public class ClientRepository : IRepository<Client>
    {
        private readonly IList<Client> clients = new List<Client>()
        {
            new Client{ ID = 1, Name="Marcio de Almeida Rosa" , Age = 35  },
            new Client{ ID = 2, Name="Xica da Silva a Negra" , Age = 64  },
        };

        public void Delete(long id)
        {
            var cliente = clients.FirstOrDefault(c => c.ID == id);
            if (cliente == null)
            {
                throw new ResourceNotFoundException("Não encontrado registro");
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
                throw new ResourceNotFoundException("Não encontrado registro");
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
