using Microsoft.EntityFrameworkCore;
using StudentManagementASPDotNet.Models;

namespace StudentManagementASPDotNet.Data;

public class UserDataDBContext : DbContext
{
    public UserDataDBContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<UserDetails> userdetails { get; set; }
    
}