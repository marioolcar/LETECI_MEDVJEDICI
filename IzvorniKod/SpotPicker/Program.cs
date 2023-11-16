using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SpotPicker.Model;
using SpotPicker.Service;
using SpotPicker.Service.Interface;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SpotPickerContext>(
        options => options.UseSqlServer("Server=tcp:spotpicker.database.windows.net,1433;Initial Catalog=SpotPicker;Persist Security Info=False;User ID=letecimedvjedici;Password=SpotPicker123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
builder.Services.AddScoped<IKorisnikService, KorisnikService>();
//builder.Services.AddScoped<JwtTokenProvider>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllRequestPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("4a56f0a7c38b9d8e2a4f6d8c2b9e1d5adsfzesfdsfzjdsfdas"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

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

app.UseRouting();
app.UseCors("AllRequestPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
