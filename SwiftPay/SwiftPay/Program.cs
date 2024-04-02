using Microsoft.EntityFrameworkCore;
using Radzen;
using SwiftPay.Components;
using SwiftPay.DAL;
using SwiftPay.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

var ConStr = builder.Configuration.GetConnectionString("ConStr");
builder.Services.AddDbContext<Context>(options => options.UseSqlite(ConStr));


//Services
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<EmpresaService>();
builder.Services.AddScoped<SalidaCajaService>();
builder.Services.AddScoped<DetalleReganteService>();
builder.Services.AddScoped<ParametrosService>();
builder.Services.AddScoped<RegantesService>();
builder.Services.AddScoped<UsuariosService>();
builder.Services.AddScoped<PagosService>();
builder.Services.AddScoped<DetallePagosService>();
builder.Services.AddScoped<AutentificacionService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();
