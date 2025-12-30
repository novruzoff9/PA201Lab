using Microsoft.AspNetCore.Identity;

namespace Pustok.Mvc.Models;

public class AppUser:IdentityUser
{
    public string FullName { get; set; }
}
