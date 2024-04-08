using FoodExpress.DriverMicroservice.Data;
using FoodExpress.DriverMicroservice.Services;
using FoodExpress.MenuMicroservice.Services;
using FoodExpress.RelationData;
using FoodExpress.RestaurantMicroservice.Services;
using FoodExpress.UserMicroservice.Data;
using FoodExpress.UserMicroservice.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<UserDbContext>(options =>
options.UseMySQL(builder.Configuration.GetConnectionString("MySQLCS")));

builder.Services.AddDbContext<DriverDbContext>(options =>
options.UseMySQL(builder.Configuration.GetConnectionString("MySQLCS")));

builder.Services.AddDbContext<DataContext>(options =>
options.UseMySQL(builder.Configuration.GetConnectionString("MySQLCS")));



builder.Services.AddScoped<IRestaurantServices, RestaurantServices>();
builder.Services.AddScoped<IMenuServices, MenuServices>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IDriverServices, DriverServices>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
