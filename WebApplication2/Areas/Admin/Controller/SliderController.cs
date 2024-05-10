using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using WebApplication2.DataAccesLayer;


namespace MVC_intro.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController(MultiShopContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var data=await _context.Sliders
                .Where(x=> !x.IsDeleted)
                .Select(s=> new GetSliderVM
            {
                Discount = s.Discount,
                Id = s.Id,
                ImageUrl = s.ImageUrl,
                Subtitle = s.Subtitle,
                Title = s.Title
            }).ToListAsync();
            
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSliderVM vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }

            Slider slider = new Slider
            {
                Discount = vm.Discount,
                CreatedTime= DateTime.Now,
                ImageUrl = vm.ImageUrl,
                IsDeleted= false,
                Subtitle= vm.Subtitle,
                Title= vm.Title,
            };
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
