using Microsoft.EntityFrameworkCore;
namespace Tricentis.RestApiTemplate.Models;

public class DemoContext : DbContext
{
    public DemoContext() { }
    public DemoContext(DbContextOptions<DemoContext> options) : base(options) { }
    public virtual DbSet<DemoItem> DemoItems { get; set; } = null!;
}