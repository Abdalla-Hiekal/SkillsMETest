using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillsTest.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkillsTest.Models;

namespace SkillsTest
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseRequestinitDb();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            

        }
    }
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // IMyScopedService is injected into Invoke
        public async Task Invoke(HttpContext httpContext, ApplicationDbContext _context)
        {
            //init
            //var config = webHost.Services.GetService<IConfiguration>();
            //var _context = app.ApplicationServices.GetService<ApplicationDbContext>();
            if (_context.Pasture.Count() == 0)
            {
                // init  4 Pastures
                List<Pasture> PastureList = new List<Pasture>()
            {
                new Pasture{Name="Pasture1",GrassCondition="good", Temperature=20},
                new Pasture{Name="Pasture2",GrassCondition="bad", Temperature=15},
                new Pasture{Name="Pasture3", GrassCondition="good", Temperature=13},
                new Pasture{Name="Pasture4",GrassCondition="bad", Temperature=22}
            };
                //init 100 Cows , 100 Bulls
                List<Cattle> CattleList = new List<Cattle>();
                for (int i = 0; i < 100; i++)
                {
                    CattleList.Add(new Cattle { Type = "Cow", Age = 10, Price = 7000, Weight = 1100, HealthStatus = "good", Color = "White" });
                }
                for (int i = 0; i < 100; i++)
                {
                    CattleList.Add(new Cattle { Type = "Bull", Age = 10, Price = 7000, Weight = 1100, HealthStatus = "good", Color = "White" });
                }

                _context.AddRange(PastureList);
                _context.AddRange(CattleList);
                await _context.SaveChangesAsync();
            }
            await _next(httpContext);
        }
    }
    public static class RequestDbMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestinitDb(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddleware>();
        }
    }
}
