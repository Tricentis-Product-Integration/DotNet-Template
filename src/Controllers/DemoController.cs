using Microsoft.AspNetCore.Mvc;
using Tricentis.Rest_API_Template.Models;

namespace Tricentis.Rest_API_Template.Controllers;

[ApiController]
[Route("[controller]")]
public class DemoController : ControllerBase
{
    private readonly IDemoRepository _demoRepository;

    public DemoController(IDemoRepository demoRepository)
    {
        _demoRepository = demoRepository;
    }

    [HttpGet(Name = "GetDemoValues")]
    public IList<DemoItem> GetDemos()
    {
        if (_demoRepository.GetDemoItems() == null)
        {
            throw new HttpRequestException("DemoItems is Empty");
        }

        return _demoRepository.GetDemoItems();
    }

    [HttpGet("{Id}")]
    public DemoItem GetDemo(int id)
    {
        if (_demoRepository.GetDemoItems() == null)
        {
            throw new HttpRequestException("DemoItems is Empty");
        }

        var demoItem = _demoRepository.GetDemoItemById(id);

        if (demoItem == null)
        {
            throw new HttpRequestException("DemoItem with this ID Does not Exist");
        }

        return demoItem;
    }
}
