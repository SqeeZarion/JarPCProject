using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace JarPControlProject.Database;

using System.Data.SqlClient;


public class DBConnection : DbContext
{

    public DBConnection(DbContextOptions<DBConnection> options) : base(options)
    {
        
    }
    
    public DbSet<UserAccount> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserAccount>().ToTable("Users");
    }
}
