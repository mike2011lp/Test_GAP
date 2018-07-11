namespace Clinica.Services.Handlers
{
    using Clinica.Services.Enums;
    using Microsoft.AspNet.Identity;

    /// <summary>
    /// Manejo de errores de base de datos
    /// </summary>
    public interface IErrorHandler
    {
        string GetMessage(MensajesErrorEnum message);
        string ErrorIdentityResult(IdentityResult result);
    }
}
