using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyanDo.Entity;

namespace FlyanDo.Repository
{
    public class FlyanDoContext : DbContext
    {
        public DbSet<Fly> Flys { get; set; }
        public DbSet<FlyOwner> FlyOwners { get; set; }
        public DbSet<FlyComment> FlyComments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
