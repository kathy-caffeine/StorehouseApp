using StorehouseApp.Bll.Models;
using StorehouseApp.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorehouseApp.Bll.Services.Interfaces;

public interface IPalletService
{
    public void AddBox(BoxModel box);
    public void AddPallet(PalletModel pallet);
    public List<PalletModel> GetStatistics();
    public Dictionary<DateOnly, List<PalletModel>> GetRulledPalletList();
}
