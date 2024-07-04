using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UniTutor.DataBase;
//using UniTutor.Configuration;
using UniTutor.Interface;
using UniTutor.Repository;
using UniTutor.Services;
using Microsoft.OpenApi.Models;
using AutoMapper;
using UniTutor.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// CORS Configuration (if needed)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("*")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Adding DB context
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ??
        throw new InvalidOperationException("Connection String is not found"));
});
//Adding AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configuring JWT Authentication
var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });


// Add Email Configuration
var emailConfig = builder.Configuration
    .GetSection("EmailConfiguration")
    .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddTransient<IEmailService, EmailService>();


builder.Services.AddScoped<IAdmin, AdminRepository>();
builder.Services.AddScoped<IStudent, StudentRepository>();
builder.Services.AddScoped<ITutor, TutorRepository>();
builder.Services.AddScoped<ISubject, SubjectRepository>();
builder.Services.AddTransient<IPasswordService, PasswordService>();

// Register Swagger generator
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "UniTutor API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "UniTutor API v1");
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();/*{
  "id": 0,
  "firstName": "sdyueuadb",
  "lastName": "jdcsucb",
  "grade": 3,
  "address": "cdnsihvisn",
  "homeTown": 4,
  "phoneNumber": "0777602179",
  "email": "nilaxsanala2001@gmail.com",
  "password": "nilax@123",
  "verificationCode": "string",
  "fileName": "string",
  "contentType": "string",
  "data": "string"
}*/
