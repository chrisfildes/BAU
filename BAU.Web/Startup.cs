using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using BAU.Data.Interfaces;
using BAU.Data.EntityFramework;
using BAU.Business.Interfaces;
using BAU.Business.Services;


namespace BAU
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
            services.AddMvc();

             // Database Context
            services.AddDbContext<EngineerRepository>(options =>options.UseSqlServer(Configuration.GetConnectionString("DBConnectionString")));
            services.AddDbContext<SupportSlotRepository>(options => options.UseSqlServer(Configuration.GetConnectionString("DBConnectionString")));


            // Register Application Services
            services.AddScoped<IEngineerRepository, EngineerRepository>();
            services.AddScoped<ISupportSlotRepository, SupportSlotRepository>();

            services.AddScoped<IEngineerService, EngineerService>();
            services.AddScoped<IScheduleService, ScheduleService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
