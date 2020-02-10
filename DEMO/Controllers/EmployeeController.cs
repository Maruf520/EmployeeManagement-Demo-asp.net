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

        #region Utilities

        #endregion
        public async Task<IActionResult> Index()
        {
            var employee = _context.Employees.Include(i =>i.Designation);
            ViewBag.Employee = employee.ToList();
            return View();
        }


        public IActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var employee = _context.Employees.Where(a => a.EmployeeId == id).Include(i => i.Salaries).Include(x => x.EmployeeProjects).FirstOrDefault();

            if (employee == null)
            {
                return NotFound();
            }

             ViewBag.salaries = employee.Salaries.Select(s => new SalaryModel
            {
                Balance = s.Balance,
                Date = s.Date.ToString("MMM-yyyy"),
                EmployeeId = s.EmployeeId,
                SalaryId = s.SalaryId
            }).ToList();
            ViewBag.project = _context.EmployeeProjects.Where(s => s.EmployeeId == id).Select(s => new EmployeeProjectModel

            {
                LastName = s.Employee.LastName,
                ProjectName = s.Project.ProjectName,
            }
            
            ) ;
        //    ViewBag.Project = _context.EmployeeProjects.Where(e => e.EmployeeId == id);
            var result = new EmployeeModel
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email,
                Address = employee.Address,
                DesignationId = employee.DesignationId,
               // designation = employee.Designation.designation,
                //ProjectName = employee.Project.ProjectName,

            };
            return View(result);
        }

        public IActionResult Create()
        {
            var des = new EmployeeModel();

            var designations = _context.Designations;

            foreach (var designation in designations) 
            {
                des.DesignationList.Add( new SelectListItem()
                {
                    Value = designation.DesignationId.ToString(),
                    Text = designation.designation
                });


            }
          
           var employee = _context.Employees.Include(i => i.Designation);
            ViewBag.employeee = employee.ToList();
            return View(des);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,PhoneNumber,Email,Address,DesignationId,designation")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

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
            var designations = _context.Designations;

            foreach (var designation in designations)
            {
                result.DesignationList.Add(new SelectListItem()
                {
                    Value = designation.DesignationId.ToString(),
                    Text = designation.designation
                });


            }
            return View(result);
        }

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
                return RedirectToAction(nameof(DesignationView));
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
        public async Task<IActionResult> DeleteDesignation(int id)
        {
            var x = _context.Designations.FirstOrDefault(e => e.DesignationId == id);
            _context.Designations.Remove(x);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(DesignationView));
        }
/*
        [HttpGet]
        public IActionResult CreateProject()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Projects()
        {
            var projects = _context.Projects.ToList();
            ViewBag.project = projects;
            return View();
        }

        public async Task<IActionResult> CreateProject([Bind("ProjectId,ProjectName")] Project project )
        {
            if(ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Projects));
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProject(int ? id, [Bind("ProjectId,ProjectName")] Project project)
        {
            if(ModelState.IsValid)
            {
                _context.Update(project);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Projects));
        }

        [HttpGet]
        public async Task<IActionResult>DeleteProject(int id)
        {
            var project = _context.Projects.FirstOrDefault(i => i.ProjectId == id);
           _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Projects));
        }*/

        [HttpGet]
        public async Task<IActionResult>EditSalary(int ? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var salary = await _context.Salaries
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            return View(salary);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>EditSalary(int id,[Bind("Balance,Date,EmployeeId,SalaryId")]Salary salary)
        {
            if(ModelState.IsValid)
            {
                _context.Update(salary);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteSalary(int id)
        {
            var x =  _context.Salaries.FirstOrDefault(e=>e.SalaryId == id);
            _context.Salaries.Remove(x);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

/*
        public async Task<IActionResult> AssignProject()
        {
            var projectx = new EmployeeProjectModel();


            var projects = _context.Projects;
            var projectss = _context.Employees;

            foreach (var projectxx in projectss)
            {
                projectx.EmpoyeeList.Add(new SelectListItem()
                {
                    Value = projectxx.EmployeeId.ToString(),
                    Text = projectxx.LastName
                }

                    ); ;
            }

            foreach (var projx in projects)
            {
                projectx.ProjectList.Add(new SelectListItem()
                {
                    Value = projx.ProjectId.ToString(),
                    Text = projx.ProjectName
                }


                  ); ; 

            }
            return View(projectx);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignProject([Bind("ProjectId,EmployeeId")]EmployeeProject employeeProject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeProject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AssignedProjects()
        {
            var project = _context.EmployeeProjects;
            ViewBag.project = project.Select(x => new EmployeeProjectModel
            {
                LastName = x.Employee.LastName,
                ProjectName = x.Project.ProjectName,

            });



            return View();
        }


*/


        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }

    
}
