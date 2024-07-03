using Microsoft.EntityFrameworkCore;

namespace rest_api_template.Models;

public class DemoContext : DbContext
{
    public DemoContext() {}
    public DemoContext(DbContextOptions<DemoContext> options) : base(options) {}
    public virtual DbSet<DemoItem> DemoItems { get; set; } = null!;
}