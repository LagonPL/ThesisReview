using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThesisReview.Data;
using ThesisReview.Data.Interface;
using ThesisReview.Data.Models;
using ThesisReview.Data.Repositories;

namespace ThesisReview
{
  public class Startup
  {
    private IConfigurationRoot _configurationRoot;
    public static string ConnectionString { get; private set; }
    public static string MailName { get; set; }
    public static string MailPassword { get; set; }
    public static string MailSMTP { get; set; }
    public static string MailPort { get; set; }

    public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
    {
      _configurationRoot = new ConfigurationBuilder().SetBasePath(hostingEnvironment.ContentRootPath).
        AddJsonFile("appsettings.json")
        .Build();
      Configuration = configuration;

    }

    public IConfiguration Configuration { get; }


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
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

      services.AddTransient<IAccountRepository, AccountRepository>();
      services.AddTransient<IListRepository, ListRepository>();
      services.AddTransient<IFormRepository, FormRepository>();
      services.AddTransient<IAdminRepository, AdminRepository>();
      services.AddTransient<IUserListRepository, UserListRepository>();

      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
      ConnectionString = Configuration["ConnectionStrings:DefaultConnection"];
      MailName = Configuration["ConnectionStrings:MailName"];
      MailPassword = Configuration["ConnectionStrings:MailPassword"];
      MailSMTP = Configuration["ConnectionStrings:MailSMTP"];
      MailPort = Configuration["ConnectionStrings:MailPort"];

      app.UseStatusCodePagesWithRedirects("/Error/{0}");
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
        routes.MapRoute(
                  name: "UserDelete",
                  template: "Admin/{action}/{id}", defaults: new { Controller = "Admin", action = "Delete", id = ""});
        routes.MapRoute(
                  name: "UserEdit",
                  template: "Admin/{action}/{id}", defaults: new { Controller = "Admin", action = "Edit", id = ""});
        routes.MapRoute(
                  name: "OrderList",
                  template: "List/{id}", defaults: new { Controller = "List", id = "" });
        routes.MapRoute(
                 name: "MailChange",
                 template: "Settings/{action}/{mail}/{token}", defaults: new { Controller = "Settings", action = "MailChange", mail = "", token = "" });

      });

      DbInitializer.Seed(userManager, roleManager);

    }
  }
}
