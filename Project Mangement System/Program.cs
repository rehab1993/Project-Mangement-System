
using Microsoft.EntityFrameworkCore;
using Project_Mangement_System.Data;
using Project_Mangement_System.Features.ProjectManagement.Projects;
using Project_Mangement_System.MiddleWares;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace Project_Mangement_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // **Register DbContext**
            builder.Services.AddDbContext<Context>(options =>
                        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                           .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                           .LogTo(log => Debug.WriteLine(log), LogLevel.Information)

                           .EnableSensitiveDataLogging()
                        );

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //Add Auto Mapper
            builder.Services.AddAutoMapper(typeof(Program));
          
            //Add Mediator
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            //General Repos
            builder.Services.AddScoped(typeof(IProjectRepository<>), typeof(ProjectRepository<>));
            //CAP Library
            builder.Services.AddCap(cfg =>
            {
                // Use your DB (for transaction logging & outbox pattern)
                cfg.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                cfg.UseEntityFramework<Context>();

                // Use RabbitMQ as the message broker
                cfg.UseRabbitMQ(options =>
                {
                    options.HostName = "localhost"; // or your RabbitMQ server
                    options.Port = 15672;
                    options.UserName = "guest";
                    options.Password = "guest";
                    options.ExchangeName = "cap.default.router";
                });

                // Optional: enable CAP dashboard at /cap
               
            });

            var app = builder.Build();
            // Add the global exception middleware
            app.UseMiddleware<GlobalExceptionMiddleWare>();
            app.UseMiddleware<TransactionMiddleWare>();

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
