using WebGames.Infrastructure.Extensions;
using WebGames.Application.Extensions;
using WebGames.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

var app = builder.Build();

IServiceScope scope = app.Services.CreateScope();
GameSeeder gameSeeder = scope.ServiceProvider.GetService<GameSeeder>()!;
await gameSeeder.Seed();

UserSeeder userSeeder = scope.ServiceProvider.GetService<UserSeeder>()!;
await userSeeder.Seed();

RoleSeeder roleSeeder = scope.ServiceProvider.GetService<RoleSeeder>()!;
await roleSeeder.Seed();

UserRoleSeeder userRoleSeeder = scope.ServiceProvider.GetService<UserRoleSeeder>()!;
await userRoleSeeder.Seed();

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
    pattern: "{controller=WebGames}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();

public partial class Program { } // for testing