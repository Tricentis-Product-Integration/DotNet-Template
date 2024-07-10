using Tricentis.Rest_API_Template.Controllers;
using Tricentis.Rest_API_Template.Models;
using Moq;
using FluentAssertions;

namespace Tests;

[TestFixture]
public class DemoControllerTest
{
    private Mock<IDemoRepository> demoRepositoryMock = new();

    [SetUp]
    public void SetUp()
    {
        demoRepositoryMock.Setup(d => d.GetDemoItems()).Returns([new DemoItem { Id = 1, DemoValue = "Test1" }, new DemoItem { Id = 2, DemoValue = "Test2" }]);
        demoRepositoryMock.Setup(d => d.GetDemoItemById(1)).Returns(new DemoItem { Id = 1, DemoValue = "Test1" });
        demoRepositoryMock.Setup(d => d.GetDemoItemById(2)).Returns(new DemoItem { Id = 2, DemoValue = "Test2" });
    }

    [TestCase(1, "Test1")]
    public void Test_GetItemByID_Pass1(int id, string expectedValue)
    {
        var controller = new DemoController(demoRepositoryMock.Object);

        controller.GetDemo(id).DemoValue.Should().Be(expectedValue);
    }

    [TestCase(2, "Test2")]
    public void Test_GetItemByID_Pass2(int id, string expectedValue)
    {
        var controller = new DemoController(demoRepositoryMock.Object);

        controller.GetDemo(id).DemoValue.Should().Be(expectedValue);
    }

    [TestCase(0)]
    public void Test_GetItemByID_NoIDFound(int id)
    {
        var controller = new DemoController(demoRepositoryMock.Object);
        Action fail = () => controller.GetDemo(id);

        fail.Should().Throw<HttpRequestException>();
    }

    [Test]
    public void Test_GetItems_IsNotNull_Pass()
    {
        var controller = new DemoController(demoRepositoryMock.Object);

        controller.GetDemos().Count.Should().Be(2);
    }
}