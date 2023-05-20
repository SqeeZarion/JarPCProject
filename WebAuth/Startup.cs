using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fligoo.Framework.Base.Api.Cors;
using JarPControlProject.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
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
        
        // Додати Swagger
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        });
    
        services.AddControllers();

        Console.WriteLine("dskds");
        // Конфігурація налаштувань автентифікації
        var authOptionsConfiguration = Configuration.GetSection("AuthOption");
        services.Configure<AuthOption>(authOptionsConfiguration);

        services.AddDbContext<DBConnection>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("SqlServerCounnStr"), 
                sqlOptions => sqlOptions.EnableRetryOnFailure()));
       

            //відповідає за додавання служби Cross-Origin Resource Sharing до контейнера служб сервісу.
            //CORS дозволяє веб-сайтам або веб-додаткам обмінюватися ресурсами з іншого джерела,
            //якщо це джерело має інший домен, порт або протокол.
            //AddCors - метод розширення служб сервісу, який додає службу CORS до контейнера служб сервісу.
            //Параметр options представляє об'єкт, який містить налаштування CORS.
            //Метод AddDefaultPolicy додає політику за замовчуванням, яка дозволяє будь-якому джерелу  отримувати доступ до ресурсів,
            //наданих цим сервером, та використовувати будь-який метод та заголовок запиту.
            //builder - це об'єкт, який надає можливість настроювати політику CORS,
            //включаючи обмеження доступу до ресурсів, дозволені методи HTTP і заголовки запитів.
            
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });
    }

    //Метод Configure використовується для вказівки того, як додаток повинен реагувати на
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        
        // Додати Swagger
        app.UseSwagger();
        
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        });
        
        if (env.IsDevelopment())
            //щоб відображати детальну інформацію про помилки у веб-браузері.
            app.UseDeveloperExceptionPage();
        
        app.UseRouting();

        app.UseCors();

        app.UseEndpoints(endpoints =>
        {
            //знаходить всі контролери
            endpoints.MapControllers();
        });
    }
}