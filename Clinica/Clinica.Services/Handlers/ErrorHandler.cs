namespace Clinica.Services.Handlers
{
    using Clinica.Services.Enums;
    using System;
    using Clinica.Recursos.ResourceFiles;
    using Microsoft.AspNet.Identity;

    /// <summary>
    /// Implementación de la interfaz de manejo de errores
    /// </summary>
    public class ErrorHandler : IErrorHandler
    {
        public string ErrorIdentityResult(IdentityResult result)
        {
            throw new NotImplementedException();
        }

        public string GetMessage(MensajesErrorEnum message)
        {
            switch (message)
            {
                case MensajesErrorEnum.EntidadNula:
                    return Messages.MSG_DB_ERR_ENTITY_NULL;

                case MensajesErrorEnum.Validacion:
                    return Messages.MSG_DB_ERR_MODEL_VALIDATION;

                case MensajesErrorEnum.UsuarioNoExiste:
                    return Messages.MSG_DB_ERR_AUTH_USER_DOES_NOT_EXISTS;

                case MensajesErrorEnum.CredencialesInvalidas:
                    return Messages.MSG_DB_ERR_AUTH_WRONG_CRED;

                case MensajesErrorEnum.NoAutorizaCreacion:
                    return Messages.MSG_DB_ERR_AUTH_CANNOT_CREATE;

                case MensajesErrorEnum.NoAutorizaBorrado:
                    return Messages.MSG_DB_ERR_AUTH_CANNOT_DEL;

                case MensajesErrorEnum.NoAutorizaActualizacion:
                    return Messages.MSG_DB_ERR_AUTH_CANNOT_UPD;

                case MensajesErrorEnum.InfoInvalida:
                    return Messages.MSG_DB_ERR_AUTH_NOT_VALID_INFO;

                case MensajesErrorEnum.NoAutorizaToken:
                    return Messages.MSG_DB_ERR_AUTH_CANNOT_RET_TOKEN;
                case MensajesErrorEnum.ParametroNoExiste:
                    return Messages.MSG_DB_ERR_PARAM_DOES_NOT_EXISTS;
                default:
                    throw new ArgumentOutOfRangeException(nameof(message), message, null);
            }
        }
    }
}
