using Covid_App.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Covid_App.Services.Admin;

public interface IAdminService
{
    ActionResult<List<User>> getAllUsers();
}