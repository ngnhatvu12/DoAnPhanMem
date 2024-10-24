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
    .AddEntityFrameworkStores<ApplicationDbContext>(); // Sử dụng ApplicationDbContext cho Identity

// Cấu hình bộ nhớ cache để lưu trữ session
builder.Services.AddDistributedMemoryCache(); // Bộ nhớ cache cho session (cần thiết cho session lưu trữ tạm thời)

// Cấu hình session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian session timeout là 30 phút
    options.Cookie.HttpOnly = true; // Cookie session chỉ có thể truy cập qua HTTP, bảo mật hơn
    options.Cookie.IsEssential = true; // Đánh dấu cookie là cần thiết
});

builder.Services.AddControllersWithViews(); // Thêm các dịch vụ MVC với view

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint(); // Hiển thị lỗi chi tiết cho nhà phát triển
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Trang lỗi chung cho người dùng
    app.UseHsts(); // Sử dụng HSTS để bảo vệ trang web
}

app.UseHttpsRedirection(); // Chuyển hướng sang HTTPS nếu người dùng truy cập qua HTTP
app.UseStaticFiles(); // Cho phép truy cập các file tĩnh (CSS, JS, hình ảnh)

app.UseRouting(); // Kích hoạt routing cho các request

// Thêm middleware để xử lý session
app.UseSession(); // Cấu hình session middleware để ứng dụng có thể sử dụng session

app.UseAuthorization(); // Kích hoạt xác thực quyền người dùng

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

app.Run(); // Chạy ứng dụng

app.MapControllerRoute(
   name: "admin",
   pattern: "{controller=Admin}/{action=Dashboard}/{id?}");
