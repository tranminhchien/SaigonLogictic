using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Utils;
using LinqToDB;
using Logictics.DAL.EFContext;
using Logictics.DAL.Repository;
using Logictics.Service.Core;
using Logictics.Service.ViewModel;
using Logictics.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Logictics.Web
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            //Add MVC Middleware  
            services.AddControllersWithViews(/*op =>*/
                //op.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
            );

            // database connection configuration
            services.AddDbContext<LogicticsDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("LogicticsDatabase"))
            );


            services.AddRazorPages();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            JWTConfig(services);

            DependencyInjectionConfig(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, LogicticsDbContext dataContext)
        {
            // migrate any database changes on startup (includes initial db creation)
            dataContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCookiePolicy();
            app.UseSession();
            app.Use(async (context, next) =>
            {
                var JWToken = context.Session.GetString("JWToken");
                if (!string.IsNullOrEmpty(JWToken))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
                }
                await next();
            });
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "MyArea",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }

        private void JWTConfig(IServiceCollection services)
        {
            Sitekeys.Configure(Configuration.GetSection("AppSettings"));
            var key = Encoding.ASCII.GetBytes(Sitekeys.Token);

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(token =>
            {
                token.RequireHttpsMetadata = false;
                token.SaveToken = true;
                token.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = Sitekeys.WebSiteDomain,
                    ValidateAudience = true,
                    ValidAudience = Sitekeys.WebSiteDomain,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        private void DependencyInjectionConfig(IServiceCollection services)
        {
            // repo dependency injection container configuration
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IOrderRepo, OrderRepo>();
            services.AddScoped<IOrderDetailRepo, OrderDetailRepo>();
            services.AddScoped<IStoreRepo, StoreRepo>();
            services.AddScoped<ICategoryProductRepo, CategoryProductRepo>();
      
            // service dependency injection container configuration
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            services.AddScoped<IClientService, ClientService>();
     
            // common di
            services.AddScoped<IDatetimeUtil, DatetimeUtil>();
            services.AddScoped<ITimestampUtil, TimestampUtil>();
            services.AddScoped<IEncryptionUtil, EncryptionUtil>();
            services.AddScoped<ITimezoneListUtil, TimezoneListUtil>();
        }

        //private static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
        //{
        //    var builder = new ServiceCollection()
        //    .AddLogging()
        //    .AddMvc()
        //    .AddNewtonsoftJson()
        //    .Services.BuildServiceProvider();

        //    return builder
        //        .GetRequiredService<IOptions<MvcOptions>>()
        //        .Value
        //        .InputFormatters
        //        .OfType<NewtonsoftJsonPatchInputFormatter>()
        //        .First();
        //}
    }
}
