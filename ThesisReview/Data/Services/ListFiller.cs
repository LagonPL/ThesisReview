using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisReview.Data.Services
{
  public class ListFiller
  {
    //https://www.p.lodz.pl/pl/wydzialy-jednostki-naukowe
    public static List<string> DepartmentFiller()
    {
      var items = new List<string> { "Instytut Inżynierii Materiałowej",
      "Instytut Obrabiarek i Technologii Budowy Maszyn",
      "Instytut Maszyn Przepływowych"};

      return items;
    }

  }
}
