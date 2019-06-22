using Nancy;

namespace NancyFXAPI.Modules
{
    public class HelloNancy : NancyModule
    {
        public HelloNancy() : base("/primeiro")
        {
            Get("/", parans => "Hello Nancy");
        }
    }
}
