using System.Data.SqlClient;
using JarPControlProject.Database;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        try {
            using (SqlConnection conn =  DBConnection.GetConnection()) {
                conn.Open();
                Console.WriteLine("Підключення до бази даних встановлено успішно.");
                // Ваш код для взаємодії з базою даних
            }
        } catch (Exception ex) {
            Console.WriteLine("Помилка при підключенні до бази даних: " + ex.Message);
        }

// // Add services to the container.
//
//         builder.Services.AddControllers();
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//         builder.Services.AddEndpointsApiExplorer();
//         builder.Services.AddSwaggerGen();
//
//         var app = builder.Build();
//
// // Configure the HTTP request pipeline.
//         if (app.Environment.IsDevelopment())
//         {
//             app.UseSwagger();
//             app.UseSwaggerUI();
//         }
//
//         app.UseHttpsRedirection();
//
//         app.UseAuthorization();
//
//         app.MapControllers();
//
//         app.Run();
    }
}