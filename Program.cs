using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProiectEB.Data;
using ProiectEB.Hubs;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProiectEBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProiectEBContext") ?? throw new InvalidOperationException("Connection string 'ProiectEBContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSignalR();

builder.Services.AddSingleton<ProiectEB.Services.ChatBotService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<ChatHub>("/chatHub");

app.Run();
