using AutoMapper;
using MicroservicesProject.Application.Interfaces;
using MicroservicesProject.Application.UseCases;
using MicroservicesProject.Infrastructure;
using MicroservicesProject.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Conexao com banco de dados usano ORM EF.
builder.Services.AddInfrastructure(builder.Configuration);
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
});

builder.Services.AddSingleton(mapperConfig.CreateMapper());
builder.Services.AddAuthentication("Bearer").AddJwtBearer();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
