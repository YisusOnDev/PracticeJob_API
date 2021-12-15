using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PracticeJob.DAL.Entities
{
    public partial class PracticeJobContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<FP> FPs { get; set; }
        public DbSet<FPFamily> FPFamilies { get; set; }
        public DbSet<FPGrade> FPGrades { get; set; }
        public DbSet<JobOffer> JobOffers { get; set; }
        public PracticeJobContext()
        {
        }

        public PracticeJobContext(DbContextOptions<PracticeJobContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;database=practicejob", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.19-mariadb"));
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
