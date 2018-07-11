namespace Clinica.Services.Handlers
{
    using Clinica.Services.Enums;

    /// <summary>
    /// Manejo de errores de base de datos
    /// </summary>
    public interface IErrorHandler
    {
        string GetMessage(MensajesErrorEnum message);
    }
}
