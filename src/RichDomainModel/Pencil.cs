using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichDomainModel;

public class Pencil
{
    private Pencil(int id, string brand, bool bold)
    {
        Id = id;
        Brand = brand;
        Bold = bold;
    }
    public int Id { get; private set; }
    public string Brand { get; private set; }
    public bool Bold { get; private set; }

    public static Pencil Create(int id, string brand, bool bold)
    {
        return new Pencil(id,brand,bold);
    }


}