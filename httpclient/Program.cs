using httpclient.Service;
using httpclient.ServiceInterface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IFinnhubService, FinHubService>();

builder.Services.Configure<OptionsClass>
    (builder.Configuration.GetSection("Parameters"));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();
