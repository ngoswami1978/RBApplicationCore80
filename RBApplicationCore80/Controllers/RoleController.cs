using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System.Diagnostics;

namespace RBApplicationCore80.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        
        RoleManager <IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole > roleManager)
        {
            this.roleManager = roleManager;                
        }

        [OutputCache(PolicyName = "PeoplePolicy")]
        [Authorize(Policy ="readpolicy")]
        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }

        [Authorize(Policy ="writepolicy")]
        public IActionResult Create()
        {
            return View(new IdentityRole());
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            await roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }
    }
}
