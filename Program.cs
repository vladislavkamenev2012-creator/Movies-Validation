using Microsoft.EntityFrameworkCore;
using MVC_1.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
    // Валідація керується лише атрибутами моделі (без неявного Required для string)
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;

    // Українські повідомлення вбудованого зв'язування моделей
    var messages = options.ModelBindingMessageProvider;
    messages.SetValueMustBeANumberAccessor(name => $"Поле «{name}» має містити число.");
    messages.SetAttemptedValueIsInvalidAccessor((value, name) => $"Значення «{value}» некоректне для поля «{name}».");
    messages.SetValueIsInvalidAccessor(value => $"Значення «{value}» некоректне.");
    messages.SetValueMustNotBeNullAccessor(value => "Заповніть це поле.");
    messages.SetMissingKeyOrValueAccessor(() => "Заповніть це поле.");
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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