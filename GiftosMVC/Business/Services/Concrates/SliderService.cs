using Business.Exceptions;
using Business.Extensions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrates
{
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;
        private readonly IWebHostEnvironment _env;

        public SliderService(ISliderRepository sliderRepository, IWebHostEnvironment env)
        {
            _sliderRepository = sliderRepository;
            _env = env;
        }

        public void AddSlider(Slider slider)
        {
            if (slider == null) throw new EntityNotFoundException("Slider tapilmadi!");
            slider.ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads\sliders", slider.ImageFile);
            _sliderRepository.Add(slider);
            _sliderRepository.Commit();
        }

        public void DeleteSlider(int id)
        {
           var existSlider= _sliderRepository.Get(x=>x.Id==id);
            if(existSlider == null) throw new EntityNotFoundException("Slider tapilmadi!");
            Helper.DeleteFile(_env.WebRootPath , @"uploads\sliders", existSlider.ImageUrl);
            _sliderRepository.Delete(existSlider);
            _sliderRepository.Commit();
        }

        public List<Slider> GetAllSlider(Func<Slider, bool>? func = null)
        {
            return _sliderRepository.GetAll(func);
        }

        public Slider GetSlider(Func<Slider, bool>? func = null)
        {
            return _sliderRepository.Get(func);
        }

        public void UpdateSlider(int id, Slider slider)
        {
            var oldSlider = _sliderRepository.Get(x => x.Id == id);
            if (oldSlider == null) throw new EntityNotFoundException("Slider tapilmadi!");
            if(slider.ImageFile != null)
            {
                Helper.DeleteFile(_env.WebRootPath, @"uploads\sliders", oldSlider.ImageUrl);
                oldSlider.ImageUrl=Helper.SaveFile(_env.WebRootPath, @"uploads\sliders",slider.ImageFile);

            }
            oldSlider.Title= slider.Title;
            oldSlider.Description= slider.Description;
            oldSlider.RedirectUrl= slider.RedirectUrl;
            _sliderRepository.Commit();
        }
    }
}
