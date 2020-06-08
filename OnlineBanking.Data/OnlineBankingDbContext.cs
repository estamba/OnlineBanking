using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineBanking.Core.Domain;
using System;

namespace OnlineBanking.Data
{
    public class OnlineBankingDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public OnlineBankingDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(configuration.GetConnectionString("DefaultConnection"));
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Transaction>().ToTable("Transaction");

            modelBuilder.Entity<User>().ToTable("User");


        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }

        




    }
}
