﻿namespace SportsStore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseMvc();
        }
    }
}
