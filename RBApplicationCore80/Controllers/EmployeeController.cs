using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using RBApplicationCore80.Data;
using RBApplicationCore80.ModelData;
using RBApplicationCore80.Models;
using RBApplicationCore80.ViewModel;

namespace RBApplicationCore80.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;// Newly Added by Murari for Role

        public EmployeeController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;// Newly Added by Murari for Role
        }

        // GET: Employee
        public IActionResult Index()
        {
            EmployeeViewModel empvm = new EmployeeViewModel ();
            List<Employee> emp_ = new List<Employee>();

            empvm.occupation = PopulateOccupation();
            empvm.role_ = PopulateRole();
            var employeelistdb_ = _context.Employee.ToList();
            foreach (var emp in employeelistdb_)
            {
                emp.OccupationName = empvm.occupation.Where(p => p.Value == Convert.ToString(emp.Occupation)).First().Text;
                emp.GenderName= Enum.GetName(typeof(Gender), Convert.ToInt16(empvm.Gender));
                emp.RoleName = empvm.role_.Where(p => p.Value == Convert.ToString(emp.role)).First().Text;
                empvm = new EmployeeViewModel
                {   
                    employee = new List<Employee> { emp }                    
                };              
            }
            return View(empvm);

            //return View(await _context.Employee.ToListAsync());            
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employee/Create
        public async Task<IActionResult> Create()
        {            
            EmployeeViewModel empvm = new EmployeeViewModel();
            //List<Occupation> objOccupation = await _context.Occupation.ToListAsync();
            empvm.occupation = PopulateOccupation();
            empvm.role_ = PopulateRole();
            
            return View(empvm);
        }
        private List<SelectListItem>  PopulateRole() {
            var roles = _roleManager.Roles.ToList();
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var merit in roles)
            {
                items.Add(
                    new SelectListItem
                    {
                        Text = merit.Name,
                        Value = merit.Id
                    });
            }

            return items;
        }

        private List<SelectListItem> PopulateOccupation()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            List<Occupation> objOccupation =  _context.Occupation.ToList();
            
            foreach (var merit in objOccupation)
            {
                items.Add(
                    new SelectListItem
                    {
                        Text = merit.OccupationName,
                        Value = merit.OccupationId.ToString()
                    });                    
            }
            return items;
        }
            // POST: Employee/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employee)
        {
            EmployeeViewModel emp = new EmployeeViewModel();

            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EmployeeViewModel empvm = new EmployeeViewModel();
            var employee = await _context.Employee.FindAsync(id);
            empvm.occupation = PopulateOccupation();
            empvm.role_ = PopulateRole();
            employee.OccupationName = empvm.occupation.Where(p => p.Value == Convert.ToString(employee.Occupation)).First().Text;
            employee.GenderName = Enum.GetName(typeof(Gender), Convert.ToInt16(empvm.Gender));
            employee.RoleName = empvm.role_.Where(p => p.Value == Convert.ToString(employee.role)).First().Text;

            empvm = new EmployeeViewModel
            {
                employee = new List<Employee> { employee }
            };
                        
             
            if (employee == null)
            {
                return NotFound();
            }
            return View(empvm);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,FirstName,MiddleName,LastName,Gender,Address,City,State,Country,StateName,PhoneNo,MobileNo,EmailAddress,Occupation,Dob,role")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee != null)
            {
                _context.Employee.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.EmployeeId == id);
        }
    }
}
