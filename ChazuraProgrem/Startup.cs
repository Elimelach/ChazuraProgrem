using ChazuraProgram.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using ChazuraProgram.MyEmail;

namespace ChazuraProgram
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
            services.AddRouting(options => { options.LowercaseUrls = true;
                options.AppendTrailingSlash = true;
            });
            services.AddMemoryCache();
            services.AddSession();

            services.AddControllersWithViews().AddNewtonsoftJson();


            services.AddDbContext<ChazuraContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("ChazuraContext")));
            services.AddIdentity<User, IdentityRole>(option =>
            {
                option.Password.RequiredLength = 6;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireDigit = false;
                
                
            }).AddEntityFrameworkStores<ChazuraContext>().AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = false;
                options.ExpireTimeSpan = TimeSpan.FromDays(90);

            });
            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.AddSingleton<IMailer, Mailer>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IChazuraUnitOfWork, ChazuraUnitOfWork>();
            services.AddTransient(typeof(IDateConverter), typeof(DateConverter));
            services.AddTransient<IRequests, Requests>();
            services.AddTransient<ISessCook, SessCook>();
            services.AddTransient<ISponserRequests, SponserRequests>();
            services.AddHttpContextAccessor();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();
            //ChazuraContext.CreateAdminUser(app.ApplicationServices).Wait();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapAreaControllerRoute(
                  name: "admin4",
                  areaName: "Admin",
                  pattern: "Admin/{controller}/{action}/page/{pagenumber}/size/{pagesize}/sort/{sortfield}/{sortdirection}/filter-by-role/{Filter}/filter-by-date/{FilterTime}/start-date/{StartDate}");

                endpoints.MapAreaControllerRoute(
                  name: "admin3",
                  areaName: "Admin",
                  pattern: "Admin/{controller}/{action}/page/{pagenumber}/size/{pagesize}/sort/{sortfield}/{sortdirection}/filter-by-role/{Filter}/filter-by-date/{FilterTime}");

                endpoints.MapAreaControllerRoute(
                   name: "admin2",
                   areaName: "Admin",
                   pattern: "Admin/{controller}/{action}/page/{pagenumber}/size/{pagesize}/sort/{sortfield}/{sortdirection}/filter-by-role/{Filter}");

                endpoints.MapAreaControllerRoute(
                    name: "admin",
                    areaName: "Admin",
                    pattern:"Admin/{controller=Home}/{action=Index}/{id?}"
                    );

                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "{controller}/{action}/page/{pagenumber}/size/{pagesize}/sort/{sortfield}/{sortdirection}");


                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
