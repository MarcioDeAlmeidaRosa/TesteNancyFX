using NancyFXAPI.Domain;
using NancyFXAPI.Repository.Contracts;
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
