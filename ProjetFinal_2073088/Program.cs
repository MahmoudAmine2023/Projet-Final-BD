using Microsoft.EntityFrameworkCore;
using ProjetFinal_2073088.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ProjetFinal_MaillotsContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("ProjetFinal_Maillots")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name:"default",
        pattern:"{controller=Maillots}/{action=Index}"
        );
});

app.MapRazorPages();

app.Run();
