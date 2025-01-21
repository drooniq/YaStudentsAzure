
using Microsoft.EntityFrameworkCore;
using System;
using YaStudents.Data;

namespace YaStudents
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure Kestrel to listen on ports 80 and 81
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(80);  // Listening on port 80 inside the container
                options.ListenAnyIP(81);  // Listening on port 81 for HTTPS (optional)
            });

            // CORS configuration to allow any origin, method, and header
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // Add controllers and OpenAPI services
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            // Fetch the connection string from the environment variables
            var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");

            // Log and handle error if connection string is missing
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string is missing. Please set the environment variable 'DefaultConnection'.");
            }

            // Log the connection string for debugging purposes (ensure this is removed in production)
            builder.Logging.AddConsole();
            builder.Services.AddLogging();
            builder.Services.AddSingleton<ILogger>(sp =>
                sp.GetRequiredService<ILoggerFactory>().CreateLogger("Connection"));

            var logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();
            logger.LogInformation($"Using connection string: {connectionString}");

            // Register the DbContext with SQL Server
            builder.Services.AddDbContext<StudentsDBContext>(options =>
                options.UseSqlServer(connectionString,
                sqlOptions => sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null)));

            var app = builder.Build();

            // Configure the HTTP request pipeline for development and production environments
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();  // OpenAPI mapping for development
            }

            app.UseHttpsRedirection();  // Use HTTPS redirection

            app.UseAuthorization();  // Use authorization middleware

            // Map controllers
            app.MapControllers();

            // Run the app
            app.Run();
        }
    }
}
