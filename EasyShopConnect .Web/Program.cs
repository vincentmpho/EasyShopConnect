using EasyShopConnect_.Web.Models.Utility;
using EasyShopConnect_.Web.Services;
using EasyShopConnect_.Web.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add HttpContextAccessor to provide access to HttpContext within the application.
builder.Services.AddHttpContextAccessor();

// Add HttpClient to facilitate making HTTP requests.
builder.Services.AddHttpClient();

// Add HttpClient with specific interface IVoucherService and its implementation VoucherService.
builder.Services.AddHttpClient<IVoucherService, VoucherService>();

// Add scoped instances of BaseService and VoucherService to the service container.
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IVoucherService, VoucherService>();

// Set the base URL for the Voucher API from configuration settings.
StaticDetails.VoucherAPIBase = builder.Configuration["ServiceUrls:VoucherAPI"];

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

app.Run();
