var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/send-message", (HttpContext context) =>
{
    
});

app.Run();
