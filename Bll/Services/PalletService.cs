using StorehouseApp.Bll.Models;
using StorehouseApp.Bll.Services.Interfaces;
using StorehouseApp.Dal.Entities;
using StorehouseApp.Dal.Repositories.Interfaces;

namespace StorehouseApp.Bll.Services;

public class PalletService : IPalletService
{
    private readonly IPalletRepository _repository;
    public PalletService(IPalletRepository _repository)
    {
        this._repository = _repository;
    }

    public void AddBox(BoxModel box)
    {
        _repository.AddBox(new BoxEntity(
            box.id,
            box.width,
            box.height, 
            box.length,
            box.weight,
            box.expirationDate,
            box.productionDate));
    }

    public void AddPallet(PalletModel pallet)
    {
        _repository.AddPallet(new PalletEntity(
            pallet.id, 
            pallet.width,
            pallet.height,
            pallet.length,
            pallet.weight,
            pallet.boxes,
            pallet.expirationDate));
    }

    public Dictionary<DateOnly, List<PalletModel>> GetRulledPalletList()
    {
        var rulledList = _repository.Query()
            .GroupBy(x => x.expirationDate)
            .OrderBy(x => x.Key)
            .ToList();
        var result = new List<List<PalletEntity>>();
        foreach (var group in rulledList)
        {
            result.Add(group
                .OrderBy(x => x.weight + x.boxes.Sum(y => y.weight))
                .ToList());
        }
        var dictionary = new Dictionary<DateOnly, List<PalletModel>>();
        foreach (var item in result)
        {
            dictionary.Add(item[0].expirationDate, 
                item.Select(
                    x=> 
                    new PalletModel(
                    x.id, 
                    x.width, 
                    x.height, 
                    x.length, 
                    x.weight, 
                    x.boxes, 
                    x.expirationDate))
                .ToList()
                    );
        }
        return dictionary;
    }

    public List<PalletModel> GetTopStatistics()
    {
        var statistics = _repository.Query()
            .OrderBy(x => x.expirationDate)
            .ToList()
            .GetRange(0, 3)
            .OrderBy(x => x.length * x.height * x.width
            + x.boxes.Sum(y => y.width * y.length * y.height))
            .ToList();
        return statistics
            .Select(x =>
            new PalletModel(
                    x.id,
                    x.width,
                    x.height,
                    x.length,
                    x.weight,
                    x.boxes,
                    x.expirationDate))
            .ToList();
    }

    public void Serealize()
    {
        _repository.Serialize();
    }

    public void Deserealize(string fileName)
    {
        _repository.Deserialize(fileName);
    }

    public bool stateCheckout()
    {
        return _repository.StateCheckout();
    }
}
