using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Demo.AspNetCore.Mvc.Http2.Mvc.Razor;

namespace Demo.AspNetCore.Mvc.Http2
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.Configure<RazorViewEngineOptions>(options => options.ViewLocationExpanders.Add(new Http2ViewLocationExpander()));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles()
            .UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller=Demo}/{action=ConditionalRendering}");
            })
            .Run(async (context) =>
            {
                await context.Response.WriteAsync($"-- Demo.AspNetCore.Mvc.Http2 ({context.Request.Protocol}) --");
            });
        }
    }
}
