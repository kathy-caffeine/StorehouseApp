using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorehouseApp.Dal.Entities;

public class PalletEntity
{
    public int id;
    public int width;
    public int height;
    public int length;
    public int weight;
    public List<BoxEntity> boxes;
    public DateOnly expirationDate;

    public PalletEntity(int id, int width, int height, int length, int weight, List<BoxEntity> boxes, DateOnly expirationDate)
    {
        this.id = id;
        this.width = width;
        this.height = height;
        this.length = length;
        this.weight = weight;
        this.boxes = boxes;
        this.expirationDate = expirationDate;
    }
}