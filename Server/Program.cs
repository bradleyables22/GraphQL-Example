using Microsoft.EntityFrameworkCore;
using Server.Data;
using System;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

builder.Services.AddDbContext<EfContext>(options =>
	options.UseInMemoryDatabase("InMemoryDb"));


app.UseHttpsRedirection();



app.Run();

