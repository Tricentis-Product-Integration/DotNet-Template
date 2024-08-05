using DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DAL;

public static class DALServicesBuilder
{
    public static void AddDALServices(this IServiceCollection services)
    {
        services.AddDbContext<DemoContext>(opt => opt.UseInMemoryDatabase("DemoItems"));
    }
}
