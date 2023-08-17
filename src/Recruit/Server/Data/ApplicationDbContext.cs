using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Recruit.Server.Models.ModelViews;
using Recruit.Shared;
using System.Reflection;

namespace Recruit.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Job> Jobs => Set<Job>();
        public DbSet<Applicant> Applicants => Set<Applicant>();
        public DbSet<Stage> Stages => Set<Stage>();
        public DbSet<Interview> Interviews => Set<Interview>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Template> Templates => Set<Template>();
        public DbSet<EmailItem> Emails => Set<EmailItem>();
        public DbSet<NgayCongDb> NgayCongs { get; set; }
        public DbSet<NhanVienDb> NhanViens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<NhanVien>(builder =>
            //{
            //    builder.HasNoKey();
            //});
            modelBuilder.Entity<NgayCongDb>().ToTable("BANGCHAMCONG");
            modelBuilder.Entity<NgayCongDb>().ToTable("BANGCHAMCONG", "dbo");

            modelBuilder.Entity<NhanVienDb>().ToTable("NHANVIEN");
            modelBuilder.Entity<NhanVienDb>().ToTable("NHANVIEN", "dbo");

            modelBuilder.Entity<NhanVienDb>()
                .HasOne(a => a.Applicant)
                .WithOne(b => b.Nhanvien)
                .HasForeignKey<Applicant>(b => b.STT);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
