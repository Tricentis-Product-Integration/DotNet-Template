using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using rest_api_template.Models;
using System.Linq;

namespace rest_api_template.Controllers;

[ApiController]
[Route("[controller]")]
public class DemoController : ControllerBase
{
    private readonly IDemoRepository _demoRepository;

    public DemoController(IDemoRepository demoRepository) {
        _demoRepository = demoRepository;
    }
    
    [HttpGet(Name = "GetDemoValues")]
    public IList<DemoItem> GetDemos()
    {
        if (_demoRepository.GetDemoItems() == null) {
            throw new HttpRequestException("DemoItems is Empty");
        }

        return _demoRepository.GetDemoItems();
    }

    [HttpGet("{Id}")]
    public DemoItem GetDemo(int id)
    {
        if (_demoRepository.GetDemoItems() == null) {
            throw new HttpRequestException("DemoItems is Empty");
        }

        var demoItem = _demoRepository.GetDemoItemById(id);

        if (demoItem == null) {
            throw new HttpRequestException("DemoItem with this ID Does not Exist");
        }

        return demoItem;
    }
}
