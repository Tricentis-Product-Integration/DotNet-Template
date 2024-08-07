using Microsoft.AspNetCore.Mvc;
using Business.Services;
using Core.Entities;


namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class DemoController(IDemoService demoService, ILogger<DemoController> logger) : ControllerBase
{
    private readonly IDemoService _demoService = demoService ?? throw new HttpRequestException("DemoItems is empty or does not exist");
    private readonly ILogger<DemoController> _logger = logger;

    [HttpGet(Name = "GetDemoValues")]
    public IList<DemoItem> GetDemos()
    {
        _logger.LogInformation("Request received by controller");
        return _demoService.GetDemoItems();
    }

    [HttpGet("{id}")]
    public DemoItem? GetDemo(int id)
    {
        _logger.LogInformation("Request received by controller");
        return _demoService.GetDemoItemById(id);
    }

    [HttpPost]
    public void PostDemo(int id, string value)
    {
        _logger.LogInformation("Request received by controller");
        var demoItem = new DemoItem { Id = id, DemoValue = value };

        _demoService.AddDemoItem(demoItem);
    }

    [HttpPut]
    public void PutDemo(int id, string value)
    {
        _logger.LogInformation("Request received by controller");
        var demoItem = new DemoItem { Id = id, DemoValue = value };

        _demoService.UpdateDemoItem(demoItem);
    }

    [HttpDelete("{id}")]
    public void DeleteDemo(int id)
    {
        _logger.LogInformation("Request received by controller");
        _demoService.DeleteDemoItemById(id);
    }
}

