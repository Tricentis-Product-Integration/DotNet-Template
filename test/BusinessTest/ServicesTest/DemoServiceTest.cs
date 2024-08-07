using Moq;
using Moq.EntityFrameworkCore;

using Business.Services;
using Core.Entities;
using DAL.Contexts;
using Microsoft.Extensions.Logging;

namespace BusinessTest.ServicesTest;
public class DemoServiceTest
{
    private readonly Mock<DemoContext> _demoContextMock = new();
    private readonly ILogger<DemoService> _logger = new Logger<DemoService>(new LoggerFactory());

    [SetUp]
    public void Setup()
    {
        var demoItems = new List<DemoItem>()
            { new() { Id = 1, DemoValue = "test1" }, new() { Id = 2, DemoValue = "test2" } };

        _demoContextMock.Setup(d => d.DemoItems).ReturnsDbSet(demoItems);
    }

    [Test]
    public void GetDemoItems_valid()
    {
        var demoService = new DemoService(_demoContextMock.Object, _logger);

        Assert.IsNotNull(demoService);
        Assert.AreEqual(2, demoService.GetDemoItems().Count);
    }

    [Test]
    [TestCase(1, "test1")]
    [TestCase(2, "test2")]
    public void GetDemoItemById_valid(int id, string expectedResult)
    {
        var demoService = new DemoService(_demoContextMock.Object, _logger);

        Assert.IsNotNull(demoService);
        Assert.AreEqual(expectedResult, demoService.GetDemoItemById(id)!.DemoValue);
    }

    [Test]
    [TestCase(3)]
    public void GetDemoItemById_null(int id)
    {
        var demoService = new DemoService(_demoContextMock.Object, _logger);

        Assert.IsNotNull(demoService);
        Assert.IsNull(demoService.GetDemoItemById(id));
    }
}
