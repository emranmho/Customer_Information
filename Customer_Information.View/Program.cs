using Customer_Information.View.Services;
using Customer_Information.View.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddHttpClient<ICustomerService, CustomerService>();
builder.Services.AddHttpClient<ICountryService, CountryService>();
builder.Services.AddHttpClient<IAddressService, AddressService>();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IAddressService, AddressService>();
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
    pattern: "{controller=Customer}/{action=CustomerInfo}/{id?}");

app.Run();
