namespace InterestRateCounter.Models.Context
{
    using InterestRateCounter.Models.Models;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Agreement> Agreements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(p => p.Agreements)
                .WithOne(b => b.Customer)
                .HasForeignKey(p => p.CustomerId);

            modelBuilder.Entity<Customer>().HasData(
                    new Customer() { Id = 67812203006, FullName = "Goras Trusevičius" },
                    new Customer() { Id = 78706151287, FullName = "Dange Kulkavičiutė" }
                );

            modelBuilder.Entity<Agreement>().HasData(
                new Agreement() { Amount = 12000, BaseRateCode = "VILIBOR3m", CustomerId = 67812203006, Id = 1, Margin = 1.6m, Duration = 60, Timestamp = new DateTime(2012, 02, 01) },
                new Agreement() { Amount = 8000, BaseRateCode = "VILIBOR1y", CustomerId = 78706151287, Id = 2, Margin = 2.2m, Duration = 36, Timestamp = new DateTime(2013, 02, 01) },
                new Agreement() { Amount = 1000, BaseRateCode = "VILIBOR6m", CustomerId = 78706151287, Id = 3, Margin = 1.85m, Duration = 24, Timestamp = new DateTime(2014, 02, 01) }
                );
        }
    }
}
