using Learning_Data_Integration.Actions;
using Learning_Data_Integration.Data;


class program
{
    ApplicationDbContext applicationDbContext = new ApplicationDbContext();
    public static async  Task Main()
    {
        //applicationDbContext.Database.EnsureCreated();
        ShopActions shop = new ShopActions();
        //shop.AddData();
        //await shop.GetOrders();
        // await shop.GetCustomers();
        //await shop.GetProducts();
        await shop.GetEverything();
    }

}
