using CmsContentScaffolding.Optimizely.Extensions;
using CmsContentScaffolding.Optimizely.Models;
using CmsContentScaffolding.Optimizely.Startup;
using CmsContentScaffolding.Shared.Resources;
using EPiServer.Cms.Shell;
using EPiServer.Cms.Shell.UI;
using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Framework.Web.Resources;
using EPiServer.Scheduler;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using Mediachase.Commerce.Anonymous;
using Mediachase.Commerce.Catalog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Optimizely.Commerce.Demo.Infrastructure.Initialization;
using Optimizely.Commerce.Demo.Models.Cms;
using Optimizely.Demo.Cms.Core.Business.Initialization;
using Optimizely.Demo.Commerce.Models.Categories;
using Optimizely.Demo.Commerce.Models.Products;
using Optimizely.Demo.Commerce.Models.Variants;
using Optimizely.Demo.ContentTypes.Blocks;
using Optimizely.Demo.Core.Business.Rendering;
using Optimizely.Demo.PublicWeb.Filters;
using System.Globalization;

namespace Optimizely.Commerce.Demo;

public class Startup
{
    private readonly IWebHostEnvironment _webHostingEnvironment;
    private readonly IConfiguration _configuration;

    public Startup(IWebHostEnvironment webHostingEnvironment, IConfiguration configuration)
    {
        _webHostingEnvironment = webHostingEnvironment;
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        if (_webHostingEnvironment.IsDevelopment())
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(_webHostingEnvironment.ContentRootPath, "App_Data"));

            services.Configure<SchedulerOptions>(o => o.Enabled = false);
            services.Configure<ClientResourceOptions>(uiOptions =>
            {
                uiOptions.Debug = true;
            });
        }

        services
            .AddCmsAspNetIdentity<ApplicationUser>()
            .AddCommerce()
            .AddFind()
            .AddAdminUserRegistration(o => o.Behavior = RegisterAdminUserBehaviors.LocalRequestsOnly)
            .AddEmbeddedLocalization<Startup>()
            .AddCmsContentScaffolding();

        services.AddSingleton<EnableCatalogRoot>();

        if (!_webHostingEnvironment.IsDevelopment())
        {
            services.AddCmsCloudPlatformSupport(_configuration);
            services.AddCommerceCloudPlatformSupport(_configuration);
        }

        services
            //.Configure<FindCommerceOptions>(o =>
            //{
            //    o.CatalogContentClientConventions.FindListenOnCommerceRemoteEvents = true;
            //    o.CatalogContentClientConventions.FindIndexCatalogContent = true;
            //})
            //.Configure<FindCmsOptions>(o => o)
            .Configure<RazorViewEngineOptions>(o => o.ViewLocationExpanders.Add(new SiteViewEngineLocationExpander()))
            .Configure<MvcOptions>(o => o.Filters.Add<PageContextActionFilter>());

        #region Optimizely API services

        //services.ConfigureContentApiOptions(o =>
        //{
        //    o.EnablePreviewFeatures = true;
        //    o.IncludeEmptyContentProperties = true;
        //    o.FlattenPropertyModel = false;
        //    o.IncludeMasterLanguage = false;

        //});

        //// Content Delivery API
        //services.AddContentDeliveryApi()
        //    .WithFriendlyUrl()
        //    .WithSiteBasedCors();
        //services.AddContentDeliveryApi(OpenIDConnectOptionsDefaults.AuthenticationScheme, options =>
        //{
        //    options.SiteDefinitionApiEnabled = true;
        //}).WithFriendlyUrl()
        //      .WithSiteBasedCors();

        //// Content Delivery Forms API
        //services.AddFormsApi();

        //// Content Delivery Commerce API
        //services.AddCommerceApi<AnaApplicationUser>(OpenIDConnectOptionsDefaults.AuthenticationScheme, o =>
        //{
        //    o.DisableScopeValidation = true;
        //});

        //// Content Definitions API
        //services.AddContentDefinitionsApi(options =>
        //{
        //    // Accept anonymous calls
        //    options.DisableScopeValidation = true;
        //});

        //// Content Management
        //services.AddContentManagementApi(OpenIDConnectOptionsDefaults.AuthenticationScheme, options =>
        //{
        //    // Accept anonymous calls
        //    options.DisableScopeValidation = true;
        //});

        #endregion
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
                o.BuildMode = BuildMode.OnlyIfEmpty;
                o.PublishContent = true;
                o.StartPageType = typeof(StartPage);
            },
            builder: b =>
            {
                b.UsePages()
                .WithStartPage<StartPage>(p =>
                {
                    p.Name = "Start Page";
                    p.Heading = "Optimizely Commerce DEMO";
                    p.MainContentArea
                    .AddItem<TeaserBlock>(b =>
                    {
                        b.Heading = ResourceHelpers.Faker.Lorem.Slug();
                        b.LeadText = ResourceHelpers.Faker.Lorem.Paragraph();
                    });
                });

                b.UseAssets(referenceConverter.GetRootLink())
                .WithContent<CatalogContent>(x =>
                {
                    x.Name = "Catalog 1";
                    x.DefaultCurrency = "EUR";
                    x.DefaultLanguage = "en";
                    x.WeightBase = "kgs";
                    x.LengthBase = "cm";
                }, l1 => l1.WithContent<FashionCategory>(x => x.Name = "Men", l2 =>
                            l2.WithContent<FashionCategory>(x => x.Name = "Shoes", l3 =>
                                l3.WithContent<FashionProduct>(x => x.Name = "Product 1", l4 =>
                                    l4
                                    .WithContent<FashionVariant>(v => v.Name = "Variant 1")
                                    .WithContent<FashionVariant>(v => v.Name = "Variant 2"))
                                .WithContent<FashionProduct>(x => x.Name = "Product 2")
                                .WithContent<FashionProduct>(x => x.Name = "Product 3")
                            ).WithContent<FashionCategory>(x => x.Name = "Accessories", l3 =>
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

        app.UseMiddleware<PreSendHeadersMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapContent();
        });
    }
}
