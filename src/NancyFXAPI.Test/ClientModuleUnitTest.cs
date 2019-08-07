using Nancy;
using Nancy.Testing;
using NancyFXAPI.Domain;
using NancyFXAPI.Modules;
using NancyFXAPI.Test.Fakes;
using System.Threading.Tasks;
using Xunit;

namespace NancyFXAPI.Test
{
    public class ClientModuleUnitTest
    {
        private static async Task<BrowserResponse> RecuperarCliente(long id, Browser browser)
        {
            return await browser.Get($"/client/{id}", (with) =>
            {
                with.Header("Authorization", "Bearer johnsmith");
                with.Header("Accept", "application/json");
                with.HttpRequest();
            });
        }

        [Fact]
        public async Task Deve_Retornar_Todos_Clientes_Cadastrados()
        {
            // given
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                with.Dependency<ClientRepositoryFake>();
                with.Module<ClientModule>();
            });

            var browser = new Browser(bootstrapper);
            var response = await browser.Get($"/client", (with) =>
            {
                with.Header("Authorization", "Bearer johnsmith");
                with.Header("Accept", "application/json");
                with.HttpRequest();
            });
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(100, response.Body.DeserializeJson<Client[]>().Length);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(50)]
        [InlineData(89)]
        public async Task Deve_Retornar_Clientes_Pelo_ID_Com_Sucessp(long id)
        {
            // given
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                with.Dependency<ClientRepositoryFake>();
                with.Module<ClientModule>();
            });

            var browser = new Browser(bootstrapper);
            BrowserResponse response = await RecuperarCliente(id, browser);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData(1, 50)]
        [InlineData(2, 25)]
        [InlineData(3, 30)]
        [InlineData(50, 70)]
        [InlineData(89, 56)]
        public async Task Deve_Atualizar_Idade_Cliente_Informado_Com_Idade_Informada_Com_Sucessp(long id, int updateAge)
        {
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                with.Dependency<ClientRepositoryFake>();
                with.Module<ClientModule>();
            });

            var browser = new Browser(bootstrapper);

            BrowserResponse beforeClientResponse = await RecuperarCliente(id, browser);
            var clientBefore = beforeClientResponse.Body.DeserializeJson<Client>();
            clientBefore.Age = updateAge;

            var response = await browser.Put($"/client/{id}", (with) =>
            {
                with.JsonBody(clientBefore);
                with.Header("Authorization", "Bearer johnsmith");
                with.Header("Accept", "application/json");
                with.HttpRequest();
            });

            var clientAfter = response.Body.DeserializeJson<Client>();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(clientBefore.Age, clientAfter.Age);
        }

        [Theory]
        [InlineData(15, "Renato Manco")]
        [InlineData(10, "Xuxa da Silva")]
        [InlineData(18, "Margareth Manes")]
        [InlineData(27, "Iolanda Breda")]
        [InlineData(35, "Manoel Nonato")]
        public async Task Deve_Cadastrar_Novo_Cliente_Com_Sucessp(int age, string nome)
        {
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                with.Dependency<ClientRepositoryFake>();
                with.Module<ClientModule>();
            });

            var browser = new Browser(bootstrapper);

            var newClient = new Client
            {
                Age = age,
                Name = nome
            };

            var response = await browser.Post($"/client", (with) =>
            {
                with.JsonBody(newClient);
                with.Header("Authorization", "Bearer johnsmith");
                with.Header("Accept", "application/json");
                with.HttpRequest();
            });

            var clientAfter = response.Body.DeserializeJson<Client>();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(newClient.Age, clientAfter.Age);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(50)]
        [InlineData(89)]
        public async Task Deve_Remover_Cliente_Informado_Com_Sucessp(long id)
        {
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                with.Dependency<ClientRepositoryFake>();
                with.Module<ClientModule>();
            });
            var browser = new Browser(bootstrapper);
            var response = await browser.Delete($"/client/{id}", (with) =>
            {
                with.Header("Authorization", "Bearer johnsmith");
                with.Header("Accept", "application/json");
                with.HttpRequest();
            });

            BrowserResponse responseClient = await RecuperarCliente(id, browser);

            Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, responseClient.StatusCode);
        }
    }
}
