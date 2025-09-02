using MiniChat.Model;
using MiniChat.DBServices;
using MiniChat.RealTimeConnection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();

app.MapHub<Connection>("/mini-chat-connection");


app.MapGet("/get-all-messages", () =>
{
    MessageDb database = new MessageDb();
    
    return Results.Json(database.GetMessages(), statusCode: 200);
});

app.Run();
