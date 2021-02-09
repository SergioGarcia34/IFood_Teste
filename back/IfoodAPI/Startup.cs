using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IfoodAPI.Models;
using Microsoft.EntityFrameworkCore;
using IfoodAPI.Middleware;
using IfoodAPI.Services;
using IfoodAPI.Repository;

namespace IfoodAPI
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
            var Connection = Configuration.GetConnectionString("IfoodDB");
            
            services.AddDbContext<AppDBContext>(c => c.UseLazyLoadingProxies().UseSqlServer(Connection));


            services.AddControllers();
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddTokenAuthentication(Configuration);
            services.AddSingleton<ValidacaoServices>();
            services.AddScoped<IRepository, Repository<AppDBContext>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Apenas para fins de demonstra��o, deixei todas as origens e cabe�alhos permitidos. 
            app.UseCors(options =>
                options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("Authorization"));

            app.UseHttpsRedirection();
            app.UseMiddleware<BasicAuthMiddleware>();

            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
