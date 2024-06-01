using Competitions.Application.Services;
using Competitions.Core.Abstractions.Auth;
using Competitions.Core.Abstractions.CoachesAbstractions;
using Competitions.Core.Abstractions.CompetitionsAbstractions;
using Competitions.Core.Abstractions.KindOfSportsAbstractions;
using Competitions.Core.Abstractions.StudentsAbstractions;
using Competitions.Core.Abstractions.TeamsAbstractions;
using Competitions.Core.Abstractions.UniversittiesAbstractions;
using Competitions.Core.Abstractions.UsersAbstractions;
using Competitions.DataAccess;
using Competitions.DataAccess.Repositories;
using Competitions.Extensions;
using Competitions.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
services.AddApiAuthentication(configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

services.AddDbContext<CompetitionsDbContext>(
    options =>
    {
        options.UseNpgsql(configuration.GetConnectionString(nameof(CompetitionsDbContext)));
    });

builder.Services.AddScoped<ICoachesService, CoachesService>();
builder.Services.AddScoped<ICoachesRepository, CoachesRepository>();

builder.Services.AddScoped<ICompetitionsService, CompetitionsService>();
builder.Services.AddScoped<ICompetitionsRepository, CompetitionsRepository>();

builder.Services.AddScoped<IKindOfSportsService, KindOfSportsService>();
builder.Services.AddScoped<IKindOfSportsRepository, KindOfSportsRepository>();

builder.Services.AddScoped<IStudentsService, StudentsService>();
builder.Services.AddScoped<IStudentsRepository, StudentsRepository>();

builder.Services.AddScoped<ITeamsService, TeamsService>();
builder.Services.AddScoped<ITeamsRepository, TeamsRepository>();

builder.Services.AddScoped<IUniversitiesService, UniversitiesService>();
builder.Services.AddScoped<IUniversitiesRepository, UniversitiesRepository>();

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<UsersService>();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors(x =>
{
    x.AllowAnyOrigin();
    x.AllowAnyMethod();
    x.AllowAnyHeader();
   /* x.WithHeaders().AllowAnyHeader();
    x.WithOrigins("http:/localhost:3000");
    x.WithMethods().AllowAnyMethod();*/
});

app.Run();
