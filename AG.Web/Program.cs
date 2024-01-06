using AG.Web.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Database;
using Services.Domains.Security;
using Services.Session;
using System;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IDepartmentsService, DefaultDepartmentsService>()
                .AddTransient<IEmployeeService, DefaultEmployeeService>();
//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
{
    opt.LoginPath = "/login";
    opt.AccessDeniedPath = "/accessdenied";
});
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapGet("/login", async (HttpContext context) =>
{
    var claims = new List<Claim>()
    {
        new Claim(ClaimsIdentity.DefaultNameClaimType, "Administrator"),
        new Claim(ClaimsIdentity.DefaultRoleClaimType, CustomRoles.ADMIN),
    };
    var identity = new ClaimsIdentity(claims, "Cookies");
    var principal = new ClaimsPrincipal(identity);
    await context.SignInAsync(principal);
    return Results.Redirect("/");
});


app.MapGet("/admin",[Authorize(Roles = CustomRoles.ADMIN)] async (HttpContext context) => {
    var user = context.User;
    var name = user.FindFirst(ClaimsIdentity.DefaultNameClaimType)?.Value ?? "None";
    var role = user.FindFirst(ClaimsIdentity.DefaultRoleClaimType)?.Value ?? "None";
    await context.Response.WriteAsync($"User: {name}\nRole: {role}");
});

app.MapGet("/accessdenied", async (HttpContext context) =>
{
    await context.Response.WriteAsync("<h1>Access denied</h1>");
});

app.MapGet("/logout", async (HttpContext context) =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
});

app.Run();
