using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace Data.Contexts;

public class DemoContext : DbContext
{
    public DemoContext() { }
    public DemoContext(DbContextOptions<DemoContext> options) : base(options) { }
    public virtual DbSet<DemoItem> DemoItems { get; set; } = null!;
}