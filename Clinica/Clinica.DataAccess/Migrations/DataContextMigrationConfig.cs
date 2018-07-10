using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    using Clinica.DataAccess.Context;

    /// <summary>
    /// Configuration para migración
    /// </summary>
    class DataContextMigrationConfig : DbMigrationsConfiguration<DataContext>
    {
        #region Construction
        public DataContextMigrationConfig()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }
        #endregion

        /// <summary>
        /// Seeding del proyecto
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(DataContext context)
        {
            new DataSeeder(context).Seed();
        }
    }
}
