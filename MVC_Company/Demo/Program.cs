using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.BLL;
using Demo.DAL.Models;
using Demo.PL.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MVC_Demo.BLL.Interfaces;
using MVC_Demo.BLL.Repositories;
using MVC_Demo.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Demo.PL.Helper;

namespace MVC_Demo
{
    public class Program
    {
        //Entry Point
        public static void Main(string[] args)
        {
            var Builder = WebApplication.CreateBuilder(args);

            #region Configure Services thats allow Dependanciy Injection

            Builder.Services.AddControllersWithViews();  //Register built in MVC Services to Container

            Builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            Builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            Builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


            Builder.Services.AddDbContext<AppDbContext>(
                options =>
            options.UseSqlServer(Builder.Configuration.GetConnectionString("Default"))
            );

            Builder.Services.AddAutoMapper(m => m.AddProfile(new MappingProfiles()));


            Builder.Services.AddScoped<IScopedService, ScopedService>();  // per Request
            Builder.Services.AddTransient<ITransientService, TransientService>();   // per Operation
            Builder.Services.AddSingleton<ISingeltonService, SingeltonService>();   // per App


            Builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddEntityFrameworkStores<AppDbContext>()
               .AddDefaultTokenProviders();

            Builder.Services.ConfigureApplicationCookie(conf =>
            {
                conf.LoginPath = "/Account/SignIn";

            });

            #endregion

            var app = Builder.Build();

            #region Configure HTTP Request pipline or middleware

            if (app.Environment.IsDevelopment())
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

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            }
                            );
            #endregion

            app.Run();

        }

    }
}
