using Microsoft.AspNetCore.Mvc;
using Business.Services;
using Core.Entities;


namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class DemoController(IDemoService demoService) : ControllerBase
{
    private readonly IDemoService _demoService = demoService ?? throw new HttpRequestException("DemoItems is empty or does not exist");

    [HttpGet(Name = "GetDemoValues")]
    public IList<DemoItem> GetDemos()
    {
        return _demoService.GetDemoItems();
    }

    [HttpGet("{id}")]
    public DemoItem? GetDemo(int id)
    {
        return _demoService.GetDemoItemById(id);
    }

    [HttpPost]
    public void PostDemo(int id, string value)
    {
        var demoItem = new DemoItem { Id = id, DemoValue = value };

        _demoService.AddDemoItem(demoItem);
    }

    [HttpPut]
    public void PutDemo(int id, string value)
    {
        var demoItem = new DemoItem { Id = id, DemoValue = value };

        _demoService.UpdateDemoItem(demoItem);
    }

    [HttpDelete("{id}")]
    public void DeleteDemo(int id)
    {
        _demoService.DeleteDemoItemById(id);
    }
}

