using Microsoft.EntityFrameworkCore;
using Notino.Domain.Models.Abstraction;

namespace Notino.Data.InMemoryEF.Database;

public class NotinoDBContext : DbContext
{
    public NotinoDBContext(DbContextOptions<NotinoDBContext> options) : base(options)
    {
    }

    public DbSet<TModel> Set<TModel>()
        where TModel : class, IModel
    {
        return base.Set<TModel>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(IModel).IsAssignableFrom(entity.ClrType))
            {
                modelBuilder.Entity(entity.ClrType).Property<Guid>("Id");
                modelBuilder.Entity(entity.ClrType).HasKey("Id");
            }
        }

        base.OnModelCreating(modelBuilder);
    }
}
