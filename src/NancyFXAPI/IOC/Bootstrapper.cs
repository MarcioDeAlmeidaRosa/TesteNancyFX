using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using NancyFXAPI.Domain;
using NancyFXAPI.Repository;
using NancyFXAPI.Repository.Contracts;

namespace NancyFXAPI.IOC
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            pipelines
                .AfterRequest
                .AddItemToEndOfPipeline(ctx => ctx.Response
                    .WithHeader("Access-Control-Allow-Origin", "*")
                    .WithHeader("Access-Control-Allow-Methods", "POST,GET,PUT,DELETE,OPTIONS")
                    .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type, X-CSRF-Token, X-Requested-With, api_key, Authorization"));

            base.ApplicationStartup(container, pipelines);

            container.Register<IRepository<Client>>(new ClientRepository());
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
        }
    }
}
