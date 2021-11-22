using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using ShoppingApp.Common;
using ShoppingApp.DataAccess.DataAccess;
using ShoppingApp.DataAccess.IDataAccess;
using ShoppingApp.Services.IServices;
using ShoppingApp.Models.Model;
using ShoppingApp.Services.Validation;
using System.IO;
using ShoppingApp.Services.Services;

namespace ShoppingApp
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
            Log.Logger = new LoggerConfiguration()
                         .ReadFrom.Configuration(Configuration)
                         .WriteTo.File($"{Directory.GetCurrentDirectory()}\\Logs\\Log.txt")
                         .CreateLogger();

            services.AddDbContext<ShoppingDbContext>(option => option.UseSqlServer(Configuration["ConnectionStrings:Name"]));
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IDBServices, DBServices>();
            services.AddScoped<ICartDbService, CartDbServices>();
            services.AddScoped<IOrderAndPaymentDBServices, OrderAndPaymentDbServices>();
            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<ICartServices, CartServices>();
            services.AddScoped<IUserDbServices, UserDbServices>();
            services.AddScoped<IUserDetailServices, UserDetailServices>();
            services.AddScoped<IOrderPaymentDetails, OrderPaymentDetails>();
            services.AddScoped<ProductSortAndFilter>();
            services.AddScoped<UserValidationAttribute>();
            services.AddScoped<ExceptionAttribute>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShoppingApp", Version = "v1" });
            });

            services.Configure<SignUpDetails>(Configuration.GetSection("AuthDetails:SignupDetails"));
            services.Configure<LoginDetails>(Configuration.GetSection("AuthDetails:LoginDetails"));

            // 1. Add Authentication Services
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = Configuration["AuthDetails:Authority"];
                options.Audience = Configuration["AuthDetails:Audience"];
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/error");
            if (env.IsDevelopment())
            {
                //app.UseExceptionHandler("/error");
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShoppingApp v1"));
            }

            app.UseSerilogRequestLogging();
            // 2. Enable authentication middleware
            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
