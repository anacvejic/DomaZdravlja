using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace DomaZdravlja
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
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDistributedMemoryCache();
            services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(15); });
            //Dodaci za lokalizaciju
            services.AddLocalization(o => o.ResourcesPath="Resources");
            services.AddMvc().AddDataAnnotationsLocalization(o => { o.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(Resource));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<RequestLocalizationOptions>(o =>
            {
                var supported = new List<CultureInfo>
                {
                    new CultureInfo("en"),
                    new CultureInfo("sr")
                };
                o.DefaultRequestCulture = new RequestCulture("sr");
                o.SupportedCultures = supported;
                o.SupportedUICultures = supported;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSession();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            //Dodato zbog lokalizacije
            app.UseRequestLocalization();
            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                //template: "{controller=MedicinskaSestra}/{action=Index}/{id?}");
                template: "{controller=Logovanje}/{action=Pocetna}/{id?}");
                //template: "{controller=Administrator}/{action=Index}/{id?}");
                //template: "{controller=Lekar}/{action=Index}/{id?}");
            });
        }
    }
}
