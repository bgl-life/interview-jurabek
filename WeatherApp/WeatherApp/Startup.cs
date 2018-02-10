using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WeatherApp.Abstraction.Facades;
using WeatherApp.Abstraction.ModelBuilders;
using WeatherApp.Abstraction.Services;
using WeatherApp.Facades;
using WeatherApp.ModelBuilders;
using WeatherApp.Services;

namespace WeatherApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
	        services.AddAutoMapper();
			services.AddScoped<IWeatherResultViewModelBuilder, WeatherResultViewModelBuilder>();
	        services.AddScoped<IOpenWeatherMapService, OpenWeatherMapService>();
	        services.AddScoped<IHttpClientFacade, HttpClientFacade>();
        }
		
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
