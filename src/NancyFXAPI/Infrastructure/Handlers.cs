using Nancy;
using Nancy.IO;
using NancyFXAPI.Extensions;
using NancyFXAPI.Repository.Infrastructure.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NancyFXAPI.Infrastructure
{
    public class ErrorHandler
    {
        private readonly IDictionary<Type, Func<Exception, dynamic>> _mappedException = new Dictionary<Type, Func<Exception, dynamic>>
        {
            {
                typeof(ResourceNotFoundException), exception => new
                {
                    Messages = exception.Message,
                    HttpStatusCode = HttpStatusCode.NotFound
                }
            },
            //{
            //    typeof(ResourceValidationException), exception => new
            //    {
            //        Messages = exception.Message,
            //        HttpStatusCode = HttpStatusCode.UnprocessableEntity
            //    }
            //},
            //{
            //    typeof(ModelValidationException), exception => new
            //    {
            //        Messages = ((ModelValidationException)exception).GetErrors(),
            //        HttpStatusCode = HttpStatusCode.UnprocessableEntity
            //    }
            //},
            {
                typeof(Nancy.ModelBinding.ModelBindingException), exception => new
                {
                    Messages = new List<string> { exception.Message },
                    HttpStatusCode = HttpStatusCode.BadRequest
                }
            },
            //{
            //    typeof(Infrastructure.Exceptions.HeaderMissingException), exception => new
            //    {
            //        Messages = new List<string> { exception.Message },
            //        HttpStatusCode = HttpStatusCode.BadRequest
            //    }
            //},
            {
                typeof(InternalServerErrorException), exception => new
                {
                    Messages = ((InternalServerErrorException)exception).GetError(),
                    HttpStatusCode = HttpStatusCode.InternalServerError
                }
            }
        };

        private readonly Guid _traceId;

        public ErrorHandler(Guid traceId)
        {
            _traceId = traceId;
        }



        public virtual Response OnError(NancyContext nancyContext, Exception exception)
        {
            var headers = JsonConvert.SerializeObject(nancyContext.Request.Headers);
            var form = JsonConvert.SerializeObject(nancyContext?.Request?.Body != null ? RequestStream.FromStream(nancyContext.Request.Body).AsString() : string.Empty);
            var method = JsonConvert.SerializeObject(nancyContext.Request.Method);
            var url = JsonConvert.SerializeObject(nancyContext.Request.Url.ToString());

            var requestInfo = new
            {
                Method = method,
                Url = url,
                Headers = headers,
                Form = form
            };

            //Log.Error($"[{_traceId}] {requestInfo}\n Erro: ", exception);

            var typeException = exception.GetType();

            if (!_mappedException.ContainsKey(typeException))
            {
                return CreateResponse(exception.Message, HttpStatusCode.InternalServerError);
            }

            var mappedException = _mappedException[typeException](exception);
            var errorMessages = mappedException.Messages ?? new List<string>();
            return CreateResponse(errorMessages, mappedException.HttpStatusCode);
        }

        private static Response CreateResponse(string error, HttpStatusCode httpStatusCode)
        {
            var jsonArray = Encoding.UTF8.GetBytes(error);

            return new Response
            {
                StatusCode = httpStatusCode,
                ContentType = "application/json; charset=utf-8",
                Contents = stream => stream.Write(jsonArray, 0, jsonArray.Length)
            };
        }

        private static Response CreateResponse(IEnumerable<string> errors, HttpStatusCode httpStatusCode)
        {
            var erros = new { erros = errors };
            var jsonArray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(erros));

            return new Response
            {
                StatusCode = httpStatusCode,
                ContentType = "application/json",
                Contents = stream => stream.Write(jsonArray, 0, jsonArray.Length)
            };
        }
    }
}
