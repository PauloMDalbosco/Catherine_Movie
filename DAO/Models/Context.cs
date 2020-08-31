using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DAO.Models
{
    class Context : DbContext
    {
        public Context(DbConnection Connection)
           : base(Connection, false)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocateRegister> LocateRegisters { get; set; }
    }
}
