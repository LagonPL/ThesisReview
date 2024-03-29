﻿using Microsoft.AspNetCore.Identity;
using System.Linq;
using ThesisReview.Data.Models;
using ThesisReview.Data.Services;
using Microsoft.AspNetCore.Builder;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace ThesisReview.Data
{
  public class DbInitializer
  {

    public static void Seed(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IServiceProvider applicationBuilder)
    {
      AppDbContext context = applicationBuilder.GetRequiredService<AppDbContext>();
      context.Database.EnsureCreated();

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
          Department = "Administrator Główny",
          IsActive = true
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
