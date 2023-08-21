using StorehouseApp.Bll.Models;
using StorehouseApp.Bll.Services.Interfaces;
using StorehouseApp.Dal.Entities;
using StorehouseApp.Dal.Repositories.Interfaces;

namespace StorehouseApp.Bll.Services;

internal class PalletService : IPalletService
{
    private readonly IPalletRepository _repository;
    public PalletService(IPalletRepository repository)
    {
        this._repository = repository;
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
        var data = _repository.RulledSortPallet();
        var dictionary = new Dictionary<DateOnly, List<PalletModel>>();
        foreach (var item in data)
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

    public List<PalletModel> GetStatistics()
    {
        return _repository.RulerPalletStatistics()
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
}
