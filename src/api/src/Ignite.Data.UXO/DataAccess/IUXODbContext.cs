using Ignite.Data.UXO.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Ignite.Data.UXO.DataAccess
{
    public interface IUXODbContext
    {
        DbSet<UXODTO> UXOs { get; set; }
        DatabaseFacade Database { get; }
    }
}