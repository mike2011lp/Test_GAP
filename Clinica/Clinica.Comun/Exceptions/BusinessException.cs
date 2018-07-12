namespace Clinica.Comun.Exceptions
{
    using System;

    /// <summary>
    /// Clase encargada del manejo de excepciones de negocio
    /// </summary>
    public class BusinessException : Exception
    {
        //Constructors
        public BusinessException() : base() { }
        public BusinessException(string message) : base(message) { }
        public BusinessException(string message, Exception e) : base(message, e) { }
        protected BusinessException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        { }
    }
}
