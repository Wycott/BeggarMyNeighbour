using BeggarMyNeighbourLibrary.Messaging;
using BeggarMyNeighbourWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Supplies simulator results to the dashboard, pulled live from the simulators over MQTT.
builder.Services.AddSingleton<ISimulatorResultsProvider, MqttSimulatorResultsProvider>();

// Broker + discovery configuration, bound from appsettings.json (and environment overrides).
builder.Services.Configure<MqttSettings>(builder.Configuration.GetSection(MqttSettings.SectionName));
builder.Services.Configure<DiscoverySettings>(builder.Configuration.GetSection(DiscoverySettings.SectionName));
builder.Services.Configure<DashboardSettings>(builder.Configuration.GetSection(DashboardSettings.SectionName));

// Discovers connected simulators over MQTT by polling them on each dashboard load.
builder.Services.AddSingleton<ISimulatorDiscoveryProvider, MqttSimulatorDiscoveryProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
