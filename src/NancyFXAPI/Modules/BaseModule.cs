using Nancy;
using NancyFXAPI.Infrastructure;
using System;

namespace NancyFXAPI.Modules
{
    public class BaseModule : NancyModule
    {
        protected BaseModule() : this(null)
        {

        }

        protected Guid TraceID { get; }

        protected BaseModule(string modulePath) : base(modulePath)
        {
            TraceID = Guid.NewGuid();

            OnError += new ErrorHandler(TraceID).OnError;
        }
    }
}
