var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
   name: "Country",
   pattern: "{area:exists}/{controller=Country}/{action=Index}/{id?}");

app.MapControllerRoute(
   name: "City",
   pattern: "{area:exists}/{controller=City}/{action=Index}/{id?}");

app.MapControllerRoute(
     name: "State",
      pattern: "{area:exists}/{controller=State}/{action=Index}/{id?}");

app.MapControllerRoute(
   name: "Branch",
   pattern: "{area:exists}/{controller=Branch}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");





app.Run();
