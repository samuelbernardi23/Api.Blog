using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TopCon.Api.Data;
using TopCon.Api.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddCors(options => options.AddPolicy(name: "FrontApp",
    policy =>
    {
        policy.WithOrigins("https://localhost:3000")
        .AllowAnyHeader()
        .AllowCredentials()
        .AllowAnyMethod();
    }));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = false; // Apenas acessível no lado do servidor
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // Use Secure apenas se a requisição for via HTTPS
    options.Cookie.Name = "YourAppCookieName"; // Nome do cookie
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        context.Response.ContentType = "application/json";

        var response = new { reconnect = true };
        return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
    };

    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        context.Response.ContentType = "application/json";

        var response = new { forbidden = true };
        return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
    };
});

builder.Services.AddScoped<ISeedUserRole, SeedUserRole>();

var app = builder.Build();
await CriarPerfisUsuariosAsync(app);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("FrontApp");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();


async Task CriarPerfisUsuariosAsync(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<ISeedUserRole>();
        await service.SeedRolesAsync();
        await service.SeedUserAsync();
    }
}