
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Shop.entities;
using Shop.helpers;
using Shop.repositories;
using Shop.repositories.RepositoryImpl;
using Shop.services;
using Shop.services.ServiceImpl;
using System.Text;

namespace Shop.api
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
            services.AddCors();
            services.AddControllers();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddDbContext<DataContext>(option => option.UseSqlServer(appSettings.DefaultConnection, b => b.MigrationsAssembly("Shop.api")));
            // configure DI for application services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepositoryImpl>();

            services.AddTransient<IBlogService, BlogService>();
            services.AddTransient<IBlogRepository, BlogRepositoryImpl>();

            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICategoryRepository, CategoryRepositoryImpl>();

            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IOrderRepository, OrderRepositoryImpl>();

            services.AddTransient<ICompositionService, CompositionService>();
            services.AddTransient<ICompositionRepository, CompositionRepositoryImpl>();

            services.AddTransient<IOrderDetailService, OrderDetailService>();
            services.AddTransient<IOrderDetailRepository, OrderDetailRepositoryImpl>();

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductRepository, ProductRepositoryImpl>(); 

            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<ICommentRepository, CommentRepositoryImpl>();

            services.AddTransient<IBrandService, BrandService>();
            services.AddTransient<IBrandRepository, BrandRepositoryImpl>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"assets")),
                RequestPath = new PathString("/assets")
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
