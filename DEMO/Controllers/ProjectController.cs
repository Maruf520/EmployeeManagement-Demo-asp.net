using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEMO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DEMO.Controllers
{
    public class ProjectController : Controller
    {
        private readonly EmployeeContext _context;

        public ProjectController(EmployeeContext context)
        {
            _context = context;
        }


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

        public async Task<IActionResult> CreateProject([Bind("ProjectId,ProjectName")] Project project)
        {
            if (ModelState.IsValid)
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
            if (id == null)
            {
                return NotFound();
            }
            var project = _context.Projects.Find(id);
            return View(project);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProject(int? id, [Bind("ProjectId,ProjectName")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Update(project);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Projects));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = _context.Projects.FirstOrDefault(i => i.ProjectId == id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Projects));
        }



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

             //   return RedirectToAction("Index", "");
                 return RedirectToAction(nameof(Projects));
            }
            return View();
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

        public async Task<IActionResult>Project()
        {
            ViewBag.proj = _context.EmployeeProjects.Select(s => new EmployeeProjectModel

            {
                LastName = s.Employee.LastName,
                ProjectName = s.Project.ProjectName,
            });

            return View();
        }





    }
}