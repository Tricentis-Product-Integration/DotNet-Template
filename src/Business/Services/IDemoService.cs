using Core.Entities;

namespace Business.Services;

public interface IDemoService
{
    IList<DemoItem> GetDemoItems();
    DemoItem? GetDemoItemById(int id);
    bool AddDemoItem(DemoItem demoItem);
    bool DeleteDemoItemById(int id);
    bool UpdateDemoItem(DemoItem demoItem);
}