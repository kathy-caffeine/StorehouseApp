using StorehouseApp.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorehouseApp.Bll.Models;

public class PalletModel{
    public int id;
    public int width;
    public int height;
    public int length;
    public int weight;
    public List<BoxEntity> boxes;
    public DateOnly expirationDate;
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

    public PalletModel(int id, 
        int width, 
        int height, 
        int length, 
        int weight, 
        List<BoxEntity> boxes, 
        DateOnly expirationDate)
    {
        this.id = id;
        this.width = width;
        this.height = height;
        this.length = length;
        this.weight = weight;
        this.boxes = boxes;
        this.expirationDate = expirationDate;
    }

    public PalletModel(int id,
        int width,
        int height,
        int length,
        int weight)
    {
        this.id = id;
        this.width = width;
        this.height = height;
        this.length = length;
        this.weight = weight;
        this.boxes = new();
        this.expirationDate = DateOnly.MaxValue;
    }
}