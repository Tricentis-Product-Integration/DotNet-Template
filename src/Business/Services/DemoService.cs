using Core.Entities;
using DAL.Contexts;
using Microsoft.Extensions.Logging;

namespace Business.Services;

public class DemoService(DemoContext dataContext, ILogger<DemoService> logger) : IDemoService
{
    private readonly DemoContext _demoContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
    private readonly ILogger<DemoService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public bool AddDemoItem(DemoItem demoItem)
    {
        _logger.LogInformation("Adding Item to Database");
        var items = _demoContext.DemoItems;

        if (items.Any(x => x.Id == demoItem.Id))
        {
            _logger.LogError("Item " + demoItem.Id + " already exists");
            return false;
        }

        items.Add(demoItem);
        _demoContext.SaveChanges();
        return true;
    }

    public bool DeleteDemoItemById(int id)
    {
        var item = GetDemoItemById(id);
        var items = _demoContext.DemoItems;

        if (item == null)
        {
            return false;
        }

        items.Remove(item);
        _demoContext.SaveChanges();
        return true;
    }

    public DemoItem? GetDemoItemById(int id)
    {
        _logger.LogInformation("Retrieving Item " + id + " from Database");
        var item = _demoContext.DemoItems.FirstOrDefault(x => x.Id == id);

        if (item != null)
        {
            _logger.LogInformation("Item " + id + " retrieved successfully");
            return item;
        }

        _logger.LogError("Item " + id + " does not exist");
        return null;
    }

    public IList<DemoItem> GetDemoItems()
    {
        _logger.LogInformation("Retrieving Items from Database");
        return _demoContext.DemoItems.ToArray();
    }

    public bool UpdateDemoItem(DemoItem demoItem)
    {
        _logger.LogInformation("Retrieving Item " + demoItem.Id + "from Database");
        var items = _demoContext.DemoItems;

        if (!items.Any(x => x.Id == demoItem.Id))
        {
            _logger.LogError("Item " + demoItem.Id + " does not exist");
            return false;
        }

        items.Update(demoItem);
        _demoContext.SaveChanges();
        return true;
    }
}