
using Microsoft.EntityFrameworkCore;
using Persistent.Entity;

namespace Persistent.Database
{
    public interface IApplicationDbContext
    {
        DbSet<Company>companies { get; }
        DbSet<Price> prices { get; }
      


    }
}
