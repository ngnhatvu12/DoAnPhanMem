using DoAnPhanMem.Data;
using DoAnPhanMem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Thêm DbContext cho ứng dụng (sử dụng cho Identity và dữ liệu của ứng dụng)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Thêm DbContext riêng cho các model khác nếu cần
builder.Services.AddDbContext<dbSportStoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Sử dụng cùng chuỗi kết nối

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Cấu hình Identity với Entity Framework
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true; // Yêu cầu người dùng xác nhận email
})
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Cấu hình bộ nhớ cache để lưu trữ session
builder.Services.AddDistributedMemoryCache(); // Bộ nhớ cache cho session

// Cấu hình session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllersWithViews(); // Thêm các dịch vụ MVC với view

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Thêm middleware để xử lý session
app.UseSession(); // Cấu hình session middleware

app.UseAuthorization();

// Cấu hình route mặc định cho ứng dụng
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Cấu hình route cho sản phẩm chi tiết
app.MapControllerRoute(
    name: "productDetail",
    pattern: "product/{id}",
    defaults: new { controller = "Home", action = "ProductDetail" });

// Cấu hình route cho Admin
app.MapControllerRoute(
    name: "admin",
    pattern: "{controller=Admin}/{action=Dashboard}/{id?}");

// Cấu hình Razor Pages cho các trang đăng nhập, đăng ký, v.v...
app.MapRazorPages();
app.MapRazorPages();
app.MapControllerRoute(
   name: "admin",
   pattern: "{controller=Admin}/{action=Dashboard}/{id?}");
app.Run();