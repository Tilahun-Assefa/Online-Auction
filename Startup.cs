using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using OnlineAuction.Data;
using OnlineAuction.Models;
using OnlineAuction.services.ReviewService;
using OnlineAuction.Services.CategoryService;
using OnlineAuction.Services.ProductCategoryService;
using OnlineAuction.Services.ProductService;
using System.IO;
using System.Reflection;

namespace OnlineAuction
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
            //Add EntityFramework support for SqlServer services.
            //AddEntityFrameworkSqlServer() DataContext
            services.AddDbContext<DataContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            services.AddControllers().AddJsonOptions(options =>
            {
                //set this option to true to indent the JSON output
                options.JsonSerializerOptions.WriteIndented = true;
                //set this option to NULL to use PascalCase instead of CamelCase(default)
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.DictionaryKeyPolicy = null;
            });
            services.AddCors();

            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            //add asp.net core identity support
            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<DataContext>();

            services.AddIdentityServer().AddApiAuthorization<ApplicationUser, DataContext>();
            services.AddAuthentication().AddIdentityServerJwt();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();            

            app.UseRouting();

            //configuration to allow request from angular port 4200
            app.UseCors(options =>
            options.WithOrigins("http://localhost:4200", "http://localhost")
            .AllowAnyMethod()
            .AllowAnyHeader());

            //add .webmanifest MIME-type support
            FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".webmanifest"] = "application/manifest+json";

            app.UseStaticFiles(new StaticFileOptions()
            {
                ContentTypeProvider = provider,
                OnPrepareResponse = (context) =>
                {
                    if (context.File.Name == "isOnline.txt")
                    {
                        //disable caching for these files
                        context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                        context.Context.Response.Headers.Add("Expires", "-1");
                    }
                    else
                    {
                        //Retrieve cache configuration from appsettings.json
                        context.Context.Response.Headers["Cache-Control"] = Configuration["StaticFiles:Headers:Cache-Control"];
                        context.Context.Response.Headers["Pragma"] = Configuration["StaticFiles:Headers:Pragma"];
                        context.Context.Response.Headers["Expires"] = Configuration["StaticFiles:Headers:Expires"];
                    }
                }
            });

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
