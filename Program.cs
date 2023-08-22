using StorehouseApp.Bll.Models;
using StorehouseApp.Bll.Services;
using StorehouseApp.Bll.Services.Interfaces;
using StorehouseApp.Controllers;
using StorehouseApp.Dal.Repositories;
using StorehouseApp.Dal.Repositories.Interfaces;

IPalletRepository palletRepository = new PalletRepository();
IPalletService palletService = new PalletService(palletRepository);
V1StorehouseAppControllers controller = new(palletService);

Console.WriteLine("Выберите формат ввода данных:");
Console.WriteLine("1: сгенерировать случайно");
Console.WriteLine("2: импортировать из файла");
var crunch = true;
while (crunch)
{
    var inputFormat = Convert.ToInt32(Console.ReadLine());

    switch (inputFormat)
    {
        case 1:
            {
                var rnd = new Random();
                const int maxValue = 20;
                const int minValue = 10;
                const int maxMonth = 11;
                const int maxDay = 27;

                // Добавление паллет
                for (int i = 0; i < 4; i++)
                {
                    controller.AddPallet(new PalletModel(
                        i,
                        rnd.Next(minValue, maxValue) + 1,
                        rnd.Next(minValue, maxValue) + 1,
                        rnd.Next(minValue, maxValue) + 1,
                        30,
                        new List<StorehouseApp.Dal.Entities.BoxEntity>(),
                        DateOnly.MaxValue)
                        );
                }

                // Добавление коробок
                for (int i = 0; i < 10; i++)
                {
                    if (i % 3 == 0) // Добавление коробки с датой только производства 
                    {
                        controller.AddBox(new BoxModel(
                        i,
                        rnd.Next(maxValue + 1), // Для проверки исключения по размерам
                        rnd.Next(maxValue),
                        rnd.Next(maxValue),
                        rnd.Next(maxValue),
                        new DateOnly(2024, rnd.Next(maxMonth) + 1, rnd.Next(maxDay) + 1)));
                        continue;
                    }
                    controller.AddBox(new BoxModel(
                        i,
                        rnd.Next(maxValue + 1), // Для проверки исключения по размерам
                        rnd.Next(maxValue),
                        rnd.Next(maxValue),
                        rnd.Next(maxValue),
                        new DateOnly(2024, rnd.Next(maxMonth) + 1, rnd.Next(maxDay) + 1),
                        null));
                }
                Console.WriteLine("Добавлено 4 паллеты и 10 коробок");
                crunch = false;
                break;
            }
        case 2:
            {
                Console.WriteLine("Введите имя файла:");
                var filename = Console.ReadLine();
                controller.Deserealize(filename);
                crunch = false;
                break;
            }
        default:
            {
                Console.WriteLine("Введено неправильное число, попробуйте ещё раз.");
                break;
            }
    }
}

// Вывод паллет, сгруппированных по сроку годности 
#region
Console.WriteLine("Паллеты, сгруппированные по сроку годности, " +
    "отсортированные по возрастанию по сроку годности, " +
    "внутри группы отсортированы по весу.");
var groups = controller.GetRulledPalletList();
foreach(var group in groups)
{
    Console.WriteLine("Годен до " + group.Key.ToString());
    foreach(var item in group.Value)
    {
        Console.WriteLine(item.ToString());
    }
}
#endregion

// 3 паллеты с наибольшим сроком годности по возрастанию объема
#region
Console.WriteLine("3 паллеты с наибольшим сроком годности " +
    "по возрастанию объема.");
var statistics =  controller.GetStatistics();
foreach(var item in statistics)
{
    Console.WriteLine(item.ToString() );
}
#endregion

Console.WriteLine("Хотите ли вы сохранить данные о состоянии паллет и коробок в файл?");
Console.WriteLine("1: да");
Console.WriteLine("2: нет");
crunch = true;
while (crunch)
{
    var choise = Convert.ToInt32(Console.ReadLine());
    switch (choise)
    {
        case 1:
            {
                controller.Serealize();
                crunch = false;
                break;
            }
            case 2:
            {
                crunch = false;
                break;
            }
        default:
            {
                Console.WriteLine("Введено неправильное число, попробуйте ещё раз.");
                break;
            }
    }
}
