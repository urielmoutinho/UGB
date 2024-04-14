using Microsoft.EntityFrameworkCore;
using UgbApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurando o ASP.NET Core

// Adiciona o serviço de controllers ao contêiner de injeção de dependência.
builder.Services.AddControllers();

// Adiciona suporte à documentação da API usando o Swagger.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurando o DbContext

// Adiciona o DbContext para UgbDbContext usando a string de conexão "DbConnectionString".
builder.Services.AddDbContext<UgbDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString")));

var app = builder.Build();

var connectionString = Environment.GetEnvironmentVariable("DbConnectionString");

// Configuração do pipeline de requisições HTTP.

// Se estiver no ambiente de desenvolvimento, ativa o uso do Swagger para documentação da API.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redireciona as requisições HTTP para HTTPS.
app.UseHttpsRedirection();

// Configuração de política de CORS para permitir qualquer origem, método e cabeçalho.
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// Middleware de autorização (caso seja usado algum esquema de autenticação/autorização).
app.UseAuthorization();

// Mapeia os endpoints dos controllers.
app.MapControllers();

// Inicia a execução da aplicação.
app.Run();