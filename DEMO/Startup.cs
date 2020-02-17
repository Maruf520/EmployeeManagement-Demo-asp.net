using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DEMO.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DEMO
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            services.AddControllersWithViews();

            services.AddDbContext<EmployeeContext>(options =>
            
            options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<EmployeeContext>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteRolePolicy",
                policy => policy.RequireClaim("Delete Role"));

                options.AddPolicy("EditRolePolicy", policy => policy.RequireAssertion(context => 
                context.User.IsInRole("Admin") && context.User.HasClaim(claim => claim.Type == "Edit Role" && claim.Value == "true")||
                context.User.IsInRole("Super Admin")));

                options.AddPolicy("CreateRolePolicy", policy => policy.RequireClaim("Create Role"));

                options.AddPolicy("AccessView", policy => policy.RequireAssertion(context => context.User.IsInRole("Admin") && context.User.HasClaim(claim => claim.Type == "View Access" && claim.Value == "true") ||
                 context.User.IsInRole("Super Admin")));
            });
            services.ConfigureApplicationCookie(options =>
            options.AccessDeniedPath = new PathString("/Administration/AccessDenied"));

        }

      
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Employee}/{action=Index}/{id?}");
            });
        }
    }
}
