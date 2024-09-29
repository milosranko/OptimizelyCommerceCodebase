using CmsContentScaffolding.Optimizely.Models;
using CmsContentScaffolding.Optimizely.Startup;
using EPiServer.Cms.Shell;
using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Scheduler;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using Mediachase.Commerce.Anonymous;
using Mediachase.Commerce.Catalog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Optimizely.Commerce.Demo.Infrastructure.Initialization;
using Optimizely.Commerce.Demo.Models;
using Optimizely.Commerce.Demo.Models.Cms;
using Optimizely.Demo.Core.Business.Rendering;
using Optimizely.Demo.PublicWeb.Filters;
using System.Globalization;

namespace Optimizely.Commerce.Demo;

public class Startup
{
    private readonly IWebHostEnvironment _webHostingEnvironment;

    public Startup(IWebHostEnvironment webHostingEnvironment)
    {
        _webHostingEnvironment = webHostingEnvironment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        if (_webHostingEnvironment.IsDevelopment())
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(_webHostingEnvironment.ContentRootPath, "App_Data"));

            services.Configure<SchedulerOptions>(options => options.Enabled = false);
        }

        services
            .AddCmsAspNetIdentity<ApplicationUser>()
            .AddCommerce()
            .AddFind()
            .AddAdminUserRegistration()
            .AddEmbeddedLocalization<Startup>()
            .AddCmsContentScaffolding();

        services.AddSingleton<EnableCatalogRoot>();

        services
            .Configure<RazorViewEngineOptions>(options => options.ViewLocationExpanders.Add(new SiteViewEngineLocationExpander()))
            .Configure<MvcOptions>(options => options.Filters.Add<PageContextActionFilter>());
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        var referenceConverter = app.ApplicationServices.GetRequiredService<ReferenceConverter>();

        app.UseCmsContentScaffolding(
            builderOptions: o =>
            {
                o.SiteName = "Demo";
                o.Language = CultureInfo.GetCultureInfo("en");
                o.SiteHost = "https://localhost:5000";
                o.BuildMode = BuildMode.Append;
                o.StartPageType = typeof(StartPage);
            },
            builder: b =>
            {
                b.UseAssets(referenceConverter.GetRootLink())
                .WithContent<CatalogContent>(x =>
                {
                    x.Name = "Catalog 1";
                    x.DefaultCurrency = "EUR";
                    x.DefaultLanguage = "en";
                    x.WeightBase = "kgs";
                    x.LengthBase = "cm";
                }, l1 => l1.WithContent<FashionNode>(x => x.Name = "Men", l2 =>
                            l2.WithContent<FashionNode>(x => x.Name = "Shoes", l3 =>
                                l3.WithContent<FashionProduct>(x => x.Name = "Product 1", l4 =>
                                    l4
                                    .WithContent<FashionVariant>(v => v.Name = "Variant 1")
                                    .WithContent<FashionVariant>(v => v.Name = "Variant 2"))
                                .WithContent<FashionProduct>(x => x.Name = "Product 2")
                                .WithContent<FashionProduct>(x => x.Name = "Product 3")
                         ).WithContent<FashionNode>(x => x.Name = "Accessories", l3 =>
                            l3
                            .WithContent<FashionProduct>(x => x.Name = "Product 1")
                            .WithContent<FashionProduct>(x => x.Name = "Product 2")
                            .WithContent<FashionProduct>(x => x.Name = "Product 3")
                         )
                    )
                );
            });

        app.UseAnonymousId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapContent();
        });
    }
}
