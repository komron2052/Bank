using Bank.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bank.Data.DbContexts;

public class BankDbContext : DbContext
{
    public BankDbContext(DbContextOptions<BankDbContext> options) : base(options)
    { 
    }

    public DbSet<User> Users { get; set; }
}
