using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEMO.Models
{
    public class EmployeeContext : IdentityDbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<EmployeeProject> EmployeeProjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Salary>()
                .HasOne(p => p.Employee)
                .WithMany(b => b.Salaries)
                .HasForeignKey(p => p.EmployeeId); ;




            modelBuilder.Entity<EmployeeProject>()
                .HasKey(x => new { x.ProjectId, x.EmployeeId });
            modelBuilder.Entity<EmployeeProject>()
                .HasOne(p => p.Employee)
                .WithMany(e => e.EmployeeProjects)
                .HasForeignKey(b => b.EmployeeId);
            modelBuilder.Entity<EmployeeProject>()
                .HasOne(p => p.Project)
                .WithMany(x => x.EmployeeProjects)
                .HasForeignKey(y => y.ProjectId);


            base.OnModelCreating(modelBuilder);

        }

    }
}

