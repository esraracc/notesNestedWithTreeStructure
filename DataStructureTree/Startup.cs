using DataLayer.Concrete.EfCore;
using DataLayer.Concrete.EfCore.Identity;
using EntityLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataStructureTree
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
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MsSqlConnection")).EnableSensitiveDataLogging(true));
            services.AddDbContext<NoteContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MsSqlConnection")), ServiceLifetime.Transient);
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // password
                options.Password.RequireDigit = false; // sayýsal deðer gire zorunluluðu
                options.Password.RequireLowercase = false; // küçük harf zorunluluðu
                options.Password.RequireUppercase = false; // büyük harf zorunluluðu
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;

                // Lockout
                options.Lockout.MaxFailedAccessAttempts = 5;//kullanýcý hesabýna 5 kere yanlýþ oturum hakkýna sahip sonra hesabý kilitlenir.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // 5dk sonra kullanýcý giriþ yapabilir.
                options.Lockout.AllowedForNewUsers = true;

                //options.User.AllowedUserNameCharacters=""
                options.User.RequireUniqueEmail = true; // her kullanýcýnýn farklý maili olmasý gerekiyor
                options.SignIn.RequireConfirmedEmail = false; // kullanýcýnýn mailine onay kodu gider
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            services.Configure<SecurityStampValidatorOptions>(options => options.ValidationInterval = TimeSpan.FromDays(24));

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = ".AspNetCore.Identity.Application";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;//cookie süresi varsayýlan 20 dk . 20 dk sonrasýnda tarayýcý login iþlemini bize gönderir.
                options.ExpireTimeSpan = TimeSpan.FromDays(1); // login olma süresi yukarýdaki maddede varsayýlan 20 dk burada ben ayarlayabiliyorum mesela 1 gün ayarladým 
            });

            services.AddScoped<DataLayer.Abstract.IUnitOfWork, DataLayer.Concrete.EfCore.UnitOfWork>();
            services.AddScoped<BusinessLayer.Abstract.IUnitOfWork, BusinessLayer.Concrete.UnitOfWork>();


            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
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

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            SeedIdentity.SeedAsync(Configuration, userManager, roleManager).Wait();
        }
    }
}
