using LAB8_David_Belizario.Services;
using LAB8_David_Belizario.Services.IServices;
using LAB8_David_Belizario.UnitOfWork;
using LAB8_David_Belizario.UnitOfWork.IUnitOfWork;
using Microsoft.EntityFrameworkCore;
using DbContext = LAB8_David_Belizario.Data.DbContext;

var builder = WebApplication.CreateBuilder(args);

// conexion a MySQL appsettings.json
builder.Services.AddDbContext<DbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("Lab8Connection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Lab8Connection"))
    )
);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IClientQueryService, ClientQueryService>();
builder.Services.AddScoped<IProductQueryService, ProductQueryService>();
builder.Services.AddScoped<IOrderQueryService, OrderQueryService>();

// controladores con vistas y Swagger
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
//1111114411111
// Swagger visible en desarrollo y produccion
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lab08 API v1");
    c.RoutePrefix = string.Empty;
});

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
//comentario 1
//comentario 2
//comentario 1
//comentario 2
//comentario 1
//comentario 2
//comentario 1
//comentario 2