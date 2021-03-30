using Logictics.DAL.Seed;
using Logictics.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace Logictics.DAL.EFContext
{
    public class LogicticsDbContext : DbContext
    {

        public LogicticsDbContext()
        {
        }

        public LogicticsDbContext(DbContextOptions<LogicticsDbContext> options)
            : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<UserAdmin>(entity =>
            {
                entity.ToTable("UserAdmin");
                entity.HasKey(x => x.Id);

                entity.Property(e => e.PassWord)
                      .HasMaxLength(150)
                      .IsUnicode(false);

                entity.Property(e => e.Role)
                      .HasMaxLength(50)
                      .IsUnicode(false);

                entity.Property(e => e.Status)
                      .HasMaxLength(50)
                      .IsUnicode(false);

                entity.Property(e => e.UserName)
                      .HasMaxLength(150)
                      .IsUnicode(false);

            
            });

            modelBuilder.Entity<OrderTbl>(entity =>
            {
                entity.ToTable("OrderTbl");
                entity.HasKey(x => x.Id);
                entity.Property(e => e.Status).HasMaxLength(50).IsUnicode(false);
            });

            modelBuilder.Entity<OrderDetailTbl>(entity =>
            {
                entity.ToTable("OrderDetailTbl");
                entity.HasKey(x => x.id);
                entity.Property(e => e.status).HasMaxLength(50).IsUnicode(false);
            });

            modelBuilder.Entity<StoreTbl>(entity =>
            {
                entity.ToTable("StoreTbl");
                entity.HasKey(x => x.Id);
            });

            modelBuilder.Entity<CategoryProductTbl>(entity =>
            {
                entity.ToTable("CategoryProductTbl");
                entity.HasKey(x => x.Id);
            });

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);

        }

       
        public virtual DbSet<UserAdmin> UserAdmin { get; set; }
        public virtual DbSet<StoreTbl> StoreTbls { get; set; }
        public virtual DbSet<CategoryProductTbl> CategoryProductTbls { get; set; }
        public virtual DbSet<OrderTbl> OrderTbls { get; set; }
        public virtual DbSet<OrderDetailTbl> OrderDetailTbls { get; set; }
    }
}
