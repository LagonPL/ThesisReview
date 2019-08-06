using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThesisReview.Data.Models;

namespace ThesisReview.Data
{
  public class AppDbContext : IdentityDbContext<IdentityUser>
  {

    public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
    {

    }

    public DbSet<Form> Forms { get; set; }

  }
}
