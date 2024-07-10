namespace Tricentis.Rest_API_Template.Models;

public interface IDemoRepository
{
    IList<DemoItem> GetDemoItems();
    DemoItem? GetDemoItemById(int id);
    bool AddDemoItem(DemoItem demoItem);
    bool DeleteDemoItem(DemoItem demoItem);
    bool UpdateDemoItem(DemoItem demoItem);
}