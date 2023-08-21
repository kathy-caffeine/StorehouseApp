using StorehouseApp.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorehouseApp.Bll.Models;

public record PalletModel(
    int id,
    int width,
    int height,
    int length,
    int weight,
    List<BoxEntity> boxes,
    DateOnly expirationDate
)
{
    public sealed override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine("id: " + id);
        sb.AppendLine("ширина: " +  width);
        sb.AppendLine("высота: " +  height);
        sb.AppendLine("длинна: " +  length);
        sb.AppendLine("вес: " +  weight);
        sb.AppendLine("Число коробок: " +  boxes.Count);
        sb.AppendLine("срок годности: " +  expirationDate.ToString());
        return sb.ToString();
    }
}