using Nancy;
using Nancy.Testing;
using NancyFXAPI.Modules;
using System.Threading.Tasks;
using Xunit;

namespace NancyFXAPI.Test
{
    public class HelloNancyModuleUnitTest
    {
        [Fact]
        public async Task GetPrimeiroTest()
        {
            var browser = new Browser(with => with.Module(new HelloNancyModule()));
            var response = await browser.Get("/nancy", (with) =>
            {
                with.Header("Authorization", "Bearer johnsmith");
                with.Header("Accept", "application/json");
                with.HttpRequest();
            });
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
