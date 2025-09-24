using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.GraphQL.DataLoaders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EfContext>(options =>
	options.UseInMemoryDatabase("InMemoryDb"));

builder.Services.AddPooledDbContextFactory<EfContext>(options =>
{
	options.UseInMemoryDatabase("InMemory");
});

//this solves for the graphql n+1 issue with ef. preloads and caches id lookups (green donut)
builder.Services.AddDataLoader<AislesByStoreIdLoader>();
builder.Services.AddDataLoader<BaysByAisleIdLoader>();
builder.Services.AddDataLoader<ShelvesByBayIdLoader>();
builder.Services.AddDataLoader<ProductsByShelfIdLoader>();
builder.Services.AddDataLoader<ProductsbyCategoryIdLoader>();
var app = builder.Build();

app.UseHttpsRedirection();



app.Run();

