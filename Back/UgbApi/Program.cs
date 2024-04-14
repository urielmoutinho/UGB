using Microsoft.EntityFrameworkCore;
using UgbApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurando o ASP.NET Core

// Adiciona o servi�o de controllers ao cont�iner de inje��o de depend�ncia.
builder.Services.AddControllers();

// Adiciona suporte � documenta��o da API usando o Swagger.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurando o DbContext

// Adiciona o DbContext para UgbDbContext usando a string de conex�o "DbConnectionString".
builder.Services.AddDbContext<UgbDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString")));

var app = builder.Build();

var connectionString = Environment.GetEnvironmentVariable("DbConnectionString");

// Configura��o do pipeline de requisi��es HTTP.

// Se estiver no ambiente de desenvolvimento, ativa o uso do Swagger para documenta��o da API.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redireciona as requisi��es HTTP para HTTPS.
app.UseHttpsRedirection();

// Configura��o de pol�tica de CORS para permitir qualquer origem, m�todo e cabe�alho.
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// Middleware de autoriza��o (caso seja usado algum esquema de autentica��o/autoriza��o).
app.UseAuthorization();

// Mapeia os endpoints dos controllers.
app.MapControllers();

// Inicia a execu��o da aplica��o.
app.Run();