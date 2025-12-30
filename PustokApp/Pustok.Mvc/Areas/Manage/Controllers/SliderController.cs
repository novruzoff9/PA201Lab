using Microsoft.AspNetCore.Mvc;
using Pustok.Mvc.Data;
using Pustok.Mvc.Extensions;
using Pustok.Mvc.Models;

namespace Pustok.Mvc.Areas.Manage.Controllers;
[Area("Manage")]
public class SliderController(AppDbContext appDbContext) : Controller
{
    public IActionResult Index()
    {
        var sliders = appDbContext.Sliders.ToList();
        return View(sliders);
    }

    public IActionResult Details(int id)
    {
        var slider = appDbContext.Sliders.Find(id);
        if (slider == null) return NotFound();
        
        return PartialView("_DetailsPartial", slider);
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Slider slider)
    {
        if (!ModelState.IsValid)
            return View();
        if (slider.File==null)
        {
            ModelState.AddModelError("File", "Please select image file");
            return View();

        }
        slider.ImageUrl =slider.File.SaveFile("wwwroot/assets/image/bg-images");
        
        appDbContext.Sliders.Add(slider);
        appDbContext.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var slider = appDbContext.Sliders.Find(id);
        if (slider == null) return NotFound();
        appDbContext.Sliders.Remove(slider);
        appDbContext.SaveChanges();
        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/image/bg-images", slider.ImageUrl);
        FileManager.DeleteFile( path);
        return Ok();
    }
    public IActionResult Update(int id)
    {
        var slider = appDbContext.Sliders.Find(id);
        if (slider == null) return NotFound();
       
        return View(slider);
    }
    [HttpPost]
    public IActionResult Update(Slider slider)
    {
        if (!ModelState.IsValid)
            return View();
        var existSlider = appDbContext.Sliders.Find(slider.Id);
        if (existSlider == null) return NotFound();

        if (slider.File != null)
        {
           
            slider.ImageUrl = slider.File.SaveFile("wwwroot/assets/image/bg-images");
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/image/bg-images", existSlider.ImageUrl);
            FileManager.DeleteFile(path);
            existSlider.ImageUrl = slider.ImageUrl;

        }
         existSlider.Title = slider.Title;
        existSlider.Description = slider.Description;
        existSlider.ButtonText = slider.ButtonText;
        existSlider.ButtonUrl = slider.ButtonUrl;
        existSlider.Order = slider.Order;
        appDbContext.SaveChanges();
        return RedirectToAction("Index");
    }
}
