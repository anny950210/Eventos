using WebApiRest.AccesoDatos;
using WebApiRest.BaseDatos.Core;
using WebApiRest.BaseDatos.DAO;
using WebApiRest.Negocio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddSingleton<EventosDAO>();
builder.Services.AddTransient<IBusinessContext, BusinessContext>();
builder.Services.AddTransient<IDatosContext, DatosContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();