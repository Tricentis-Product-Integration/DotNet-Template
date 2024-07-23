using FluentAssertions;
using Moq;

using API;
using Business;
using Core;


namespace Test;

[TestFixture]
public class DemoControllerTest
{
    private readonly Mock<IDemoService> _demoServiceMock = new();

    [SetUp]
    public void SetUp()
    {
        _demoServiceMock.Setup(d => d.GetDemoItems()).Returns([new DemoItem { Id = 1, DemoValue = "Test1" }, new DemoItem { Id = 2, DemoValue = "Test2" }]);
        _demoServiceMock.Setup(d => d.GetDemoItemById(1)).Returns(new DemoItem { Id = 1, DemoValue = "Test1" });
        _demoServiceMock.Setup(d => d.GetDemoItemById(2)).Returns(new DemoItem { Id = 2, DemoValue = "Test2" });
    }

    [TestCase(1, "Test1")]
    [TestCase(2, "Test2")]
    public void Test_GetItemByID_Pass1(int id, string expectedValue)
    {
        var controller = new DemoController(_demoServiceMock.Object);

        var demo = controller.GetDemo(id)!;
        demo.DemoValue.Should().Be(expectedValue);
    }

    [TestCase(0)]
    public void Test_GetItemByID_NoIDFound(int id)
    {
        var controller = new DemoController(_demoServiceMock.Object);

        controller.GetDemo(id).Should().BeNull();
    }

    [Test]
    public void Test_GetItems_IsNotNull_Pass()
    {
        var controller = new DemoController(_demoServiceMock.Object);

        controller.GetDemos().Should().NotBeNull();
    }
}