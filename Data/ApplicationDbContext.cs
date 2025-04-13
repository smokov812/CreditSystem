using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BankCreditSystem.Domain.Entities;

namespace BankCreditSystem.Data
{
    // Наследуем от IdentityDbContext для использования стандартного IdentityUser
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet для ваших собственных сущностей
        public DbSet<Client> Clients { get; set; }
        public DbSet<CreditApplication> CreditApplications { get; set; }
        public DbSet<CreditAssessment> CreditAssessments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Важно! Вызовите базовый метод, чтобы Identity настроил свои таблицы
            base.OnModelCreating(modelBuilder);

            // Пример настройки отношений и типов для ваших сущностей
            modelBuilder.Entity<Client>()
                .HasMany(c => c.CreditApplications)
                .WithOne(a => a.Client)
                .HasForeignKey(a => a.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CreditApplication>()
                .HasOne(a => a.CreditAssessment)
                .WithOne(ca => ca.CreditApplication)
                .HasForeignKey<CreditAssessment>(ca => ca.CreditApplicationId);

            modelBuilder.Entity<Client>()
                .Property(c => c.Income)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<CreditApplication>()
                .Property(ca => ca.Amount)
                .HasColumnType("decimal(18,2)");
        }
    }
}
