using Application.ApplicationDependencyInjection;
using IdentityService.Application.Interfaces;
using IdentityService.Application.UseCases;
using Infrastructure.InfrastructureDependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication("Bearer").AddJwtBearer();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddApplication();
builder.Services.AddInfrastructue(builder.Configuration);


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
