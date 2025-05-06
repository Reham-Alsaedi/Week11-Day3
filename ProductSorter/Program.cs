using ProductAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

var sorter = new ProductSorter();
sorter.SortProducts();

app.MapControllers();

app.Run();
