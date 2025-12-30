using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPageApp.Pages;

public class IndexModel : PageModel
{
    public string Name { get; set; }
    public string SurName { get; set; }
    public void OnGet()
    {
        Name = "Your Name";
        SurName = "Your Surname";
    }
}
