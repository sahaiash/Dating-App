using System;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)  // deriing the db context class
    {
        public DbSet<AppUser> Users { get; set; }
    }
}
