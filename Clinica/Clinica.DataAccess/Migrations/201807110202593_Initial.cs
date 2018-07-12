namespace Clinica.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Citas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Paciente_Id = c.String(nullable: false, maxLength: 128),
                        TipoCita_Id = c.Int(nullable: false),
                        Sitio_Id = c.Int(nullable: false),
                        FechaCita = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Paciente_Id)
                .ForeignKey("dbo.Ubicaciones", t => t.Sitio_Id)
                .ForeignKey("dbo.Parametros", t => t.TipoCita_Id)
                .Index(t => t.Paciente_Id)
                .Index(t => t.TipoCita_Id)
                .Index(t => t.Sitio_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        NumeroIdentificacion = c.String(nullable: false, maxLength: 25),
                        Nombres = c.String(nullable: false, maxLength: 100),
                        Apellidos = c.String(nullable: false, maxLength: 100),
                        Telefono = c.String(maxLength: 50),
                        FechaNacimiento = c.DateTime(),
                        FechaCreacion = c.DateTime(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Estado_Id = c.Int(nullable: false),
                        TipoIdentificacion_Id = c.Int(nullable: false),
                        TipoUsuario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Parametros", t => t.Estado_Id)
                .ForeignKey("dbo.Parametros", t => t.TipoIdentificacion_Id)
                .ForeignKey("dbo.Parametros", t => t.TipoUsuario_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Estado_Id)
                .Index(t => t.TipoIdentificacion_Id)
                .Index(t => t.TipoUsuario_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Parametros",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Categoria = c.String(nullable: false, maxLength: 20),
                        Codigo = c.String(nullable: false, maxLength: 20),
                        ValorPrincipal = c.String(nullable: false, maxLength: 100),
                        ValorSecundario = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Ubicaciones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 100),
                        Direccion = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Citas", "TipoCita_Id", "dbo.Parametros");
            DropForeignKey("dbo.Citas", "Sitio_Id", "dbo.Ubicaciones");
            DropForeignKey("dbo.Citas", "Paciente_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "TipoUsuario_Id", "dbo.Parametros");
            DropForeignKey("dbo.AspNetUsers", "TipoIdentificacion_Id", "dbo.Parametros");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Estado_Id", "dbo.Parametros");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "TipoUsuario_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "TipoIdentificacion_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Estado_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Citas", new[] { "Sitio_Id" });
            DropIndex("dbo.Citas", new[] { "TipoCita_Id" });
            DropIndex("dbo.Citas", new[] { "Paciente_Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Ubicaciones");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Parametros");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Citas");
        }
    }
}
