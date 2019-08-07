using System;

namespace NancyFXAPI.Repository.Infrastructure.Exceptions
{
    public class InternalServerErrorException : Exception
    {
        private readonly string _error;

        public InternalServerErrorException(string error) : base(error)
        {
            _error = error;
        }

        public string GetError()
        {
            return _error;
        }
    }
}
