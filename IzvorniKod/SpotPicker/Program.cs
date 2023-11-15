using Microsoft.EntityFrameworkCore;
using SpotPicker.Model;
using SpotPicker.Service;
using SpotPicker.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SpotPickerContext>(
        options => options.UseSqlServer("Server=tcp:spotpicker.database.windows.net,1433;Initial Catalog=SpotPicker;Persist Security Info=False;User ID=letecimedvjedici;Password=SpotPicker123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
builder.Services.AddScoped<IKorisnikService, KorisnikService>();
builder.Services.AddCors();

var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SpotPicker");

        // Add this line to specify the Swagger UI URL
        c.RoutePrefix = string.Empty;
    });
}

app.UseStaticFiles();
app.UseCors("AllRequestPolicy");
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
