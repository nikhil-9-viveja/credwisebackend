using CredWiseAdmin.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CredWiseAdmin.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<LoanProduct> LoanProducts { get; set; }
        public DbSet<HomeLoanDetail> HomeLoanDetails { get; set; }
        public DbSet<PersonalLoanDetail> PersonalLoanDetails { get; set; }
        public DbSet<GoldLoanDetail> GoldLoanDetails { get; set; }
        public DbSet<Fdtype> Fdtypes { get; set; }
        public DbSet<LoanProductDocument> LoanProductDocuments { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure LoanProduct
            modelBuilder.Entity<LoanProduct>(entity =>
            {
                entity.HasKey(e => e.LoanProductId);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.ImageUrl).IsRequired();
                entity.Property(e => e.LoanType).IsRequired().HasMaxLength(20);
            });

            // Configure relationships
            modelBuilder.Entity<LoanProduct>()
                .HasOne(e => e.HomeLoanDetail)
                .WithOne(e => e.LoanProduct)
                .HasForeignKey<HomeLoanDetail>(e => e.LoanProductId);

            modelBuilder.Entity<LoanProduct>()
                .HasOne(e => e.PersonalLoanDetail)
                .WithOne(e => e.LoanProduct)
                .HasForeignKey<PersonalLoanDetail>(e => e.LoanProductId);

            modelBuilder.Entity<LoanProduct>()
                .HasOne(e => e.GoldLoanDetail)
                .WithOne(e => e.LoanProduct)
                .HasForeignKey<GoldLoanDetail>(e => e.LoanProductId);

            modelBuilder.Entity<LoanProduct>()
                .HasMany(e => e.LoanProductDocuments)
                .WithOne(e => e.LoanProduct)
                .HasForeignKey(e => e.LoanProductId);
        }
    }
} 