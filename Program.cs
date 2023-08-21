using StorehouseApp.Bll.Services;
using StorehouseApp.Bll.Services.Interfaces;
using StorehouseApp.Controllers;
using StorehouseApp.Dal.Repositories;
using StorehouseApp.Dal.Repositories.Interfaces;

IPalletRepository palletRepository = new PalletRepository();
IPalletService palletService = new PalletService(palletRepository);
V1StorehouseAppControllers controller = new(palletService);
var rnd = new Random();
const int maxValue = 20;
const int minValue = 10;
const int maxMonth = 11;
const int maxDay = 27;

for(int i = 0; i<3; i++)
{
    controller.AddPallet(new StorehouseApp.Bll.Models.PalletModel(
        i, 
        rnd.Next(minValue, maxValue) + 1,
        rnd.Next(minValue, maxValue) + 1, 
        rnd.Next(minValue, maxValue) + 1,
        rnd.Next(minValue, maxValue) + 1, 
        new List<StorehouseApp.Dal.Entities.BoxEntity>(), 
        DateOnly.MaxValue)
        );
}

for(int i = 0; i<10; i++)
{
    controller.AddBox(new StorehouseApp.Bll.Models.BoxModel(
        i,
        rnd.Next(maxValue + 1), // Для проверки исключения по размерам
        rnd.Next(maxValue),
        rnd.Next(maxValue),
        rnd.Next(maxValue),
        new DateOnly(2024, rnd.Next(maxMonth) + 1, rnd.Next(maxDay) + 1),
        null));
}

var groups = controller.GetRulledPalletList();
foreach(var group in groups)
{
    Console.WriteLine("Годен до " + group.Key.ToString());
    foreach(var item in group.Value)
    {
        Console.WriteLine(item.ToString());
    }
}

var statistics =  controller.GetStatistics();
foreach(var item in statistics)
{
    Console.WriteLine(item.ToString() );
}
