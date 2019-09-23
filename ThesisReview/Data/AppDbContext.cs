using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ThesisReview.Data.Models;

namespace ThesisReview.Data
{
  public class AppDbContext : IdentityDbContext<ApplicationUser>
  {

    public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
    {

    }

    public DbSet<Form> Forms { get; set; }
    public DbSet<Questions> Questions { get; set; }
    public DbSet<UserList> UserLists { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<RequestForm> RequestForms { get; set; }
  }
}
