using Business.Exceptions;
using Business.Services.Abstracts;
using Business.Services.Concrates;
using Core.Models;
using Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GiftosMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class SliderController : Controller
    {
       private ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public IActionResult Index()
        {
            var sliders = _sliderService.GetAllSlider();
            return View(sliders);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                _sliderService.AddSlider(slider);
            }
            catch(ImageContentException ex)
            {
                ModelState.AddModelError("imageFile",ex.Message);
                return View();
            }
            catch(ImageLengthException ex)
            {
                ModelState.AddModelError("imageFile", ex.Message);
                return View();
            }
            catch(EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction("Index");   
        }
        public IActionResult Update(int id)
        {
            var slider = _sliderService.GetSlider(x => x.Id == id);
            if (slider == null) return NotFound("Slider tapilmadi!");
            return View(slider);
        }
        [HttpPost]
        public IActionResult Update(Slider slider)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                _sliderService.UpdateSlider(slider.Id, slider);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Business.Exceptions.FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ImageContentException ex)
            {
                ModelState.AddModelError("imageFile", ex.Message);
                return View();
            }
            catch (ImageLengthException ex)
            {
                ModelState.AddModelError("imageFile", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var existSlider=_sliderService.GetSlider(x=>x.Id == id);
            if (existSlider == null) return NotFound("Slider tapilmadi!");
            return View(existSlider);
        }
        [HttpPost]
        public IActionResult DeletePost(int id)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                _sliderService.DeleteSlider(id);
            }
            catch(EntityNotFoundException ex)
            {
                return NotFound(ex.Message) ;
            }
            catch (Business.Exceptions.FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }

    }
}
