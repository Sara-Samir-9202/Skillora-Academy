using Microsoft.EntityFrameworkCore;
using StudentSystem.Models;
using StudentSystem.Mapping;
using StudentSystem.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ================= MVC =================
builder.Services.AddControllersWithViews();

// ================= DbContext =================
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// ================= AutoMapper =================
builder.Services.AddAutoMapper(typeof(MappingProfile));

// ================= Build =================
var app = builder.Build();

// ================= Custom Middleware =================
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseMiddleware<LoggingMiddleware>();

// ================= Default Middleware =================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// ================= Routes =================
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}"
);

app.Run();