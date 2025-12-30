using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MimeKit;
using MimeKit.Text;
using Pustok.Mvc.Data;
using Pustok.Mvc.Models;
using Pustok.Mvc.Services;
using Pustok.Mvc.ViewModels.Users;
using System.Security.Cryptography;
using System.Threading.Tasks;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Pustok.Mvc.Controllers;
public class AccountController(
    UserManager<AppUser> userManager,
    RoleManager<IdentityRole> roleManager,
    SignInManager<AppUser> signInManager,
    IEmailService emailService,
    AppDbContext context
    ) : Controller
{
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(UserRegisterVm userRegisterVm)
    {
        if (!ModelState.IsValid)
        {
            return View(userRegisterVm);
        }
        var user = await userManager.FindByNameAsync(userRegisterVm.UserName);
        if (user != null)
        {
            ModelState.AddModelError("UserName", "Username already exists");
            return View(userRegisterVm);
        }
        user = new()
        {
            FullName = userRegisterVm.FullName,
            UserName = userRegisterVm.UserName,
            Email = userRegisterVm.Email
        };
        IdentityResult identityResult = await userManager.CreateAsync(user, userRegisterVm.Password);
        if (!identityResult.Succeeded)
        {
            foreach (var error in identityResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(userRegisterVm);

        }
        await userManager.AddToRoleAsync(user, "Member");
        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmationLink = Url.Action("ConfirmEmail", "Account", new { email = user.Email, token = token }, Request.Scheme);


        FileStream fileStream = new FileStream("wwwroot/EmailTemplates/Emailconfirmation.html", FileMode.Open);
        using StreamReader reader = new StreamReader(fileStream);
        var emailBody = await reader.ReadToEndAsync();
        emailBody = emailBody.Replace("{{user_name}}", user.UserName);
        emailBody = emailBody.Replace("{{confirm_url}}", confirmationLink!);


        await emailService.SendEmailAsync(user.Email, "Confirm your email", emailBody, true);

        return RedirectToAction("Login");
    }
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(UserLoginVm userLoginVm)
    {
        if (!ModelState.IsValid)
            return View(userLoginVm);
        var user = await userManager.FindByNameAsync(userLoginVm.UserNameOrEmail);
        if (user == null)
        {
            user = await userManager.FindByEmailAsync(userLoginVm.UserNameOrEmail);
            if (user == null)
            {
                ModelState.AddModelError("", "Username or email is incorrect");
                return View(userLoginVm);
            }
        }
        if (await userManager.IsInRoleAsync(user, "Admin"))
        {

            ModelState.AddModelError("", "Username or email is incorrect");
            return View(userLoginVm);
        }
        var result = await signInManager.PasswordSignInAsync(user, userLoginVm.Password, userLoginVm.RememberMe, true);
        if(!user.EmailConfirmed)
        {
            ModelState.AddModelError("", "Please confirm your email address before logging in.");
            return View(userLoginVm);
        }
        if (result.IsLockedOut)
        {
            ModelState.AddModelError("", "Your account is locked out. Please try again later.");
            return View(userLoginVm);
        }
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Username or password is incorrect");
            return View(userLoginVm);

        }

        //var result = await userManager.CheckPasswordAsync(user, userLoginVm.Password);
        //if (!result)
        //{
        //    ModelState.AddModelError("", "Password is incorrect");
        //    return View(userLoginVm);
        //}
        //await signInManager.SignInAsync(user, isPersistent: userLoginVm.RememberMe);
        return RedirectToAction("Index", "Home");
    }

    [Authorize(Roles = "Member")]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    //public async Task<IActionResult> CreateRole()
    //{
    //    await roleManager.CreateAsync(new IdentityRole("Member"));
    //    await roleManager.CreateAsync(new IdentityRole("Admin"));
    //    return Content("Roles created successfully");
    //}
    public IActionResult Profile()
    {
        return View();
    }

    public async Task<IActionResult> ConfirmEmail(string email, string token)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
            return BadRequest();
        var result = await userManager.ConfirmEmailAsync(user, token);

        if (result.Succeeded)
        {
            await userManager.UpdateSecurityStampAsync(user);
            return RedirectToAction(nameof(Login));
        }
        return BadRequest();
    }

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    //[HttpPost]
    //public async Task<IActionResult> ForgotPassword(ForgotPasswordVm forgotPasswordVm)
    //{
    //    if (!ModelState.IsValid) { 
    //        return View(forgotPasswordVm);
    //    }
    //    var user = await userManager.FindByEmailAsync(forgotPasswordVm.Email);
    //    if (user == null)
    //    {
    //        ModelState.AddModelError("", "Bu email-de istifadeci yoxdur");
    //        return View(forgotPasswordVm);
    //    }
    //    var token = await userManager.GeneratePasswordResetTokenAsync(user);
    //    var resetLink = Url.Action("ResetPassword", "Account", new { email = user.Email, token = token }, Request.Scheme);


    //    FileStream fileStream = new FileStream("wwwroot/EmailTemplates/PasswordReset.html", FileMode.Open);
    //    using StreamReader reader = new StreamReader(fileStream);
    //    var emailBody = await reader.ReadToEndAsync();
    //    emailBody = emailBody.Replace("{{user_name}}", user.UserName);
    //    emailBody = emailBody.Replace("{{reset_url}}", resetLink!);

    //    await emailService.SendEmailAsync(user.Email, "Reset your password", emailBody, true);

    //    return RedirectToAction(nameof(Login));
    //}
    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordVm forgotPasswordVm)
    {
        if (!ModelState.IsValid)
        {
            return View(forgotPasswordVm);
        }
        var user = await userManager.FindByEmailAsync(forgotPasswordVm.Email);
        if (user == null)
        {
            ModelState.AddModelError("", "Bu email-de istifadeci yoxdur");
            return View(forgotPasswordVm);
        }


        var code = RandomNumberGenerator.GetInt32(100000, 1000000).ToString();

        PasswordResetToken resetToken = new()
        {
            Key = code,
            ExpireDate = DateTime.UtcNow.AddMinutes(15),
            UserEmail = user.Email
        };



        FileStream fileStream = new FileStream("wwwroot/EmailTemplates/PasswordResetWithCode.html", FileMode.Open);
        using StreamReader reader = new StreamReader(fileStream);
        var emailBody = await reader.ReadToEndAsync();
        emailBody = emailBody.Replace("{{user_name}}", user.UserName);
        emailBody = emailBody.Replace("{{reset_code}}", code);
        emailBody = emailBody.Replace("{{expiry_minutes}}", 15.ToString());

        await emailService.SendEmailAsync(user.Email, "Reset your password", emailBody, true);
        await context.PasswordResetTokens.AddAsync(resetToken);
        await context.SaveChangesAsync();

        return RedirectToAction(nameof(ResetPasswordWithCode), new { email = user.Email });
    }

    [HttpGet]
    public IActionResult ResetPasswordWithCode(string email)
    {
        ResetPasswordWithCodeVm resetPasswordWithCodeVm = new ResetPasswordWithCodeVm
        {
            Email = email
        };
        return View(resetPasswordWithCodeVm);
    }

    [HttpPost]
    public async Task<IActionResult> ResetPasswordWithCode(ResetPasswordWithCodeVm resetPasswordWithCodeVm)
    {
        if (!ModelState.IsValid)
            return View(resetPasswordWithCodeVm);
        
        var user = await userManager.FindByEmailAsync(resetPasswordWithCodeVm.Email);
        if (user == null)
            return BadRequest();
        var resetToken = await context.PasswordResetTokens.FirstOrDefaultAsync(x => x.UserEmail == user.Email);
        if (resetToken == null || resetToken.ExpireDate < DateTime.UtcNow || resetToken.Key != resetPasswordWithCodeVm.Code)
        {
            ModelState.AddModelError("", "Invalid or expired reset code.");
            return View(resetPasswordWithCodeVm);
        }
        var token = await userManager.GeneratePasswordResetTokenAsync(user);

        context.PasswordResetTokens.Remove(resetToken);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(ResetPassword), new { email = user.Email, token = token });
    }

    [HttpGet]
    public async Task<IActionResult> ResetPassword(string email, string token)
    {
        var user = await userManager.FindByEmailAsync(email);
        if( user == null)
            return BadRequest();
        var result = await userManager.VerifyUserTokenAsync(user, userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token);
        if (!result)
            return BadRequest();
        ResetPasswordVm resetPasswordVm = new ResetPasswordVm
        {
            Email = email,
            Token = token
        };
        return View(resetPasswordVm);
    }
    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordVm resetPasswordVm)
    {
        if (!ModelState.IsValid)
            return View(resetPasswordVm);
        var user = await userManager.FindByEmailAsync(resetPasswordVm.Email);
        if (user == null)
            return BadRequest();
        var result = await userManager.ResetPasswordAsync(user, resetPasswordVm.Token, resetPasswordVm.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(resetPasswordVm);
        }
        await userManager.UpdateSecurityStampAsync(user);
        return RedirectToAction(nameof(Login));
    }
}