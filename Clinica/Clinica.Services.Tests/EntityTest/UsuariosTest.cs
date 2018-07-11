namespace Clinica.Services.Tests.EntityTest
{
    using AutoMapper;
    using Clinica.DataAccess.Context;
    using Clinica.DataAccess.Entities;
    using Clinica.Services.Mapping;
    using Clinica.Services.Models;
    using Clinica.Services.Repositories;
    using Clinica.Services.Services.Services.Implementation;
    using Clinica.Services.Services.Services.Interfaces;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class UsuariosTest
    {
        #region Fields
        private Mock<IIdentityRepository> myMockRepository { get; }
        private IUsuariosRepository myRepository { get; }
        private DataContext myContext;

        List<Usuario> users = null;

        private UserManager<Usuario, string> myManager
        {
            get
            {
                return new UserManager<Usuario, string>(new UserStore<Usuario>(this.myContext));
            }
        }
        #endregion

        #region Construction
        public UsuariosTest()
        {
            //Crear un nuevo contexto de trabajo con la BD
            this.myContext = new DataContext();

            //Generar el manejador de usuarios
            var store = new UserStore<Usuario, Rol, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>(this.myContext);
            var manager = new UserManager<Usuario, string>(store);

            //Generar nueva instancia del mapper
            var config = new AutoMapperBase().Mapper;

            //Lista de usuarios con los cuales trabajar para las pruebas
            users = new List<Usuario>
            {
                new Usuario
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "paciente",
                    Email = "paciente@test.com",
                    TipoIdentificacion = new Parametro { Categoria = "TIPO_ID", Codigo = "TI_CEDULA", ValorPrincipal = "CC" },
                    NumeroIdentificacion = "123000",
                    Nombres = "Andres",
                    Apellidos = "Cuellar",
                    Estado = new Parametro { Categoria = "ESTADO_USUARIO", Codigo = "EU_ACTIVO", ValorPrincipal = "A" },
                    TipoUsuario = new Parametro { Categoria = "TIPO_USUARIO", Codigo = "TU_NA", ValorPrincipal = "NA" }
                }
            };

            //Configuración Moq
            this.myMockRepository = new Mock<IIdentityRepository>();

            this.myMockRepository.Setup(x => x.GetAll())
                .Returns(users.AsQueryable());

            this.myMockRepository.Setup(x => x.GetByEmail(It.IsAny<string>()))
                .Returns((string email) => users.Find(s => s.Email == email));

            this.myMockRepository.Setup(x => x.Create(It.IsAny<Usuario>(), It.IsAny<string>()))
                .Callback((Usuario user, string password) => users.Add(user));

            this.myMockRepository.Setup(x => x.Update(It.IsAny<Usuario>()))
                .Callback((Usuario user) => users[users.FindIndex(x => x.Id == user.Id)] = user);

            this.myMockRepository.Setup(x => x.Delete(It.IsAny<Usuario>()))
            .Callback((Usuario user) => users.RemoveAt(users.FindIndex(x => x.Id == user.Id)));

            //Generar repositorio de acceso para base de datos
            this.myRepository = new UsuariosRepository(this.myMockRepository.Object, config, new UserValidator<Usuario>(myManager), new PasswordHasher());
        }
        #endregion

        #region Test Methods
        [TestMethod]
        public void GetAll()
        {
            // Act
            var users = this.myRepository.GetAll();

            // Assert
            this.myMockRepository.Verify(x => x.GetAll(), Times.Once);
            Assert.AreEqual(1, users.Count());
        }

        [TestMethod]
        public void InsertUser()
        {
            // Arrange
            var user = new UsuarioModel
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "test_1",
                Email = "paciente_2@test.com",
                TipoIdentificacion = new Parametro { Categoria = "TIPO_ID", Codigo = "TI_CEDULA", ValorPrincipal = "CC" },
                NumeroIdentificacion = "123001",
                Nombres = "Nombre_Test_1",
                Apellidos = "Ape_Test_1",
                Estado = new Parametro { Categoria = "ESTADO_USUARIO", Codigo = "EU_ACTIVO", ValorPrincipal = "A" },
                TipoUsuario = new Parametro { Categoria = "TIPO_USUARIO", Codigo = "TU_NA", ValorPrincipal = "NA" }
            };
            // Act
            this.myRepository.Create(user, "pass_test_1");
            // Assert
            this.myMockRepository.Verify(x => x.Create(It.IsAny<Usuario>(), It.IsAny<string>()), Times.Once);
            var labels = this.myRepository.GetAll();
            Assert.AreEqual(2, labels.Count());
        }

        [TestMethod]
        public void UpdateUser()
        {
            // Arrange
            var user = new UsuarioModel
            {
                Id = this.users[0].Id,
                UserName = "paciente",
                Email = "mod@email.com",
                PhoneNumber = "123456"

            };
            // Act
            this.myRepository.Update(user);
            // Assert
            this.myMockRepository.Verify(x => x.Update(It.IsAny<Usuario>()), Times.Once);
            var items = this.myRepository.GetAll();
            Assert.AreEqual(1, items.Count());
            Assert.AreEqual("123456", items.First().PhoneNumber);
        }

        [TestMethod]
        public void DeleteUser()
        {
            const string email = "paciente@test.com";

            var user = this.myRepository.GetByEmail(email);
            this.myRepository.Delete(user);

            // Assert
            this.myMockRepository.Verify(x => x.GetByEmail(It.IsAny<string>()), Times.Once);
            this.myMockRepository.Verify(x => x.Delete(It.IsAny<Usuario>()), Times.Once);
            Assert.AreEqual(0, this.myRepository.GetAll().Count());
        }
        #endregion
    }
}
