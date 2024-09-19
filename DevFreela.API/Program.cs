using DevFreela.API.Filters;
using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetAllSkills;
using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Application.Services.Implementations;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.Validators;
using DevFreela.Core.Payments;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Auth;
using DevFreela.Infrastructure.MessageBus;
using DevFreela.Infrastructure.Payments;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Persistence.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();


builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter)));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProjectCommandValidator>();

var connectionString = builder.Configuration.GetConnectionString("DevFreelaCs");

builder.Services.AddDbContext<DevFreelaDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddHttpClient();

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IMessageBusService, MessageBusService>();

builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssemblyContaining(typeof(CreateProjectCommand)));
builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssemblyContaining(typeof(CreateCommentCommand)));
builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssemblyContaining(typeof(DeleteProjectCommand)));
builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssemblyContaining(typeof(CreateProjectCommand)));
builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssemblyContaining(typeof(FinishProjectCommand)));
builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssemblyContaining(typeof(StartProjectCommand)));
builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssemblyContaining(typeof(UpdateProjectCommand)));
builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssemblyContaining(typeof(GetAllProjectsQuery)));
builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssemblyContaining(typeof(GetAllSkillsQuery)));
builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssemblyContaining(typeof(GetProjectByIdQuery)));
builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssemblyContaining(typeof(CreateUserCommand)));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DevFreela.API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header usando o esquema Bearer."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                     {
                           new OpenApiSecurityScheme
                             {
                                 Reference = new OpenApiReference
                                 {
                                     Type = ReferenceType.SecurityScheme,
                                     Id = "Bearer"
                                 }
                             },
                             new string[] {}
                     }
                 });
});

builder.Services
  .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,

          ValidIssuer = configuration["Jwt:Issuer"],
          ValidAudience = configuration["Jwt:Audience"],
          IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
      };
  });

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