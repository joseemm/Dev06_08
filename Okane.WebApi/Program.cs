using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Okane.Application;
using Okane.Storage.EntityFramework;
using Okane.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
            ValidIssuer = "https://okane.com",
            ValidAudience = "public",
            IssuerSigningKey = JwtTokenGenerator.SymmetricSecurityKey()
        };
    });

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IExpenseService, ExpenseService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<Func<DateTime>>(_ => () => DateTime.Now);
builder.Services.AddTransient<IExpensesRepository, ExpensesRepository>();
builder.Services.AddTransient<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddTransient<IUsersRepository, UsersRepository>();
builder.Services.AddTransient<IPasswordHasher, BCryptPasswordHasher>();
builder.Services.AddTransient<ITokenGenerator, JwtTokenGenerator>();
builder.Services.AddTransient<IUserSession, HttpContextUserSession>();
builder.Services.AddDbContext<OkaneDbContext>();

// builder.Services.AddSingleton<IExpensesRepository, InMemoryExpensesRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();