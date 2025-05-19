using AutoMapper.Execution;
using BookingSystem.BAL.Interfaces;
using BookingSystem.BAL.Services;
using BookingSystem.DAL.Interfaces;
using BookingSystem.DAL.Services;
using BookingSystem.Mapper.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(InventoryMapping));
builder.Services.AddAutoMapper(typeof(MemberMapping));

builder.Services.AddTransient<IDBExecuter, DBExecuter>();

builder.Services.AddTransient<IBookingBAL, BookingBAL>();

builder.Services.AddTransient<IInventoryClientBAL, InventoryClientBAL>();
builder.Services.AddTransient<IInventoryClient, InventoryClient>();

builder.Services.AddTransient<IMemberBAL, MemberBAL>();
builder.Services.AddTransient<IMemberDAL, MemberDAL>();

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
