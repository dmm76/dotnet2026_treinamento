using CultBook12.infra.data.repositorios;
using CultBook12.infra.repositories;
using CultBook12.model.interfaces;
using CultBook12.model.usecases.clientes;

var builder = WebApplication.CreateBuilder(args);

// Controllers (Hello e futuras APIs)
builder.Services.AddControllers();

// Repo fake/in-memory (singleton para manter estado "Logado")
builder.Services.AddSingleton<IClienteRepositorio, ClienteRepositorioFake>();
builder.Services.AddSingleton<ILivroRepositorio, LivroRepositorioFake>();

//Clientes Use Case
builder.Services.AddScoped<LoginClienteUseCase>();
builder.Services.AddScoped<LogoutClienteUseCase>();
builder.Services.AddScoped<ListarClientesUseCase>();
builder.Services.AddScoped<CadastrarClienteUseCase>();
builder.Services.AddScoped<AtualizarClienteUseCase>();
builder.Services.AddScoped<RemoverClienteUseCase>();

//Livros useCase

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Mapeia controllers
app.MapControllers();

app.Run();
