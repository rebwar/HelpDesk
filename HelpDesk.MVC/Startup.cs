using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpDesk.Domain.Contracts.Articles;
using HelpDesk.Domain.Contracts.Categories;
using HelpDesk.Domain.Contracts.Images;
using HelpDesk.InfraStructures.DataAccess.Articles;
using HelpDesk.InfraStructures.DataAccess.Categories;
using HelpDesk.InfraStructures.DataAccess.Common;
using HelpDesk.InfraStructures.DataAccess.Images;
using HelpDesk.MVC.Models.Users;
using HelpDesk.MVC.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HelpDesk.MVC
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

            int minPasswordLenght = int.Parse(Configuration["MinPasswordLength"]);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<HelpDeskContext>(c => c.UseSqlServer(Configuration.GetConnectionString("HelpDesk")));
            services.AddDbContext<UserDbContext>(c => c.UseSqlServer(Configuration.GetConnectionString("UserDb")));
            services.AddIdentity<ApplicationUsers, ApplicaionRoles>(c =>
            {
                c.User.RequireUniqueEmail = true;
                c.Password.RequireDigit = false;
                c.Password.RequiredLength = minPasswordLenght;
                c.Password.RequireNonAlphanumeric = false;
                c.Password.RequireUppercase = false;
                c.Password.RequiredUniqueChars = 1;
                c.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<UserDbContext>()
            .AddDefaultTokenProviders(); 
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IimageRepository, ImageRepository>();
            services.AddScoped<IUploadFileRepository, UploadFileRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
}
