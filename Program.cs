using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var invitations = new List<Invitation>();

var app = builder.Build();


app.MapGet("/", async context =>
{
    context.Response.ContentType = "text/html";
    await context.Response.SendFileAsync("wwwroot/index.html");
});

app.MapGet("/invite", async context =>
{
    context.Response.ContentType = "text/html";
    await context.Response.SendFileAsync("wwwroot/invite.html");
});

app.MapPost("/invite", async context =>
{
    var form = await context.Request.ReadFormAsync();
    string? name = form["name"];
    string? email = form["email"];
    string? phone = form["phone"];

    invitations.Add(new Invitation
    {
        Name = name,
        Email = email,
        Phone = phone
    });

    context.Response.Redirect("/thankyou");
});

app.MapGet("/thankyou", async context =>
{
    context.Response.ContentType = "text/html";
    await context.Response.SendFileAsync("wwwroot/thankyou.html");
});

app.MapGet("/invitations", async context =>
{
    context.Response.ContentType = "application/json";
    await context.Response.WriteAsJsonAsync(invitations);
});

app.Run();

public class Invitation
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}
