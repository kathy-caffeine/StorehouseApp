using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorehouseApp.Bll.Models;

public record BoxModel(
    int id,
    int width,
    int height,
    int length,
    int weight,
    DateOnly expirationDate,
    DateOnly? productionDate
);