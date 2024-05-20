using Core.Models;
using GiftosMVC.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GiftosMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(MemberRegisterVM memberRegisterVM)
        {
            if(!ModelState.IsValid) return View();
            AppUser user = null;
            user= await _userManager.FindByNameAsync(memberRegisterVM.Name);
            if(user != null) { ModelState.AddModelError("", "bele bir name var!"); return View(); }
            user = await _userManager.FindByEmailAsync(memberRegisterVM.Email);
            if (user != null) { ModelState.AddModelError("", "bele bir email var!"); return View(); }
            user = new AppUser()
            {
                UserName= memberRegisterVM.Name,
                Email= memberRegisterVM.Email,
            };
            var result= await _userManager.CreateAsync(user,memberRegisterVM.Password);
            if(!result.Succeeded) 
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                    return View();
                }
            }
            await _userManager.AddToRoleAsync(user, "Member");
            return RedirectToAction("Login");
        }
    }
}
