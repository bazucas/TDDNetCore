using System;
using System.Collections.Generic;
using System.Linq;

namespace CursoOnline.Dominio._Base
{
    public class BaseValidator
    {
        private readonly List<string> _errorMessages;

        private BaseValidator()
        {
            _errorMessages = new List<string>();
        }

        public static BaseValidator New()
        {
            return new BaseValidator();
        }

        public BaseValidator When(bool temErro, string mensagemDeErro)
        {
            if(temErro)
                _errorMessages.Add(mensagemDeErro);

            return this;
        }

        public void TriggersIfExceptionExists()
        {
            if (_errorMessages.Any())
                throw new DomainException(_errorMessages);
        }
    }

    public class DomainException : ArgumentException
    {
        public List<string> ErrorMessages { get; set; }

        public DomainException(List<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }
    }
}
