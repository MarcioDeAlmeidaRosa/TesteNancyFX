using Nancy;
using Nancy.Testing;
using NancyFXAPI.Modules;
using Xunit;

namespace NancyFXAPI.Test
{
    public class HelloNancyUnitTest
    {
        [Fact]
        public async void SimplestGetTest()
        {
            var browser = new Browser(with => with.Module(new HelloNancy()));
            var response = await browser.Get("/primeiro", (with) =>
            {
                with.Header("Authorization", "Bearer johnsmith");
                with.Header("Accept", "application/json");
                with.HttpRequest();
            });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
