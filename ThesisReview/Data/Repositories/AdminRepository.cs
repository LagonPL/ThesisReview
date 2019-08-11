﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThesisReview.Data.Interface;
using ThesisReview.Data.Models;

namespace ThesisReview.Data.Repositories
{
  public class AdminRepository : IAdminRepository
  {
    private readonly AppDbContext _appDbContext;
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminRepository(AppDbContext appDbContext, UserManager<ApplicationUser> userManager)
    {
      _appDbContext = appDbContext;
      _userManager = userManager;
    }

    public void DeleteUser(string useId)
    {
      var deleteduser = _appDbContext.Users.FirstOrDefault(p => p.Email == useId);
      _appDbContext.Users.Remove(deleteduser);
      _appDbContext.SaveChanges();
    }
    //TODO: Password Reset
    public void EditUser(string useId)
    {
      var changeuser = _appDbContext.Users.FirstOrDefault(p => p.Email == useId);
    }

    public IEnumerable<ApplicationUser> GetAllUser() => _appDbContext.Users;
  }
}