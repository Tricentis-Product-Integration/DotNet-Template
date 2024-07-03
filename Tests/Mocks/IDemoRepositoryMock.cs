using rest_api_template.Models;

namespace rest_api_template.Tests;



public class IDemoRepositoryMock {
    public static IDemoRepository GetMock()
    {
        List<DemoItem> lstItem = GenerateTestData();
        DemoContext dbContextMock = DbContextMock.GetMock<DemoItem, DemoContext>(lstItem, x => x.DemoItems);
        return new DemoRepository(dbContextMock);
    }

    private static List<DemoItem> GenerateTestData()
    {
        List<DemoItem> lstItems = [new DemoItem{Id = 1, DemoValue = "Test"}];

        /*
        for (int index = 1; index <= 10; index++)
        {                
            lstItems.Add(new DemoItem
            {
                Id = index,
                DemoValue = "Item 2"

            });
        }
        */
        return lstItems;
    }
}
