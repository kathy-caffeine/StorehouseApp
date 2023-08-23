namespace StorehouseApp.Bll.Models;

public class BoxModel{
    public int id;
    public int width;
    public int height;
    public int length;
    public int weight;
    public DateOnly expirationDate;
    public DateOnly? productionDate;
    public BoxModel(int id, 
        int width, 
        int height, 
        int length, 
        int weight, 
        DateOnly productionDate)
    {
        this.id = id;
        this.width = width;
        this.height = height;
        this.length = length;
        this.weight = weight;
        expirationDate = productionDate.AddDays(100);
        this.productionDate = productionDate;
    }
    public BoxModel(int id, 
        int width, 
        int height, 
        int length, 
        int weight, 
        DateOnly expirationDate, 
        DateOnly? productionDate)
    {
        this.id = id;
        this.width = width;
        this.height = height;
        this.length = length;
        this.weight = weight;
        this.expirationDate = expirationDate;
        this.productionDate = productionDate;
    }
}