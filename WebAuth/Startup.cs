using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebAuthCommon;

namespace WebApplication3;


public class Startup
{
    
    public IConfiguration Configuration { get; set; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    // використовується для додавання сервісів до контейнера
    public void ConfigureServices(IServiceCollection services)
    {
        
        services.AddControllers(); 
        
        var authOptionsConfiguration = Configuration.GetSection("Auth");
        services.Configure<AuthOption>(authOptionsConfiguration);
    }

    //Метод Configure використовується для вказівки того, як додаток повинен реагувати на
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            //щоб відображати детальну інформацію про помилки у веб-браузері.
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            
            //знаходить всі контролери
            
            endpoints.MapControllers(); 
        });
    }
}