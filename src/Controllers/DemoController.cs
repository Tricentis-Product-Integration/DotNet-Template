using Microsoft.AspNetCore.Mvc;
using Tricentis.RestApiTemplate.Models;

namespace Tricentis.RestApiTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class DemoController(IDemoRepository demoRepository) : ControllerBase
{
    private readonly IDemoRepository _demoRepository = demoRepository ?? throw new HttpRequestException("DemoItems is empty or does not exist");

    [HttpGet(Name = "GetDemoValues")]
    public IList<DemoItem> GetDemos()
    {
        return _demoRepository.GetDemoItems();
    }

    [HttpGet("{id}")]
    public DemoItem? GetDemo(int id)
    {
        return _demoRepository.GetDemoItemById(id);
    }

    [HttpPost]
    public void PostDemo(int id, string value) { 
        var demoItem = new DemoItem { Id = id, DemoValue = value };

        _demoRepository.AddDemoItem(demoItem);
    }

    [HttpPut]
    public void PutDemo(int id, string value)
    {
        var demoItem = new DemoItem { Id = id, DemoValue = value };

        _demoRepository.UpdateDemoItem(demoItem);
    }

    [HttpDelete("{id}")]
    public void DeleteDemo(int id)
    {
        _demoRepository.DeleteDemoItemById(id);
    }
}
