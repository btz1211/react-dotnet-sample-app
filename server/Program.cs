using Microsoft.EntityFrameworkCore;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddDbContext<AppDbContext>(o => {
    o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddScoped<IHouseRepository, HouseRepository>();
builder.Services.AddScoped<IBidRepository, BidRepository>();
builder.Services.AddScoped<IHouseController, HouseController>();
builder.Services.AddScoped<IBidController, BidController>();
builder.Services.AddScoped<AppDbOptionsBuilder>(sp => new AppDbOptionsBuilder(sp.GetRequiredService<IConfiguration>()));


if (builder.Environment.IsProduction() || builder.Environment.IsStaging()) {
    builder.Configuration.AddAzureKeyVault(
        new Uri($"https://{builder.Configuration["KeyVaultName"]}.vault.azure.net/"),
        new DefaultAzureCredential(new DefaultAzureCredentialOptions()));
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(p => p.WithOrigins("http://localhost:3000")
.AllowAnyHeader().AllowAnyMethod());

app.UseHttpsRedirection();
app.UseHouseEndpoints();
app.UseBidEndpoints();

app.MapGet("/", (IConfiguration config) =>
    string.Join(
        Environment.NewLine,
        "SecretName (Name in Key Vault: 'SecretName')",
        @"Obtained from configuration with config[""SecretName""]",
        $"Value: {config["KeyVaultName"]}",
        "",
        "Section:SecretName (Name in Key Vault: 'Section--SecretName')",
        @"Obtained from configuration with config[""Section:SecretName""]",
        $"Value: {config["Section:SecretName"]}",
        "",
        "Section:SecretName (Name in Key Vault: 'Section--SecretName')",
        @"Obtained from configuration with config.GetSection(""Section"")[""SecretName""]",
        $"Value: {config.GetSection("ConnectionStrings")["WebApiDatabase"]}"));

app.Run();

public partial class Program { }