using Microsoft.EntityFrameworkCore;
using Domain.Entities;


namespace Infrastructure.Context
{
    public class uniContext : DbContext
    {
        public uniContext(DbContextOptions<uniContext> options) : base(options) 
        {
        }
        public DbSet<Professor>Professors { get; set; }
        public DbSet<VipClient>VipClients { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
    }
}
