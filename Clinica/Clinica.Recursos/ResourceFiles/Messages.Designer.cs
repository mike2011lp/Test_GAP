﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Clinica.Recursos.ResourceFiles {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Clinica.Recursos.ResourceFiles.Messages", typeof(Messages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user name or password is incorrect..
        /// </summary>
        public static string MSG_AUTH_INVALID_CREDENTIALS {
            get {
                return ResourceManager.GetString("MSG_AUTH_INVALID_CREDENTIALS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No se puede crear el usuario..
        /// </summary>
        public static string MSG_DB_ERR_AUTH_CANNOT_CREATE {
            get {
                return ResourceManager.GetString("MSG_DB_ERR_AUTH_CANNOT_CREATE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No se puede borrar el usuario..
        /// </summary>
        public static string MSG_DB_ERR_AUTH_CANNOT_DEL {
            get {
                return ResourceManager.GetString("MSG_DB_ERR_AUTH_CANNOT_DEL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No se puede retornar el token..
        /// </summary>
        public static string MSG_DB_ERR_AUTH_CANNOT_RET_TOKEN {
            get {
                return ResourceManager.GetString("MSG_DB_ERR_AUTH_CANNOT_RET_TOKEN", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No se puede actualizar el usuario..
        /// </summary>
        public static string MSG_DB_ERR_AUTH_CANNOT_UPD {
            get {
                return ResourceManager.GetString("MSG_DB_ERR_AUTH_CANNOT_UPD", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Información inválida..
        /// </summary>
        public static string MSG_DB_ERR_AUTH_NOT_VALID_INFO {
            get {
                return ResourceManager.GetString("MSG_DB_ERR_AUTH_NOT_VALID_INFO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El usuario no existe.
        /// </summary>
        public static string MSG_DB_ERR_AUTH_USER_DOES_NOT_EXISTS {
            get {
                return ResourceManager.GetString("MSG_DB_ERR_AUTH_USER_DOES_NOT_EXISTS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Credenciales incorrectas..
        /// </summary>
        public static string MSG_DB_ERR_AUTH_WRONG_CRED {
            get {
                return ResourceManager.GetString("MSG_DB_ERR_AUTH_WRONG_CRED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to La entidad es nula {0}. Información adicional {1}..
        /// </summary>
        public static string MSG_DB_ERR_ENTITY_NULL {
            get {
                return ResourceManager.GetString("MSG_DB_ERR_ENTITY_NULL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to La información ingresada es nula..
        /// </summary>
        public static string MSG_DB_ERR_INPUT_NULL {
            get {
                return ResourceManager.GetString("MSG_DB_ERR_INPUT_NULL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Los datos de la solicitud no son correctos. Información adicional {0}..
        /// </summary>
        public static string MSG_DB_ERR_MODEL_VALIDATION {
            get {
                return ResourceManager.GetString("MSG_DB_ERR_MODEL_VALIDATION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El parámetro no existe..
        /// </summary>
        public static string MSG_DB_ERR_PARAM_DOES_NOT_EXISTS {
            get {
                return ResourceManager.GetString("MSG_DB_ERR_PARAM_DOES_NOT_EXISTS", resourceCulture);
            }
        }
    }
}
