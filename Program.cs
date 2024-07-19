using Microsoft.EntityFrameworkCore;
using ReponsitoryPatternExample.Data;
using ReponsitoryPatternExample.Reponsitory;
using System;

namespace ReponsitoryPatternExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();


            //add entity 
            builder.Services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>
               (opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("WebDbContext")));
            //register reponsitory
            builder.Services.AddScoped(typeof(IReponsitory<>), typeof(Reponsitory<>));
            //register automapper
            builder.Services.AddAutoMapper(typeof(Program));




            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
