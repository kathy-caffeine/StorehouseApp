using StorehouseApp.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorehouseApp.Dal.Repositories.Interfaces;

internal interface IPalletRepository
{
    public void Save(PalletEntity entity);
    public IReadOnlyList<PalletEntity> Query();
    public void Clear();
    public void AddBox(BoxEntity box);
    public void AddPallet(PalletEntity pallet);
    public List<List<PalletEntity>> RulledSortPallet();
    public List<PalletEntity> RulerPalletStatistics();
    public void Serialize();
    public void Deserialize(string fileName);
}
