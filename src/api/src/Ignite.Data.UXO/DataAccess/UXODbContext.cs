using Ignite.Data.UXO.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ignite.Data.UXO.DataAccess
{
    public class UXODbContext : DbContext, IUXODbContext
    {
        public UXODbContext(DbContextOptions<UXODbContext> options) : base(options)
        {
        }
        
        public DbSet<UXODTO> UXOs { get; set; }
        public DbSet<ContactDTO> Contacts { get; set; }
    }
}