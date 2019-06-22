using Nancy;

namespace NancyFXAPI.Modules
{
    public class SegundoModulo : NancyModule
    {
        public SegundoModulo() : base("/segundo")
        {
            Get("/", parans => "Hello Segundo Módulo Nancy");
        }
    }
}
