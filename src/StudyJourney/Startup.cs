using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using NToastNotify;
using StudyJourney.Data;
using StudyJourney.Models;

namespace StudyJourney
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(hostEnvironment.ContentRootPath)
               .AddJsonFile("appsettings.json", true, true)
               .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
               .AddEnvironmentVariables();

            if (hostEnvironment.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<StudyJourneyDbContext>(options =>
              options.UseSqlServer(
                  Configuration.GetConnectionString("SqlServerConnection")));


            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                  .AddEntityFrameworkStores<StudyJourneyDbContext>()
                  .AddDefaultUI()
                  .AddDefaultTokenProviders();

            services.AddRazorPages()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Topics");
                    options.Conventions.AuthorizeFolder("/Questions");
                    options.Conventions.AuthorizeFolder("/Answers");
                }).AddNToastNotifyNoty(new NotyOptions
                {
                    Layout = "bottomRight",
                    ProgressBar = true,
                    Timeout = 5000,
                    Theme = "metroui"
                });

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.Configure<SMTPSettings>(Configuration.GetSection("SMTPSettings"));
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();

            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseNToastNotify();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCookiePolicy();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
