using Core.Entities;
using Data.Contexts;

namespace Business.Services;

public class DemoService(DemoContext dataContext) : IDemoService
{

    private readonly DemoContext _demoContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));

    public bool AddDemoItem(DemoItem demoItem)
    {
        var items = _demoContext.DemoItems;

        if (items.Any(x => x.Id == demoItem.Id))
        {
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
        var item = _demoContext.DemoItems.FirstOrDefault(x => x.Id == id);

        if (item != null)
        {
            return item;
        }

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
            return false;
        }

        items.Update(demoItem);
        _demoContext.SaveChanges();
        return true;
    }
}