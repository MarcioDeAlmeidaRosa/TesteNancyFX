using Nancy;

namespace NancyFXAPI.Modules
{
    public class HelloNancyModule : NancyModule
    {
        public HelloNancyModule() : base("/nancy")
        {
            Get("/", parans => "Hello Nancy");
        }
    }
}
