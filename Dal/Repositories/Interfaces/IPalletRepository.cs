using StorehouseApp.Dal.Entities;

namespace StorehouseApp.Dal.Repositories.Interfaces;

public interface IPalletRepository
{
    public void Save(PalletEntity entity);
    public IReadOnlyList<PalletEntity> Query();
    public void Clear();
    public void AddBox(BoxEntity box);
    public void AddPallet(PalletEntity pallet);
    public void Serialize();
    public void Deserialize(string fileName);
    public bool StateCheckout();
}
