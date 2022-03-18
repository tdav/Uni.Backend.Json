using System.Linq;
using Microsoft.EntityFrameworkCore;
using Toolbelt.ComponentModel.DataAnnotations;

namespace App.Database
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql("Host=127.0.0.1;Database=uni_db;Username=postgres;Password=1;Pooling=true;")
                             .EnableDetailedErrors()
                             .EnableSensitiveDataLogging()
                             .UseSnakeCaseNamingConvention()
                             .EnableServiceProviderCaching();
        }

        public DbSet<spRole> spRoles { get; set; }
        public DbSet<tbData> tbDatas { get; set; }
        public DbSet<tbUser> tbUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.BuildIndexesFromAnnotations();

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}