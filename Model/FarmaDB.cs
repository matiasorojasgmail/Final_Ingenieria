using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMApplication.Model
{
    public class FarmaDB:DbContext
    {
        public FarmaDB():base("name=DefaultConnection")
        {
            Database.SetInitializer<FarmaDB>(new CreateDatabaseIfNotExists<FarmaDB>());
        }

        public DbSet<Proveedor> Proveedor { get; set; }
        public DbSet<Producto> Producto { get; set; }
    }
}
