using StorehouseApp.Dal.Entities;
using StorehouseApp.Dal.Repositories.Interfaces;
using System.Text;

namespace StorehouseApp.Dal.Repositories;

public class PalletRepository : IPalletRepository
{
    private readonly List<PalletEntity> _repository;
    bool crunch = true;

    public PalletRepository()
    {
        _repository = new List<PalletEntity>();
    }

    public void Save(PalletEntity entity)
    {
        _repository.Add(entity);
    }

    public IReadOnlyList<PalletEntity> Query()
    {
        return _repository;
    }

    public void Clear()
    {
        _repository.Clear();
    }

    public void AddBox(BoxEntity box)
    {
        try
        {
            var pallet = _repository
            .OrderBy(x => x.boxes.Count)
            // Cамая пустая паллета, в которую влезает коробка
            .First(x => (x.length >= box.length && x.width >= box.width));
            pallet.boxes.Add(box);
            if(box.expirationDate<pallet.expirationDate) pallet.expirationDate = box.expirationDate;

        }
        catch (InvalidOperationException) // Если нет такой паллеты
        {
            Console.WriteLine("Для коробки такого размера нет подходящей паллеты.");
        }
    }

    public void AddPallet(PalletEntity pallet)
    {
        _repository.Add(pallet);
    }

    public List<List<PalletEntity>> RulledSortPallet()
    {
        var groups = _repository
            .GroupBy(x=>x.expirationDate)
            .OrderBy(x=>x.Key)
            .ToList();
        var result = new List<List<PalletEntity>>();
        foreach (var group in groups)
        {
            result.Add(group
                .OrderBy(x => x.weight + x.boxes.Sum(y => y.weight))
                .ToList());
        }
        return result;
    }

    public List<PalletEntity> RulerPalletStatistics()
    {
        var result = _repository
            .OrderBy(x => x.expirationDate)
            .ToList()
            .GetRange(0, 3)
            .OrderBy(x => x.length * x.height * x.width 
            + x.boxes.Sum(y => y.width * y.length * y.height))
            .ToList();

        return result;
    }

    public void Serialize()
    {
        var filename = String.Format("Storehouse_{0}_{1}_{2}_{3}_{4}_{5}.txt",
            DateTime.Now.Day,
            DateTime.Now.Month,
            DateTime.Now.Year,
            DateTime.Now.Hour,
            DateTime.Now.Minute,
            DateTime.Now.Second);
        var sb = new StringBuilder();
        foreach(var pallet in _repository)
        {
            sb.AppendLine(pallet.ToString());
        }
        File.WriteAllText(filename, sb.ToString());
        Console.WriteLine("Состояние паллет и коробок успешно сохранено в файл {0}", filename);
    }

    public void Deserialize(string fileName)
    {
        try
        {
            var inputData = new StreamReader(fileName).ReadToEnd();
            var palletsArray = inputData.Split("Паллета");
            for(int j = 1; j < palletsArray.Length; j++)
            {
                var boxes = new List<BoxEntity>();
                var splites = palletsArray[j].Split("\r\n");
                // [1] данные о паллете
                // [2] фраза про коробки для человекочитаемости
                // [3] и дальше данные о коробках
                for (int i = 3; i < splites.Length-2; i++)
                {
                    var boxData = splites[i].Split(" ");
                    if (boxData.Length -1 == 6)
                    {
                        boxes.Add(new BoxEntity(
                        Convert.ToInt32(boxData[0]),
                        Convert.ToInt32(boxData[1]),
                        Convert.ToInt32(boxData[2]),
                        Convert.ToInt32(boxData[3]),
                        Convert.ToInt32(boxData[4]),
                        DateOnly.FromDateTime(DateTime.Parse(boxData[5])),
                        null));
                    }
                    if (boxData.Length -1 == 7)
                    {
                        boxes.Add(new BoxEntity(
                        Convert.ToInt32(boxData[0]),
                        Convert.ToInt32(boxData[1]),
                        Convert.ToInt32(boxData[2]),
                        Convert.ToInt32(boxData[3]),
                        Convert.ToInt32(boxData[4]),
                        DateOnly.FromDateTime(DateTime.Parse(boxData[5])),
                        DateOnly.FromDateTime(DateTime.Parse(boxData[6]))));
                    }
                }
                var palletData = splites[1].Split(" ");
                _repository.Add(new PalletEntity(
                    Convert.ToInt32(palletData[0]),
                    Convert.ToInt32(palletData[1]),
                    Convert.ToInt32(palletData[2]),
                    Convert.ToInt32(palletData[3]),
                    Convert.ToInt32(palletData[4]),
                    boxes,
                    DateOnly.FromDateTime(DateTime.Parse(palletData[5]))
                    ));
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Такой файл не найден. Повторите попытку ввода.");
            crunch = false;
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("Неверный формат файла. Повторите попытку ввода.");
            crunch = false;
        }
        catch (Exception)
        {
            Console.WriteLine("Что-то пошло не так. Повторите попытку ввода.");
            crunch = false;
        }

        if(crunch)
        {
            Console.WriteLine("Десериализация прошла успешно");
        }
    }

    public bool stateCheckout()
    {
        return crunch;
    }
}
