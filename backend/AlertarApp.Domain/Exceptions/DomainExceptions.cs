using System;

namespace AlertarApp.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
        }

        public DomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class EntityNotFoundException : DomainException
    {
        public EntityNotFoundException(string entity, object id) 
            : base($"Entidade '{entity}' com ID '{id}' não foi encontrada")
        {
        }
    }

    public class InvalidPinException : DomainException
    {
        public InvalidPinException() : base("PIN inválido")
        {
        }
    }

    public class UnauthorizedOperationException : DomainException
    {
        public UnauthorizedOperationException(string operation)
            : base($"Operação não autorizada: {operation}")
        {
        }
    }

    public class InvalidLocationException : DomainException
    {
        public InvalidLocationException(string message)
            : base($"Localização inválida: {message}")
        {
        }
    }
}
