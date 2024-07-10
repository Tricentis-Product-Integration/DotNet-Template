namespace Tricentis.Rest_API_Template.Models;

public class DemoRepository : IDemoRepository
{

    private readonly DemoContext _demoContext;

    public DemoRepository(DemoContext dataContext)
    {
        _demoContext = dataContext;
    }

    public bool AddDemoItem(DemoItem demoItem)
    {
        if (_demoContext.DemoItems == null)
            return false;

        if (_demoContext.DemoItems.Any(x => x.Id == demoItem.Id)) { return false; }

        _demoContext.DemoItems.Add(demoItem);
        _demoContext.SaveChanges();
        return true;
    }

    public bool DeleteDemoItem(DemoItem demoItem)
    {
        if (_demoContext.DemoItems == null)
            return false;

        if (!_demoContext.DemoItems.Any(x => x.Id == demoItem.Id)) { return false; }

        _demoContext.DemoItems.Remove(demoItem);
        _demoContext.SaveChanges();
        return true;
    }

    public DemoItem? GetDemoItemById(int id)
    {
        if (_demoContext.DemoItems == null)
            return null;

        return _demoContext.DemoItems.FirstOrDefault(x => x.Id == id);
    }

    public IList<DemoItem> GetDemoItems()
    {
        if (_demoContext.DemoItems == null)
            return new List<DemoItem>();

        return _demoContext.DemoItems.ToList();
    }

    public bool UpdateDemoItem(DemoItem demoItem)
    {
        if (_demoContext.DemoItems == null)
            return false;

        if (!_demoContext.DemoItems.Any(x => x.Id == demoItem.Id)) { return false; }

        _demoContext.DemoItems.Update(demoItem);
        _demoContext.SaveChanges();
        return true;
    }
}