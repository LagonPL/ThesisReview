﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThesisReview.Data;
using ThesisReview.Data.Interface;
using ThesisReview.Data.Models;

namespace ThesisReview
{
  public class Startup
  {
    private IConfigurationRoot _configurationRoot;

    public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
    {
      _configurationRoot = new ConfigurationBuilder().SetBasePath(hostingEnvironment.ContentRootPath).
        AddJsonFile("appsettings.json")
        .Build();
      Configuration = configuration;

    }

    public IConfiguration Configuration { get; }


    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<IdentityOptions>(options =>
      {
        // Default Password settings.
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 4;
        options.Password.RequiredUniqueChars = 0;
      });

      services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(_configurationRoot.GetConnectionString("DefaultConnection")));

      services.Configure<CookiePolicyOptions>(options =>
      {
              // This lambda determines whether user consent for non-essential cookies is needed for a given request.
              options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });

      services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>();

      services.AddTransient<IListRepository, ListRepository>();

      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseCookiePolicy();
      app.UseAuthentication();
      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");
        routes.MapRoute(
                  name: "FormEdit",
                  template: "Form/{action}/{id}", defaults: new { Controller = "Form", action = "Edit", id = "" });
        routes.MapRoute(
                  name: "FormView",
                  template: "Form/{action}/{id}/{password}", defaults: new { Controller = "Form", action = "View", id = "", password = "" });
      });

      DbInitializer.Seed(userManager, roleManager);

    }
  }
}
