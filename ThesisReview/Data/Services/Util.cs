using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThesisReview.Data.Models;

namespace ThesisReview.Data.Services
{
  public class Util
  {
    public static Sum Sum(Form form)
    {
      int x = 0;
      var suma = new Sum
      {
        Sum1 = 0, Sum2 = 0, Sum3 = 0, SumFinal = 0
      };
      var sumaGuardian = new Sum
      {
        Sum1 = 0, Sum2 = 0, Sum3 = 0, SumFinal = 0
      };
      if (Int32.TryParse(form.Questions.Question1, out x))
      {
        suma.Sum1 = suma.Sum1 + Int32.Parse(form.Questions.Question1);
      }
      if (Int32.TryParse(form.Questions.Question2, out x))
      {
        suma.Sum1 = suma.Sum1 + Int32.Parse(form.Questions.Question2);
      }
      if (Int32.TryParse(form.Questions.Question3, out x))
      {
        suma.Sum1 = suma.Sum1 + Int32.Parse(form.Questions.Question3);
      }
      if (Int32.TryParse(form.Questions.Question4, out x))
      {
        suma.Sum1 = suma.Sum1 + Int32.Parse(form.Questions.Question4);
      }
      if (Int32.TryParse(form.Questions.Question5, out x))
      {
        suma.Sum1 = suma.Sum1 + Int32.Parse(form.Questions.Question5);
      }
      if (Int32.TryParse(form.Questions.Question6, out x))
      {
        suma.Sum2 = suma.Sum2 + Int32.Parse(form.Questions.Question6);
      }
      if (Int32.TryParse(form.Questions.Question7, out x))
      {
        suma.Sum2 = suma.Sum2 + Int32.Parse(form.Questions.Question7);
      }
      if (Int32.TryParse(form.Questions.Question8, out x))
      {
        suma.Sum3 = suma.Sum3 + Int32.Parse(form.Questions.Question8);
      }
      if (Int32.TryParse(form.Questions.Question9, out x))
      {
        suma.Sum3 = suma.Sum3 + Int32.Parse(form.Questions.Question9);
      }
      if (Int32.TryParse(form.Questions.Question0, out x))
      {
        suma.Sum3 = suma.Sum3 + Int32.Parse(form.Questions.Question0);
      }

      suma.SumFinal = suma.Sum1 + suma.Sum2 + suma.Sum3;

      return suma;
    }
    public static Sum SumGuardian(Form form)
    {
      int x = 0;
      var suma = new Sum
      {
        Sum1 = 0,
        Sum2 = 0,
        Sum3 = 0,
        SumFinal = 0
      };
      if (Int32.TryParse(form.QuestionsGuardian.Question1, out x))
      {
        suma.Sum1 = suma.Sum1 + Int32.Parse(form.QuestionsGuardian.Question1);
      }
      if (Int32.TryParse(form.QuestionsGuardian.Question2, out x))
      {
        suma.Sum1 = suma.Sum1 + Int32.Parse(form.QuestionsGuardian.Question2);
      }
      if (Int32.TryParse(form.QuestionsGuardian.Question3, out x))
      {
        suma.Sum1 = suma.Sum1 + Int32.Parse(form.QuestionsGuardian.Question3);
      }
      if (Int32.TryParse(form.QuestionsGuardian.Question4, out x))
      {
        suma.Sum1 = suma.Sum1 + Int32.Parse(form.QuestionsGuardian.Question4);
      }
      if (Int32.TryParse(form.QuestionsGuardian.Question5, out x))
      {
        suma.Sum1 = suma.Sum1 + Int32.Parse(form.QuestionsGuardian.Question5);
      }
      if (Int32.TryParse(form.QuestionsGuardian.Question6, out x))
      {
        suma.Sum2 = suma.Sum2 + Int32.Parse(form.QuestionsGuardian.Question6);
      }
      if (Int32.TryParse(form.QuestionsGuardian.Question7, out x))
      {
        suma.Sum2 = suma.Sum2 + Int32.Parse(form.QuestionsGuardian.Question7);
      }
      if (Int32.TryParse(form.QuestionsGuardian.Question8, out x))
      {
        suma.Sum3 = suma.Sum3 + Int32.Parse(form.QuestionsGuardian.Question8);
      }
      if (Int32.TryParse(form.QuestionsGuardian.Question9, out x))
      {
        suma.Sum3 = suma.Sum3 + Int32.Parse(form.QuestionsGuardian.Question9);
      }
      if (Int32.TryParse(form.QuestionsGuardian.Question0, out x))
      {
        suma.Sum3 = suma.Sum3 + Int32.Parse(form.QuestionsGuardian.Question0);
      }

      suma.SumFinal = suma.Sum1 + suma.Sum2 + suma.Sum3;

      return suma;
    }

    public static bool QuestionsChecker(Questions questions)
    {
      return true;
    }

  }
}
