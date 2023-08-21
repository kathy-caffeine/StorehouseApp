using StorehouseApp.Dal.Entities;
using StorehouseApp.Dal.Repositories.Interfaces;

namespace StorehouseApp.Dal.Repositories;

internal class PalletRepository : IPalletRepository
{
    private readonly List<PalletEntity> _repository;

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
                .OrderBy(x => x.weight)
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
            .OrderBy(x => x.length * x.height * x.width)
            .ToList();

        return result;
    }
}
