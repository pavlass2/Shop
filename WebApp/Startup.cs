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
using WebApp.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Models;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace WebApp
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

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });

            services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
           
            services.AddIdentity<ApplicationUser, IdentityRole<int>>()
                .AddRoleManager<RoleManager<IdentityRole<int>>>()
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<IdentityDbContext>();

            //Microsoft login
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddOpenIdConnect(options =>
            {
                Configuration.Bind("AzureAd", options);
            })
            .AddCookie();

            services.AddAuthorization();
            
            services.AddDbContext<ShopDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IDataAcces, SqlData>();            

            services.AddMvc();               

            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            services.AddDataProtection();

            services.AddSingleton<PurposeStringConstants>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                
                //Ignore http requests
                app.UseHsts();
            }
            
            //Setting the Content security policy
             app.UseCsp(options => options
            .DefaultSources(s => s.Self())
            .StyleSources(s => s.Self())
            .ScriptSources(s => s.Self().CustomSources("https://ajax.aspnetcdn.com/ajax/"))
            .ReportUris(r => r.Uris("/Reports")));
            
            //Protection against showing potencially malicious iFrames from different sites
            app.UseXfo(options => options.SameOrigin());

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            CreateRoles(services).Wait();

            app.UseMvc();
        }        

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initiate
            //currently only "Admin" is used 
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "Manager", "Customer" };
            IdentityResult roleResult;

            //check and create roles
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole<int>(roleName));
                }
            }

            //check and create admin
            string adminEmail = Configuration["AdminEmail"];
            var user = await UserManager.FindByEmailAsync(adminEmail);

            if (user == null)
            {
                var adminUser = new ApplicationUser
                {
                    Email = adminEmail,
                    UserName = Configuration["AdminEmail"]
                };

                var createAdmin = await UserManager.CreateAsync(adminUser, Configuration["AdminPassword"]);

                if (createAdmin.Succeeded)
                {
                    //adding claim
                    //await UserManager.AddClaimAsync(adminUser, new Claim("Role", "Admin"));
                    //adding role to a user
                    await UserManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}

