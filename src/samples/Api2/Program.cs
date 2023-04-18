namespace Api2;

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
            // The prefix is set here explicitly.
            // For reading the prefix from the X-Forwarded-Prefix header see https://github.com/ctyar/Swashbuckle.Yarp/blob/main/src/samples/Api1/Program.cs#L21
            o.AddYarp("api2");
        });
        app.UseSwaggerUI();

        app.MapControllers();

        app.Run();
    }
}
