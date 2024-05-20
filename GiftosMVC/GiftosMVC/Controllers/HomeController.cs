using Business.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GiftosMVC.Controllers
{
    public class HomeController : Controller
    {
        ISliderService _sliderService;

        public HomeController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public IActionResult Index()
        {
            var sliders= _sliderService.GetAllSlider();
            return View(sliders);
        }
    }
}
