
using DashBoard_API_BiblioPro.Context;
using Microsoft.EntityFrameworkCore;

namespace DashBoard_API_BiblioPro
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            string connectionString = "Data Source=Localhost;Initial Catalog=BiblioProReportes;" +
                                      "user id=sa;password=123;TrustServerCertificate=True";

            builder.Services.AddDbContext<ContextoBiblioPro>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                                        builder =>
                                        {
                                            builder.AllowAnyOrigin();
                                            builder.AllowAnyMethod();
                                            builder.AllowAnyHeader();
                                        });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.UseCors("AllowAllOrigins");

            app.MapControllers();
            app.Run();
        }
    }
}
