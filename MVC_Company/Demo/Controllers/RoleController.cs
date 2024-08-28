using AutoMapper;
using Demo.DAL.Models;
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
    [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, IMapper mapper,UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.mapper = mapper;
            this.userManager = userManager;
        }
        public IActionResult Index(string seachr)
        {
            var dbrole = roleManager.Roles;

            var roles = dbrole.Select(r => new RoleViewModel() { Id = r.Id, Name = r.Name });
            if (!string.IsNullOrEmpty(seachr))
                roles = dbrole.Where(r => r.NormalizedName.Contains(seachr.ToUpper())).Select(r => new RoleViewModel() { Id = r.Id, Name = r.Name });
            ViewData["search"] = seachr;
            return View(roles);
        }

        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await roleManager.FindByNameAsync(model.Name);
                if (role is null)
                {
                    role = new IdentityRole() { Name = model.Name };
                    var result = await roleManager.CreateAsync(role);
                    if (result.Succeeded) return RedirectToAction(nameof(Index));
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);

                    return View(model);

                }
                ModelState.AddModelError(string.Empty, "Role Already Exist");
                return View(model);
            }
            return View(model);
        }

        public async Task<IActionResult> Details(string id, string viewname = "Details")
        {
            var dbrole = await roleManager.FindByIdAsync(id.ToString());
            var role = new RoleViewModel() { Id = dbrole.Id, Name = dbrole.Name };
            return View(viewname, role);
        }
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole() { Name = model.Name, Id = model.Id };
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(model);
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await roleManager.FindByIdAsync(model.Id.ToString());
                var result = await roleManager.DeleteAsync(role);
                if (result.Succeeded) return RedirectToAction(nameof(Index));
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(model);
            }
            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if(role is null)
                return NotFound();


            ViewData["RoleId"] = roleId;



            var usersInRole = new List<UsersInRoleViewModel>();            
            var users = await userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var userInRole = new UsersInRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };

                if(await userManager.IsInRoleAsync(user, role.Name))
                {
                    userInRole.IsSelected = true;
                }
                else
                {
                    userInRole.IsSelected= false;
                }

                usersInRole.Add(userInRole);

            }
            return View(usersInRole);

        }


        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId,List<UsersInRoleViewModel> users)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role is null)
                return NotFound();

            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appUser = await userManager.FindByIdAsync(user.UserId);
                    if(appUser is not null)
                    {
                        if (user.IsSelected && ! await userManager.IsInRoleAsync(appUser,role.Name))
                        {
                            await userManager.AddToRoleAsync(appUser,role.Name);
                        }
                        else if (!user.IsSelected && await userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await userManager.RemoveFromRoleAsync(appUser, role.Name);

                        }
                    }
                }

                return RedirectToAction(nameof(Edit), new { id = roleId });
            }
            return View(users);

        }
    }
}
