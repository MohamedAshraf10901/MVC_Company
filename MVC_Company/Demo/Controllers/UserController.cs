using AutoMapper;
using Demo.BLL;
using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Demo.PL.Helper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
	[Authorize]
	public class UserController : Controller
	{
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public UserController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }


        public async Task<IActionResult> Index(string SearchInput)
        {
            var users = mapper.Map<IEnumerable<UserViewModel>>(await userManager.Users.Select(u => new UserViewModel()
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                //ImageUrl = u.ImageUrl,
                FirstName = u.FirstName,
                LastName = u.LastName,

            }).ToListAsync());
            if (SearchInput is not null)
                users = mapper.Map<IEnumerable<UserViewModel>>(await userManager.Users.Where(u => u.NormalizedEmail.Contains(SearchInput.ToLower())).Select(u => new UserViewModel()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    //ImageUrl = u.ImageUrl,
                    FirstName = u.FirstName,
                    LastName = u.LastName,

                }).ToListAsync());
            return View(users);
        }



        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();   //400 ERROR

            var user = await userManager.FindByIdAsync(id);
            
            if (user is null)
                return NotFound();  //404             

            var userr = new UserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,

            };


            return View(ViewName, userr);
       
        }




        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id,ViewName: "Edit");

        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = mapper.Map<ApplicationUser>(model);

                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    TempData["Updated"] = $"{user.UserName} has been updated ";
                    return RedirectToAction("Index");
                }
                foreach (var item in result.Errors)
                    ModelState.AddModelError(string.Empty, item.Description);
                return View(model);
            }
            return View(model);
        }




        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var result = await userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        TempData["message"] = $"{user.UserName} has been deleted";
                        return RedirectToAction(nameof(Index));
                    }
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }

                    return View(model);
                }
                return View(model);

            }
            return View(model);
        }


    }
}
