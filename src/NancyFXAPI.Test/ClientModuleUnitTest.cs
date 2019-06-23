using Nancy;
using Nancy.Testing;
using NancyFXAPI.Modules;
using NancyFXAPI.Test.Fakes;
using System.Threading.Tasks;
using Xunit;

namespace NancyFXAPI.Test
{
    public class ClientModuleUnitTest
    {
        [Fact]
        public async Task GetClientByIdTest()
        {
            // given
            var bootstrapper = new ConfigurableBootstrapper(with =>
            {
                with.Dependency<ClientRepositoryFake>();
                with.Module<ClientModule>();
            });

            var browser = new Browser(bootstrapper);
            var response = await browser.Get($"/client/{1}", (with) =>
            {
                with.Header("Authorization", "Bearer johnsmith");
                with.Header("Accept", "application/json");
                with.HttpRequest();
            });
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
