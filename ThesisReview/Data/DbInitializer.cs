using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThesisReview.Data.Models;
using ThesisReview.Data.Services;

namespace ThesisReview.Data
{
  public class DbInitializer
  {

    public static void Seed(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
      if (!roleManager.RoleExistsAsync("Admin").Result)
      {
        IdentityRole role = new IdentityRole
        {
          Name = "Admin"
        };
        IdentityResult roleResult = roleManager.
        CreateAsync(role).Result;
      }


      if (!roleManager.RoleExistsAsync("Reviewer").Result)
      {
        IdentityRole role = new IdentityRole
        {
          Name = "Reviewer"
        };
        IdentityResult roleResult = roleManager.
        CreateAsync(role).Result;
      }

      if (!userManager.Users.Any())
      {
        ApplicationUser user = new ApplicationUser
        {
          UserName = "admin",
          Email = "recenzjeprac@gmail.com",
          Department = "Administrator Główny"
        };

        IdentityResult result = userManager.CreateAsync(user, "admin").Result;

        if (result.Succeeded)
        {
          userManager.AddToRoleAsync(user, "Admin").Wait();
          string content = "Drogi użytkowniku.\nDostałeś właśnie dostęp do strony Recenzje Prac i jesteś pierwszym adminem.\nTwój i hasło to: admin. \nZalecamy zmianę hasła na bardziej bezpieczne.\nPozdrawiam Dawid Sowała - Twórca";
          EmailSender.Send(user.Email, "ThesisReview - Administrator", content);
        }
      }

    }



  }
}
