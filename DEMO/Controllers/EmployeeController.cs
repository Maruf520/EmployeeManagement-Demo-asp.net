using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DEMO.Models;

namespace DEMO.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeContext _context;

        public EmployeeController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var employee = _context.Employees.Include(i =>i.Designation);
            ViewBag.Employee = employee.ToList();
            return View();
        }

        // GET: Employees/Details/5
        public IActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var employee = _context.Employees.Where(a => a.EmployeeId == id).Include(i => i.Salaries).Include(x =>x.Designation).FirstOrDefault();

            if (employee == null)
            {
                return NotFound();
            }

             ViewBag.salaries = employee.Salaries.Select(s => new SalaryModel
            {
                Balance = s.Balance,
                Date = s.Date.ToString("MMM-yyyy"),
            }).ToList();
            ViewBag.designationx = _context.Designations.Where(i => i.DesignationId == id);

            var result =  new EmployeeModel
            {
                EmployeeId  =employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email,
                Address = employee.Address,
                DesignationId = employee.DesignationId ,

            };
            return View(result);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            var employee = _context.Employees.Include(i => i.Designation);
            ViewBag.employeee = employee.ToList();
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,PhoneNumber,Email,Address,DesignationId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            var result = new EmployeeModel
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email,
                Address = employee.Address,
                DesignationId = employee.DesignationId,



            };
            return View(result);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,PhoneNumber,Email,Address,DesignationId,designation,EmployeeId")] Employee employee)
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

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }
            //ViewBag.delete = employee;

            var result = new EmployeeModel
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email,
                Address = employee.Address,
                DesignationId = employee.DesignationId,
            };



            return View(result);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create_des()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create_des([Bind( "designation")] Designation designationx)
        {
            if(ModelState.IsValid)
            {
                _context.Add(designationx);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> Edit_des(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var destinationx = await _context.Designations.FindAsync(id);
            return View(destinationx);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_des([Bind(" DesignationId,designation")] Designation designationx)
        {
            if (ModelState.IsValid)
            {
                _context.Update(designationx);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> DesignationView()
        {
            var des = _context.Designations;
            ViewBag.designationn = des.ToList();
            return View();
        }

        [HttpGet]
        public IActionResult CreateProject()
        {
            return View();
        }

        public async Task<IActionResult> CreateProject([Bind("ProjectId,ProjectName")] Project project )
        {
            if(ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpGet]

        public async Task<IActionResult> EditProject(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var project = _context.Projects.Find(id);
            return View(project);
        }

/*        public async Task<IActionResult> Edit_des(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var destinationx = await _context.Designations.FindAsync(id);
            return View(destinationx);

        }*/


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProject(int ? id, [Bind("ProjectId,ProjectName")] Project project)
        {
            if(ModelState.IsValid)
            {
                _context.Update(project);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(EditProject));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }

    
}
