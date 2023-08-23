using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StorehouseApp.Dal.Entities;

public record BoxEntity(
    int id,
    int width,
    int height,
    int length,
    int weight,
    DateOnly expirationDate,
    DateOnly? productionDate
)
{
    public sealed override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine(id + " " + 
            width + " " + 
            height + " " + 
            length + " " + 
            weight + " " + 
            expirationDate + " " + 
            productionDate);
        return sb.ToString();
    }
}