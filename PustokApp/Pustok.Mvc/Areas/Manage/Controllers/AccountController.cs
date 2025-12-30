using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pustok.Mvc.Areas.Manage.ViewModels;
using Pustok.Mvc.Models;
using Pustok.Mvc.ViewModels.Users;
using System.Threading.Tasks;

namespace Pustok.Mvc.Areas.Manage.Controllers;
[Area("Manage")]
public class AccountController(
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager
    ) : Controller
{
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(AdminLoginVm adminLoginVm)
    {
        if (!ModelState.IsValid) return View(adminLoginVm);
        AppUser user = await userManager.FindByNameAsync(adminLoginVm.UserName);
        if (user is null)
        {
            ModelState.AddModelError("", "Username or password is incorrect");
            return View(adminLoginVm);
        }
        var result = await userManager.CheckPasswordAsync(user, adminLoginVm.Password);
        if (!result)
        {
            ModelState.AddModelError("", "Username or password is incorrect");
            return View(adminLoginVm);
        }
        if (await userManager.IsInRoleAsync(user, "Member"))
        {

            ModelState.AddModelError("", "Username or email is incorrect");
            return View(adminLoginVm);
        }
        await signInManager.SignInAsync(user, isPersistent: false);

        return RedirectToAction("Index", "Dashboard");
    }
    [Authorize(Roles = "Admin")]
    public IActionResult Logout()
    {
        signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UserProfile()
    {
        //var user =await userManager.GetUserAsync(User);
        if (User.Identity.IsAuthenticated)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user is null) return NotFound();
        return Json(user);
        }
        return NotFound("User is not authenticated");
    }
    //public async Task<IActionResult> CreateAdmin()
    //{
    //    AppUser admin = new()
    //    {
    //        UserName = "_admin",
    //        Email = "admin@gmail.com",
    //        FullName = "Admin User"
    //    };
    //    var result = await userManager.CreateAsync(admin, "_Admin123");
    //    if (!result.Succeeded)
    //        return Json(result.Errors);
    //    await userManager.AddToRoleAsync(admin, "Admin");
    //    return Json(result);

    //}
}
