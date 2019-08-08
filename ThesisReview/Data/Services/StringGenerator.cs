using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThesisReview.Data.Services
{
  public class StringGenerator
  {
    //https://www.p.lodz.pl/pl/wydzialy-jednostki-naukowe
    //TODO: Uzupelnij to
    public static List<string> DepartmentFiller()
    {
      var items = new List<string> { "Instytut Inżynierii Materiałowej",
      "Instytut Obrabiarek i Technologii Budowy Maszyn",
      "Instytut Maszyn Przepływowych"};

      return items;
    }

    public static List<string> BasicQuestion()
    {

      var items = new List<string>
      {
        "Czy treść pracy odpowiada tematowy określonemu w tytule?",
        "Ocena układu pracy, struktury podziału treści kolejnych rozdziałów",
        "Merytoryczna ocena pracy",
        "Czy i w jakim zakresie praca stanowi nowe ujęcie problemu",
        "Charakterystyka doboru i wykorzystania źródeł",
        "Ocena formalnej strony pracy (poprawność języka, opanowanie techniki pisania pracy, spis rzeczy, odsyłacze)",
        "Sposób wykorzystania pracy (publikacja, udostępnienie instytucjom, materiał źródłowy)",
        "Ocena pracy"
      };
      return items;
    }

    public static List<string> AdvanceQuestion()
    {

      var items = new List<string>
      {
        "Czy treść pracy jest zgodna z tematem określonym w tytule?	",
        "Czy praca jest zgodna z zakresem tematycznym studiów?",
        "Czy cel określony w pracy został zrealizowany?",
        "Czy układ pracy i struktura podziału treści są prawidłowe?",
        "Czy praca zawiera spis treści i prawidłowe odsyłacze do źródeł?",
        "Czy praca jest napisana poprawnym językiem?	",
        "Czy dobór źródeł i ich wykorzystanie są prawidłowe?",
        "Czy zostały osiągnięte założone efekty kształcenia dla pracy końcowej?"
      };
      return items;
    }

    public static string LinkGenerator(UriBuilder uri, string gid)
    {
      string url;

      url = uri.Host.ToString().Replace("[", "");
      url = url.Replace("]", "");
      url = url + uri.Path;
      url = url + gid;
      url = "https://" + url;

      return url;
    }


  }
}
