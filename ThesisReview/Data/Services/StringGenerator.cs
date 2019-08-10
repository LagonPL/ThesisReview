using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThesisReview.Data.Models;

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

    public static Questions BasicQuestion()
    {

      var items = new Questions
      {
        Question1 = "Czy treść pracy odpowiada tematowy określonemu w tytule?",
        Question2 = "Ocena układu pracy, struktury podziału treści kolejnych rozdziałów",
        Question3 = "Merytoryczna ocena pracy",
        Question4 = "Czy i w jakim zakresie praca stanowi nowe ujęcie problemu",
        Question5 = "Charakterystyka doboru i wykorzystania źródeł",
        Question6 = "Ocena formalnej strony pracy (poprawność języka, opanowanie techniki pisania pracy, spis rzeczy, odsyłacze)",
        Question7 = "Sposób wykorzystania pracy (publikacja, udostępnienie instytucjom, materiał źródłowy)",
        Question8 = "Ocena pracy"
      };
      return items;
    }

    public static Questions AdvanceQuestion()
    {

      var items = new Questions
      {
        Question1 = "Czy treść pracy jest zgodna z tematem określonym w tytule?	",
        Question2 = "Czy praca jest zgodna z zakresem tematycznym studiów?",
        Question3 = "Czy cel określony w pracy został zrealizowany?",
        Question4 = "Czy układ pracy i struktura podziału treści są prawidłowe?",
        Question5 = "Czy praca zawiera spis treści i prawidłowe odsyłacze do źródeł?",
        Question6 = "Czy praca jest napisana poprawnym językiem?	",
        Question7 = "Czy dobór źródeł i ich wykorzystanie są prawidłowe?",
        Question8 = "Czy zostały osiągnięte założone efekty kształcenia dla pracy końcowej?",
        LongReview = "Krótka ocena merytoryczna: "
      };
      return items;
    }
    public static Questions MasterQuestion()
    {

      var items = new Questions
      {
        Question1 = "1. Sformułowanie celu (ów) pracy",
        Question2 = "2. Układ i struktura pracy",
        Question3 = "3. Sformułowanie problemu i hipotez",
        Question4 = "4. Trafność doboru metod i narzędzi badawczych",
        Question5 = "5. Nowatorstwo i oryginalność ujęcia problemu",
        Question6 = "1. Dobór i liczebność wykorzystanej literatury",
        Question7 = "2. Zakres wykorzystanych informacji (np. zakres empirycznych  badań własnych)",
        Question8 = "1. Poprawność językowa i technika pisania",
        Question9 = "2. Redakcja przypisów i odsyłaczy",
        Question0 = "3. Poprawność spisów treści, wykorzystanej literatury, graficznej prezentacji danych itp.",

      };
      return items;
    }

    public static List<string> ReviewTypesFiller()
    {
      var items = new List<string>
      {
        "Praca Inżynierska",
        "Praca Licencjacka",
        "Praca Magisterska",
        "Praca Doktorska"
      };
      return items;
    }

    public static List<Answers> AnswersGenerator()
    {
      var answers = new List<Answers>
      {
        new Answers{Answer = "W PELNI"},
        new Answers{Answer = "CZESCIOWO"},
        new Answers{Answer = "NIE"},
        new Answers{Answer = "NIE DOTYCZY"},
        new Answers{Answer = ""}
      };

      return answers;
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

    public static Questions GetQuestions(string reviewtype)
    {
      if(reviewtype.Equals("Praca Inzynierska") || reviewtype.Equals("Praca Licencjacka"))
      {
        var items = StringGenerator.BasicQuestion();
        return items;
      }
      else if(reviewtype.Equals("Praca Magisterska"))
      {
        var items = StringGenerator.MasterQuestion();
        return items;
      }
      else
      {
        var items = StringGenerator.AdvanceQuestion();
        return items;
      }
    }


  }
}
