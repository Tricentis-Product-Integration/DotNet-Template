using Tricentis.RestApiTemplate.Controllers;

namespace Tricentis.RestApiTemplate.Models;

public class DemoRepository(DemoContext dataContext, ILogger<DemoController> logger) : IDemoRepository
{

    private readonly DemoContext _demoContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
    private readonly ILogger<DemoController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public bool AddDemoItem(DemoItem demoItem)
    {
        var items = _demoContext.DemoItems;

        if (items.Any(x => x.Id == demoItem.Id)) {
            _logger.LogWarning("Item with this Id already exists");
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

        if (item == null) {
            return false; 
        }

        items.Remove(item);
        _demoContext.SaveChanges();
        return true;
    }

    public DemoItem? GetDemoItemById(int id)
    {
        var item = _demoContext.DemoItems.FirstOrDefault(x => x.Id == id);

        if (item != null)
        {
            return item;
        }

        _logger.LogWarning("Item with this ID does not exist");
        return null;
    }

    public IList<DemoItem> GetDemoItems()
    {
        return _demoContext.DemoItems.ToArray();
    }

    public bool UpdateDemoItem(DemoItem demoItem)
    {
        var items = _demoContext.DemoItems;

        if (!items.Any(x => x.Id == demoItem.Id))
        {
            _logger.LogWarning("Item with this ID does not exist");
            return false;
        }

        items.Update(demoItem);
        _demoContext.SaveChanges();
        return true;
    }
}