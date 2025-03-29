using guslinks.Components;
using guslinks.Components.Services;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient<TokenAuthenticationProvider>();
builder.Services.AddScoped<TokenAuthenticationProvider>();

builder.Services.AddHttpClient<AuthenticationStateProvider, TokenAuthenticationProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthenticationProvider>(
  provider => provider.GetRequiredService<TokenAuthenticationProvider>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
