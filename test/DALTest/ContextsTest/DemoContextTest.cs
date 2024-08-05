using Moq;
using Moq.EntityFrameworkCore;

using Business.Services;
using Core.Entities;
using DAL.Contexts;
using Microsoft.Extensions.Logging;

namespace DALTest.ContextsTest;
public class Tests
{
    private readonly DemoService demoService = new DemoService;
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}