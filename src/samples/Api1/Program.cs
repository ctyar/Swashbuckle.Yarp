namespace Api1;

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
            // YARP for this sample is configured to set the X-Forwarded-Prefix header.
            // See https://github.com/ctyar/Swashbuckle.Yarp/blob/main/src/samples/Yarp/appsettings.json#L13-L20
            // For explicitly setting the prefix see https://github.com/ctyar/Swashbuckle.Yarp/blob/main/src/samples/Api2/Program.cs#L20
            o.AddYarp();
        });
        app.UseSwaggerUI();

        app.MapControllers();

        app.Run();
    }
}
