namespace Clinica.DataAccess.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using Clinica.Constantes;
    using Clinica.DataAccess.Context;
    using Clinica.DataAccess.Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Linq;
    using Clinica.Extensiones.Main;
    using System.Threading.Tasks;
    using System;

    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

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
            new Parametro { Categoria = "TIPO_USUARIO", Codigo = "TU_NA", ValorPrincipal = "NA", ValorSecundario = "No Aplica" },
            //Estados Usuario
            new Parametro { Categoria = "ESTADO_USUARIO", Codigo = "EU_ACTIVO", ValorPrincipal = "A", ValorSecundario = "Activo" },
            new Parametro { Categoria = "ESTADO_USUARIO", Codigo = "EU_INACTIVO", ValorPrincipal = "I", ValorSecundario = "Inactivo" },
            //Estados Cita
            new Parametro { Categoria = "ESTADO_CITA", Codigo = "EC_ASIGNADA", ValorPrincipal = "A", ValorSecundario = "Asignada" },
            new Parametro { Categoria = "ESTADO_CITA", Codigo = "EC_CANCELADA", ValorPrincipal = "C", ValorSecundario = "Cancelada" },
            new Parametro { Categoria = "ESTADO_CITA", Codigo = "EC_ATENDIDA", ValorPrincipal = "T", ValorSecundario = "Atendida" },
            //Especialidades
            new Parametro { Categoria = "ESPECIALIDAD", Codigo = "ESP_MG", ValorPrincipal = "MG", ValorSecundario = "Medicina General" },
            new Parametro { Categoria = "ESPECIALIDAD", Codigo = "ESP_ODO", ValorPrincipal = "ODO", ValorSecundario = "Odontología" },
            new Parametro { Categoria = "ESPECIALIDAD", Codigo = "ESP_PED", ValorPrincipal = "PED", ValorSecundario = "Pediatría" },
            new Parametro { Categoria = "ESPECIALIDAD", Codigo = "ESP_NEU", ValorPrincipal = "NEU", ValorSecundario = "Neurología" },
        };

        //Información de Ubicaciones (Entidades)
        static IList<Ubicacion> UbicacionesIniciales = new List<Ubicacion>
        {
            new Ubicacion { Descripcion = "Clinica A", Direccion = "Ubicación A" },
            new Ubicacion { Descripcion = "Clinica B", Direccion = "Ubicación B" },
            new Ubicacion { Descripcion = "Clinica C", Direccion = "Ubicación C" }
        };

        //Información inicial de roles
        static string[] RolesIniciales = new string[] { SystemConstants.ROL_ADMIN, SystemConstants.ROL_PACIENTE, SystemConstants.ROL_MEDICO };

        //Información inicial de usuarios
        static IList<Usuario> UsuariosIniciales = new List<Usuario>
        {
            new Usuario {
                Id = Guid.NewGuid().ToString(),
                UserName = "admin",
                TipoIdentificacion = new Parametro { Categoria = "TIPO_ID", Codigo = "TI_CEDULA", ValorPrincipal = "CC" },
                NumeroIdentificacion = "1085285379",
                Nombres = "Miguel",
                Apellidos = "Lopez",
                Estado = new Parametro { Categoria = "ESTADO_USUARIO", Codigo = "EU_ACTIVO", ValorPrincipal = "A" },
                TipoUsuario = new Parametro { Categoria = "TIPO_USUARIO", Codigo = "TU_NA", ValorPrincipal = "NA" }
            }
        };

        #endregion

        protected override void Seed(DataContext context)
        {
            //Seed de Parámetros
            this.ParametrosSeed(context);

            //Seed de Roles
            this.RolesSeed(context);

            //Seed de Usuarios
            this.UsuariosSeed(context);

            //Seed de Ubicaciones
            this.UbicacionesSeed(context);
        }

        /// <summary>
        /// Seed para la tabla Parametros
        /// </summary>
        private void ParametrosSeed(DataContext context)
        {
            //Solo efectuar Seed si los datos del contexto para la tabla están vacios
            if (context.Parametros.IsEmpty())
            {
                //Asignar información inicial de parámetros
                context.Parametros.AddRange(ParametrosIniciales);

                //Guardar cambios
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Seed para la tabla Roles
        /// </summary>
        private void RolesSeed(DataContext context)
        {
            //Solo efectuar Seed si los datos del contexto para la tabla están vacios
            if (context.Roles.IsEmpty())
            {
                //Creación de roles iniciales
                foreach (string rol in RolesIniciales)
                {
                    if (!context.Roles.Any(r => r.Name == rol))
                    {
                        var store = new RoleStore<Rol>(context);
                        var manager = new RoleManager<Rol>(store);
                        var role = new Rol { Name = rol };

                        manager.Create(role);
                    }
                }

                //Guardar cambios
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Seed para la tabla Usuarios
        /// </summary>
        private void UsuariosSeed(DataContext context)
        {
            //Solo efectuar Seed si los datos del contexto para la tabla están vacios
            if (context.Users.IsEmpty())
            {
                //Iterar sobre cada usuario a crear
                foreach (var usuario in UsuariosIniciales)
                {
                    var store = new UserStore<Usuario, Rol, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>(context);
                    var manager = new UserManager<Usuario, string>(store);

                    //manager.Create(usuario, new PasswordHasher().HashPassword("Test_password"));
                    manager.Create(usuario, "Test_password");
                    manager.AddToRole(usuario.Id, SystemConstants.ROL_ADMIN);
                }

                //Guardar cambios
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Seed para la tabla Ubicaciones
        /// </summary>
        private void UbicacionesSeed(DataContext context)
        {
            //Solo efectuar Seed si los datos del contexto para la tabla están vacios
            if (context.Ubicaciones.IsEmpty())
            {
                //Asignar información inicial de parámetros
                context.Ubicaciones.AddRange(UbicacionesIniciales);

                //Guardar cambios
                context.SaveChanges();
            }
        }
    }
}
