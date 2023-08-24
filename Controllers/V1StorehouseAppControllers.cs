using StorehouseApp.Bll.Services.Interfaces;
using StorehouseApp.Bll.Models;

namespace StorehouseApp.Controllers;

public class V1StorehouseAppControllers
{
    private readonly IPalletService _palletService;

    public V1StorehouseAppControllers(IPalletService palletService)
    {
        this._palletService = palletService;
    }

    public Dictionary<DateOnly, List<PalletModel>> GetRulledPalletList()
    {
        return _palletService.GetRulledPalletList();
    }

    public List<PalletModel> GetStatistics()
    {
        return _palletService.GetTopStatistics();
    }

    public void AddPallet(PalletModel pallet)
    {
        _palletService.AddPallet(pallet);
    }

    public void AddBox(BoxModel box)
    {
        _palletService.AddBox(box);
    }

    public void Serealize()
    {
        _palletService.Serealize();
    }

    public void Deserealize(string fileName)
    {
        _palletService.Deserealize(fileName);
    }

    public bool stateCheckout()
    {
        return _palletService.stateCheckout();
    }
}
