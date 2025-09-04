using Hangfire;
using Hangfire.Console;
using Hangfire.SqlServer;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// 1. Configura��o do Hangfire
builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection"))
    .UseConsole()); // Habilita a extens�o Hangfire.Console

// 2. Adiciona o servidor do Hangfire que processar� os jobs
builder.Services.AddHangfireServer();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// 3. Adiciona o Dashboard do Hangfire
app.UseHangfireDashboard(); // Por padr�o, acess�vel em /hangfire

app.MapControllers();

// 4. (Opcional) Rota para enfileirar um job de teste
app.MapGet("/testjob", () =>
{
    // Enfileira um job simples
    var jobId = BackgroundJob.Enqueue(() => Console.WriteLine("Ol� do Hangfire!"));
    return $"Job {jobId} enfileirado!";
});

app.Run();