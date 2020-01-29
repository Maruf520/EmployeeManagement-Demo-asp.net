using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DEMO.Models
{
    public class EmployeeContext:DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
        }
            public DbSet<Employee> Employees { get; set; }
            public DbSet<Salary> Salaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Salary>()
                .HasOne(p => p.Employee)
                .WithMany(b => b.Salaries)
                .HasForeignKey(p => p.EmployeeId); ;
        }

    }
    }

