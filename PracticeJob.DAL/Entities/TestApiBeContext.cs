using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PracticeJob.DAL.Entities;

#nullable disable

namespace PracticeJob.DAL.Entities
{
    public partial class TestApiBeContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public TestApiBeContext()
        {
        }

        public TestApiBeContext(DbContextOptions<TestApiBeContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;database=testapibe", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.19-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
