using Competitions.Application.Services;
using Competitions.DataAccess;
using Competitions.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CompetitionsDbContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(CompetitionsDbContext)));
    });

builder.Services.AddScoped<CoachesService>();
builder.Services.AddScoped<CoachesRepository>();

builder.Services.AddScoped<CompetitionsService>();
builder.Services.AddScoped<CompetitionsRepository>();

builder.Services.AddScoped<KindOfSportsService>();
builder.Services.AddScoped<KindOfSportsRepository>();

builder.Services.AddScoped<StudentsService>();
builder.Services.AddScoped<StudentsRepository>();

builder.Services.AddScoped<TeamsService>();
builder.Services.AddScoped<TeamsRepository>();

builder.Services.AddScoped<UniversitiesService>();
builder.Services.AddScoped<UniversitiesRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
