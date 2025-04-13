using BankCreditSystem.Data;
using BankCreditSystem.web.Services;  // Добавляем пространство имён сервиса
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Получаем строку подключения из appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Регистрируем ApplicationDbContext с использованием SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Регистрируем Identity. Используем стандартного IdentityUser и IdentityRole.
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Регистрируем наш сервис оценки кредитоспособности
builder.Services.AddScoped<ICreditRiskService, CreditRiskService>();

// Если нужно использовать готовые страницы Identity, регистрируем Razor Pages
builder.Services.AddRazorPages();

// Добавляем поддержку контроллеров с представлениями (MVC)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Настройка middleware

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Обязательно добавляем middleware аутентификации и авторизации
app.UseAuthentication();
app.UseAuthorization();

// Настройка маршрутов для контроллеров
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Маршруты для Razor Pages (Identity UI)
app.MapRazorPages();

app.Run();
