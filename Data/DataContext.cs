using BackendDotNet.Models.Tables;
using Microsoft.EntityFrameworkCore;

namespace BackendDotNet.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<AppUser> Users { get; set; }
}