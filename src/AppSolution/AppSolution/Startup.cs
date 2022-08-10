using API.Middleware;
using AppSolution.Extention;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AppSolution
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
            services.AddControllers();
            services.AddSwaggerExtention("AppSolution", "v1");
            services.AddDBExtention(Configuration);
            services.AddIdentityExtention();
            services.AddopenIddictExtention();
            services.AddStackExchangeRedisCache(
                options =>
                {
                    options.Configuration = Configuration["RedisCacheUrl:Url"];
                    options.InstanceName = Configuration["RedisCacheUrl:Database"];
                });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AppSolution v1"));
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionFormattingMiddleware>();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}