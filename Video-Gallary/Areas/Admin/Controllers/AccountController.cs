using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Video_Gallary.Areas.Admin.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Video_Gallary.Areas.Admin.Models;
using Video_Gallary.Models;
using Microsoft.AspNetCore.Authorization;

namespace Video_Gallary.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<IdentityCustomeUser> userManager;
        private SignInManager<IdentityCustomeUser> signInManager;
        private readonly ApplicationDbContext context;

        public AccountController(UserManager<IdentityCustomeUser> _userManager, SignInManager<IdentityCustomeUser> _signInManager, ApplicationDbContext _context)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            context = _context;
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //success
                var user = context.Users.SingleOrDefault(e => e.UserName == model.Email);
                if (user != null)
                {
                    //   var result=await  signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                    var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false,false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home",new { @Area="Admin" });
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login credentials !");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid User !");
                    return View();
                }
                
            }
            else
            {
                return View(model);
            }
          
        }

        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                //success
                var user = new IdentityCustomeUser()
                {
                FirstName=model.FirstName,
                LastName=model.LastName,
                Email=model.Email,
                UserName=model.Email,
                PasswordHash=model.Password,
                PhoneNumber=model.Phone,
                Gender=model.Gender

                };
                IdentityResult result = await userManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                {
                    ViewBag.message = "User created successfully !";
                    return View();
                }
                else
                {
                    foreach(var er in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, er.Description);
                    }
                    return View();
                }

            }
            else
            {
                return View(model);
            }
            
        }

        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
      
    }
}
