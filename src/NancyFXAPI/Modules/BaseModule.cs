using Nancy;
using NancyFXAPI.Infrastructure;
using System;
using NewRelicAgent = NewRelic.Api.Agent.NewRelic;

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

            Before += ctx =>
            {
                var routeDescription = ctx.ResolvedRoute.Description;
                NewRelicAgent.SetTransactionName("Custom/Endpoint", string.Format("{0} {1}", routeDescription.Method, routeDescription.Path));

                return null;
            };
        }
    }
}
