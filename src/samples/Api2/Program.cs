namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseSwagger(o =>
        {
            o.AddYarp("api2");
        });
        app.UseSwaggerUI();

        app.MapControllers();

        app.Run();
    }
}
