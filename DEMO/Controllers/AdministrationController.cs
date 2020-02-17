using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DEMO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DEMO.Controllers
{
    [Authorize]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateRole()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [Authorize(Policy = "DeleteRolePolicy")]
        [HttpPost]
        [Authorize(Policy = "CreateRolePolicy")]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName,
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRole", "Administration");

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListRole()
        {
            var role = roleManager.Roles;
            return View(role);
        }
        [HttpGet]
        [Authorize(Policy= "EditRolePolicy")]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            var model = new EditRoleViewModel

            {
                Id = role.Id,
                RoleName = role.Name

            };
            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [Authorize(Policy = "EditRolePolicy")]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found ";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRole", "Administration");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditeUserInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with this Id = {roleId} cannot be found";
                return View("NotFound");
            }
            var model = new List<UserRoleViewModel>();
            foreach (var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;

                }
                model.Add(userRoleViewModel);
            }
            return View(model);
        }


        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditeUserInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot ne found";
                return View("NotFound");
            }
            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && (await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                /*if(result.Succeeded)
                {
                    if (i < (model.Count - 1))
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("EditRole",new { Id = roleId });
                    }
                }*/
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> AllUsersRole()
        {
            ViewBag.role = roleManager.Roles.Select(s => new RoleUserViewModel
            {
                RoleName = s.Name,

            });
            ViewBag.user = userManager.Users.Select(s => new RoleUserViewModel
            {
                UserName = s.UserName,
            });
            return View();
        }

        [HttpGet]
        public IActionResult DeleteRole()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy ="DeleteRolePolicy")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with this Id = {id} Can not be found";
                return View("NotFound");
            }
            else
            {
                var result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRole");

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return RedirectToAction("ListRole");
            }
        }
        [HttpGet]
        public async Task<IActionResult> AllUser()
        {
            var user = userManager.Users;
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            var userClaims = await userManager.GetClaimsAsync(user);
            var userRoles = await userManager.GetRolesAsync(user);
            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Claims = userClaims.Select(c =>c.Type+ ":" + c.Value).ToList(),
                Roles = userRoles,


            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (id == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                await userManager.DeleteAsync(user);
            }
            return RedirectToAction("AllUser");
        }
        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            ViewBag.userId = userId;
            if (userId == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }
            var model = new List<UserRolesViewModel>();
            foreach (var role in roleManager.Roles)

            {
                var uerRolesViewModel = new UserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name,

                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    uerRolesViewModel.IsSelected = true;
                }
                else
                {
                    uerRolesViewModel.IsSelected = false;
                }
                model.Add(uerRolesViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(List<UserRolesViewModel> model, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            var roles = await userManager.GetRolesAsync(user);
            var result = await userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }

            result = await userManager.AddToRolesAsync(user,
                model.Where(x => x.IsSelected).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }

            return RedirectToAction("EditUser", new { Id = userId });
        }
        [HttpGet]
        public async Task<IActionResult> ManageUserClaims(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }
            var existingUserClaims = await userManager.GetClaimsAsync(user);
            var model = new UserClaimViewModel
            {
                UserId = userId,
            };
            foreach (Claim claim in ClaimsStore.AllClaims)
            {
                UserClaim userClaim = new UserClaim
                {
                    ClaimType = claim.Type,
                };
                if (existingUserClaims.Any(c => c.Type == claim.Type && c.Value == "true"))
                {
                    userClaim.IsSelected = true;
                }
                model.Claims.Add(userClaim);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ManageUserClaims(UserClaimViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.UserId} cannot be found";
                return View("NotFound");
            }
            var claims = await userManager.GetClaimsAsync(user);
            var result = await userManager.RemoveClaimsAsync(user, claims);
            if (!result.Succeeded) 
            {
                ModelState.AddModelError("", "Cannot remove user existing claims");
                return View(model);
            }
            result = await userManager.AddClaimsAsync(user, model.Claims.Select(c => new Claim(c.ClaimType , c.IsSelected ? "true":"false")));

            


            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected claims to user");
                return View(model);
            }
            return RedirectToAction("EditUser",new { Id = model.UserId});
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }




    }
