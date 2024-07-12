using Microsoft.AspNetCore.Mvc;
using Tricentis.RestApiTemplate.Models;

namespace Tricentis.RestApiTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class DemoController : ControllerBase
{
    private readonly IDemoRepository _demoRepository;
    private readonly ILogger<DemoController> _logger;

    public DemoController(IDemoRepository demoRepository, ILogger<DemoController> logger)
    {
        _demoRepository = demoRepository;
        _logger = logger;
    }

    [HttpGet(Name = "GetDemoValues")]
    public IList<DemoItem> GetDemos() { 

        IList<DemoItem>? items = _demoRepository.GetDemoItems();

        if (items == null)
        {
            _logger.LogError("DemoItems is empty or does not exist");
            throw new HttpRequestException("DemoItems is empty or does not exist");
        }

        return items;
    }

    [HttpGet("{id}")]
    public DemoItem GetDemo(int id)
    {
        var demoItem = _demoRepository.GetDemoItemById(id);

        if (demoItem == null)
        {   
           _logger.LogError("DemoItem with this ID does not Exist");
            throw new HttpRequestException("DemoItem with this ID does not Exist");
        }

        return demoItem;
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
        var demoItem = _demoRepository.GetDemoItemById(id);

        if (demoItem == null)
        {
            _logger.LogError("DemoItem with this ID does not Exist");
            throw new HttpRequestException("DemoItem with this ID does not Exist");
        }

        _demoRepository.DeleteDemoItemById(id);
    }
}
