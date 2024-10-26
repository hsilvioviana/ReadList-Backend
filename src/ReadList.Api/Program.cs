using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ReadList.Api;
using ReadList.Application.AutoMapper;
using ReadList.CrossCutting;
using ReadList.Infraestructure.Context;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .Build();

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddConfiguration(configuration);
});

// Add services to the container.

builder.Services.AddDependencies(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

    setup.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "ReadList API",
        Description = "Com ReadList um usuário poderá criar uma conta e salvar livros lidos, o programa irá gerar estátisticas baseadas nos livros cadastrados (Autores mais lidos, Gêneros mais lidos, Anos com maiores leituras, etc...). Este programa foi construído para que um usuário possa ter um registro de todos os livros lidos e conseguir ver como sua relação com a leitura mudou com o passar dos anos, se ele mudou sua prefêrencia em relação com os gêneros literários ou se a quantidade de livros lidos por ano sofreu alguma alteração com o passar do tempo, por exemplo. Logo abaixo, está todos os endpoints do backend para o seu uso.",
        Version = "v1",
        Contact = new OpenApiContact()
        {
            Name = "Silvio Viana",
            Email = "hsilvioviana@hotmail.com",
            Url = new Uri("https://www.linkedin.com/in/hsilvioviana/")
        }
    });

    var xmlPath = Path.Combine(AppContext.BaseDirectory, "ReadList.Api.xml");

    setup.IncludeXmlComments(xmlPath);
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton<IMapper>(s => AutoMapperSetup.RegisterMapping().CreateMapper());

builder.Services.AddAuthentication(x =>
{	
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWT:SecretKey"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetService<PostgresDbContext>();
    dbContext.Database.Migrate();
}

app.Run();
