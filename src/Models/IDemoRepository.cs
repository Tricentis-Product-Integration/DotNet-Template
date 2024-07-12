namespace Tricentis.RestApiTemplate.Models;

public interface IDemoRepository
{
    IList<DemoItem>? GetDemoItems();
    DemoItem? GetDemoItemById(int id);
    bool AddDemoItem(DemoItem demoItem);
    bool DeleteDemoItemById(int id);
    bool UpdateDemoItem(DemoItem demoItem);
}