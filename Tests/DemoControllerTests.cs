using rest_api_template.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using rest_api_template.Models;

namespace rest_api_template.Tests;

    [TestClass]
    public class DemoControllerTests
    {
        private IDemoRepository demoRepository = IDemoRepositoryMock.GetMock();
        

        [TestMethod]
        public void Test_GetItemByID_Pass() {
            var controller = new DemoController(demoRepository);
            var expectedItem = new DemoItem{Id = 0, DemoValue = "Test"};

            Assert.AreEqual(expectedItem.DemoValue, controller.GetDemo(1).DemoValue);
        }

        [TestMethod]
        public void Test_GetItemByID_NoIDFound() {
            var controller = new DemoController(demoRepository);

            try{
                var fail = controller.GetDemo(2);
                Assert.Fail("An ID not Found Exception hould have been thrown");
            } catch (Exception e) {
                Assert.AreEqual("DemoItem with this ID Does not Exist", e.Message);
            }
        }
    }