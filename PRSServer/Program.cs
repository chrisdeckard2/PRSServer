using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PRSServer.Data;
namespace PRSServer;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<PRSServerContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("PRSServerContext") ?? throw new InvalidOperationException("Connection string 'PRSServerContext' not found.")));

        // Add services to the container.

        builder.Services.AddControllers();

       /// builder.Services.AddCors();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseAuthorization();

        ///app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

        app.MapControllers();

        app.Run();
    }
}

//http://localhost:5001
