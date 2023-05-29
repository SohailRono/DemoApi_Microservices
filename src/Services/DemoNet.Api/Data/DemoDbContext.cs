using DemoNet.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoNet.Api.Data
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options) { }

        public DbSet<CustomerInfo> CustomerInfo { get; set; }
    }
}
