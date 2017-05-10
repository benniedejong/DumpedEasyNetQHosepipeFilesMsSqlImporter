namespace DumpedEasyNetQHosepipeFilesMsSqlImporter.Console.Model
{
    using Entities;
    using System.Data.Entity;

    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base() {
            Database.SetInitializer<DatabaseContext>(new CreateDatabaseIfNotExists<DatabaseContext>());
        }

        public DatabaseContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Database.SetInitializer<DatabaseContext>(new CreateDatabaseIfNotExists<DatabaseContext>());
        }

        /// <summary>
        /// Overrides the default OnModelCreation in order to enable the discovery of EntityFramework Fluent API table mapping files
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.AddFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
        }

        public DbSet<EasyNetQHosepipeDumped> HosepipeDumps { get; set; }

        public DbSet<EasyNetQHosepipeDumpedProperties> Properties { get; set; }

        public DbSet<EasyNetQHosepipeDumpedInfo> Infos { get; set; }
    }
}
