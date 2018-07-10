namespace Clinica.DataAccess.Migrations
{
    using Clinica.DataAccess.Context;
    using Clinica.Extensiones.Main;
    using Clinica.DataAccess.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Manejo de seeding del proyecto
    /// </summary>
    class DataSeeder
    {
        #region Fields
        private DataContext myContext;
        #endregion

        #region Construction
        public DataSeeder(DataContext dataContext)
        {
            this.myContext = dataContext;
        }
        #endregion

        #region Seed Info
        //Informacion inicial de parámetros
        static IList<Parametro> ParametrosIniciales = new List<Parametro>
        {
            //Tipos de identificacion
            new Parametro { Categoria = "TIPO_ID", Codigo = "TI_CEDULA", ValorPrincipal = "CC", ValorSecundario = "Cedula de Ciudadania" },
            new Parametro { Categoria = "TIPO_ID", Codigo = "TI_TAR_ID", ValorPrincipal = "TI", ValorSecundario = "Tarjeta de Identidad" },
            //Tipo de Usuario
            new Parametro { Categoria = "TIPO_USUARIO", Codigo = "TU_PACIENTE", ValorPrincipal = "PC", ValorSecundario = "Paciente" },
            new Parametro { Categoria = "TIPO_USUARIO", Codigo = "TU_MEDICO", ValorPrincipal = "MD", ValorSecundario = "Medico" },
            //Estados Usuario
            new Parametro { Categoria = "ESTADO_USUARIO", Codigo = "EU_ACTIVO", ValorPrincipal = "A", ValorSecundario = "Activo" },
            new Parametro { Categoria = "ESTADO_USUARIO", Codigo = "EU_INACTIVO", ValorPrincipal = "I", ValorSecundario = "Inactivo" },
            //Estados Cita
            new Parametro { Categoria = "ESTADO_CITA", Codigo = "EC_ASIGNADA", ValorPrincipal = "A", ValorSecundario = "Asignada" },
            new Parametro { Categoria = "ESTADO_CITA", Codigo = "EC_CANCELADA", ValorPrincipal = "C", ValorSecundario = "Cancelada" },
            new Parametro { Categoria = "ESTADO_CITA", Codigo = "EC_ATENDIDA", ValorPrincipal = "T", ValorSecundario = "Atendida" }
        };

        //Información inicial de roles
        static IList<Rol> RolesIniciales = new List<Rol>
        {
            new Rol { Codigo = "ROL_ADMIN", Description = "Rol Admin" },
            new Rol { Codigo = "ROL_PC", Description = "Rol Paciente" },
            new Rol { Codigo = "ROL_MD", Description = "Rol Medico" }
        };

        //Información inicial de usuarios
        static IList<Usuario> UsuariosIniciales = new List<Usuario>
        {
            new Usuario {
                Id = 1,
                TipoIdentificacion = new Parametro { Categoria = "TIPO_ID", Codigo = "TI_CEDULA" },
                NumeroIdentificacion = "1085285379",
                Nombres = "Miguel",
                Apellidos = "Lopez",
                Contrasena = "",
                Roles = new List<Rol> { new Rol { Codigo = "ROL_ADMIN" }  },
                Estado = new Parametro { Categoria = "ESTADO_USUARIO", Codigo = "EU_ACTIVO" }
            }
        };

        #endregion

        /// <summary>
        /// Proceso de Seeding
        /// </summary>
        public void Seed()
        {
            //Seed de Parámetros
            this.ParametrosSeed();

            //Seed de Roles
            this.RolesSeed();

            //Seed de Usuarios
            this.UsuariosSeed();

            //Seed de Citas Usuario
            //this.CitasSeed();
        }

        /// <summary>
        /// Seed para la tabla Parametros
        /// </summary>
        private void ParametrosSeed()
        {
            //Solo efectuar Seed si los datos del contexto para la tabla están vacios
            if (this.myContext.Parametros.IsEmpty())
            {
                //Asignar información inicial de parámetros
                this.myContext.Parametros.AddRange(ParametrosIniciales);

                //Guardar cambios
                this.myContext.SaveChanges();
            }
        }

        /// <summary>
        /// Seed para la tabla Roles
        /// </summary>
        private void RolesSeed()
        {
            //Solo efectuar Seed si los datos del contexto para la tabla están vacios
            if (this.myContext.Roles.IsEmpty())
            {
                //Roles iniciales
                this.myContext.Roles.AddRange(RolesIniciales);

                //Guardar cambios
                this.myContext.SaveChanges();
            }
        }

        /// <summary>
        /// Seed para la tabla Usuarios
        /// </summary>
        private void UsuariosSeed()
        {
            //Solo efectuar Seed si los datos del contexto para la tabla están vacios
            if (this.myContext.Usuarios.IsEmpty())
            {
                //Roles iniciales
                this.myContext.Usuarios.AddRange(UsuariosIniciales);

                //Guardar cambios
                this.myContext.SaveChanges();
            }
        }

        /// <summary>
        /// Seed para la tabla Cita
        /// </summary>
        private void CitasSeed()
        {

        }
    }
}
