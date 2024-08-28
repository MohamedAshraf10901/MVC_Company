using Demo.DAL.Models;
using Demo.PL.Helper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Demo.Controllers;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region Sign Up
        [HttpGet]
        public IActionResult SignUp()
        {

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)  // server side validation
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user is null)
                {
                    user = await _userManager.FindByEmailAsync(model.Email);
                    if (user is null)
                    {

                        user = new ApplicationUser()
                        {

                            UserName = model.UserName,
                            Email = model.Email,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            IsAgree = model.IsAgree,
                            //ImageUrl = DocumentSettings.Upload(model.Image, "images")

                        };

                        var result = await _userManager.CreateAsync(user, model.Password);
                       
                        if (result.Succeeded)
                            return RedirectToAction(nameof(SignIn));

                        foreach (var item in result.Errors)
                            ModelState.AddModelError(string.Empty, item.Description);

                        return View(model);


                    }
                    ModelState.AddModelError("Errors", "Email is Already Exist");
                }
                ModelState.AddModelError("Errors", "User is Already Exist!");
            }


            return View(model);
        }
        #endregion

        #region Sign In

        [HttpGet]
        public IActionResult SignIn()
        { return View(); }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    if (await _userManager.CheckPasswordAsync(user, model.Password))
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe
                            , false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction(/*"Index"*/nameof(HomeController.Index), controllerName: "Home");
                        }

                    }
                    ModelState.AddModelError(string.Empty, "Wrong Password");
                    return View(model);

                }
                ModelState.AddModelError(string.Empty, "Email Not Exist!!");

            }
            return View(model);
        }

        #endregion

        #region Sign Out
        public new async Task<IActionResult> SignOut()
        {

            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
        #endregion

        #region Forget Password
        public IActionResult ForgetPassword()
        {
            return View();
        }


        public async Task<IActionResult> SendResetPasswardUrl(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    //generate token
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    //create url
                    var url = Url.Action(action: "ResetPassword", controller: "Account", new { email = model.Email, token = token }, Request.Scheme);
                    //create Email
                    var email = new Email()
                    {
                        Subject = "Reset Your Password",
						Recipients = model.Email,
                        Body = url
					};

					//Send Email

					EmailSettings.SendEmail(email);

					//redirect to action
					return RedirectToAction(nameof(CheckYourInbox));

                }
                ModelState.AddModelError(string.Empty, "Email Not Exist!!");

            }

            //return View(model);
			return View(nameof(ForgetPassword),model);

		}
        public IActionResult CheckYourInbox()
        {
            return View();
        }
        #endregion

        #region Reset Password
        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            TempData[key:"email"] = email;
            TempData[key:"token"] = token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var email = TempData[key: "email"] as string;
                var token = TempData[key: "token"] as string;

                var user = await _userManager.FindByEmailAsync(email);
                if (user is not null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                    if (result.Succeeded)
                    {
                        //return RedirectToAction(controllerName: "Home", actionName: nameof(HomeController.Index));
                        return RedirectToAction(nameof(SignIn));
                    }
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);

                        return View(model);

                    }

                }
                ModelState.AddModelError(string.Empty, "Invalid Reset Passward ");

                return View(model);
            }
            return View(model);
        }

        #endregion


        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
