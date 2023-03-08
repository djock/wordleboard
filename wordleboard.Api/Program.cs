using Microsoft.EntityFrameworkCore;
using wordleboard.Db;
using wordleboard.Models;
using wordleboard.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("WordleboardDbContextConnection") ?? throw new InvalidOperationException("Connection string 'WordleboardDbContextConnection' not found.");

builder.Services.AddDbContext<WordleBoardDbContext>(options =>
    options.UseSqlServer(connectionString));

//builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
//{
//    options.SignIn.RequireConfirmedAccount = false;
//    options.Password.RequireNonAlphanumeric = false;
//    options.Password.RequireDigit = false;
//    options.Password.RequireLowercase = false;
//    options.Password.RequireUppercase = false;
//    options.User.RequireUniqueEmail = false;
//})
//    .AddEntityFrameworkStores<WordleBoardDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBoardRepository, BoardRepository>();
builder.Services.AddScoped<IWordleRepository, WordleRepository>();

builder.Services.AddScoped<IBoardService, BoardService>();
builder.Services.AddScoped<IWordleService, WordleService>();
builder.Services.AddScoped<IWordleAnswersService, WordleAnswersService>();
builder.Services.AddScoped<IWordDefinitionService, WordDefinitionService>();

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapControllers();

app.Run();
