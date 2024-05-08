
using DashBoard_API_BiblioPro.Context;
using Microsoft.EntityFrameworkCore;

namespace DashBoard_API_BiblioPro
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //Usuario sa, contraseña 123, base de datos BiblioProReportes, servidor localhost, trusted connection
            string connectionString = "Data Source=Localhost;Initial Catalog=BiblioProReportes;user id=sa;password=123;TrustServerCertificate=True";

            builder.Services.AddDbContext<ContextoBiblioPro>(options =>
                options.UseSqlServer(connectionString));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
