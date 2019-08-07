namespace NancyFXAPI.Modules
{
    public class HelloNancyModule : BaseModule
    {
        public HelloNancyModule() : base("/nancy")
        {
            Get("/", parans => "Hello Nancy");
        }
    }
}
