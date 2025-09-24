using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.GraphQL;
using Server.GraphQL.DataLoaders;
using Server.GraphQL.Types;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPooledDbContextFactory<EfContext>(options =>
{
	options.UseInMemoryDatabase("InMemoryDb");
});

builder.Services.AddHostedService<DataSeeder>();

//this solves for the graphql n+1 issue with ef. preloads and caches id lookups (green donut)
builder.Services.AddDataLoader<AislesByStoreIdLoader>();
builder.Services.AddDataLoader<BaysByAisleIdLoader>();
builder.Services.AddDataLoader<ShelvesByBayIdLoader>();
builder.Services.AddDataLoader<ProductsByShelfIdLoader>();
builder.Services.AddDataLoader<ProductsbyCategoryIdLoader>();

builder.Services.AddGraphQLServer()
	.AddQueryType<Query>()
	.AddType<StoreType>()
	.AddType<AisleType>()
	.AddType<BayType>()
	.AddType<ShelfType>()
	.AddType<ProductType>()
	.AddType<CategoryType>()
	.AddFiltering()
	.AddSorting()
	.AddProjections();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapGraphQL("");
app.Run();

