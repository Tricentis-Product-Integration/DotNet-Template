using Microsoft.EntityFrameworkCore;

namespace Tricentis.RestApiTemplate.Models;

public class DemoRepository : IDemoRepository
{

    private readonly DemoContext _demoContext;
    private DbSet<DemoItem> _items;

    public DemoRepository(DemoContext dataContext)
    {
        _demoContext = dataContext;
        _items = _demoContext.DemoItems;
    }

    public bool AddDemoItem(DemoItem demoItem)
    {
        if (_items == null) { return false; }

        if (_items.Any(x => x.Id == demoItem.Id)) { return false; }

        _items.Add(demoItem);
        _demoContext.SaveChanges();
        return true;
    }

    public bool DeleteDemoItemById(int id)
    {
        var item = GetDemoItemById(id);

        if (_items == null) { return false; }

        if (item == null) { return false; }

        _items.Remove(item);
        _demoContext.SaveChanges();
        return true;
    }

    public DemoItem? GetDemoItemById(int id)
    {
        if (_items == null) { return null; }

        return _items.FirstOrDefault(x => x.Id == id);
    }

    public IList<DemoItem>? GetDemoItems()
    {
        var items = _demoContext.DemoItems;

        if (_items == null) { return null; }

        return _items.ToList();
    }

    public bool UpdateDemoItem(DemoItem demoItem)
    {
        if (_demoContext.DemoItems == null) { return false; }

        if (!_demoContext.DemoItems.Any(x => x.Id == demoItem.Id)) { return false; }

        _demoContext.DemoItems.Update(demoItem);
        _demoContext.SaveChanges();
        return true;
    }
}