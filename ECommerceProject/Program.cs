using System.Text.Json.Serialization;
using DotNetEnv;
using ECommerceProject.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateSlimBuilder(args);

Env.Load();

builder
    .Services
    .ConfigureHttpJsonOptions(options =>
    {
        options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
    });

builder.Services.AddDbContext<ECommerceDbContext>(options =>
{
    string pass = Env.GetString("PASSWORD");
    string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (connectionString != null) {
        connectionString = connectionString.Replace("myPassword", pass);
        options.UseMySql(connectionString, new MySqlServerVersion(new Version(9, 0, 1)));
    }
});

var app = builder.Build();

var productsApi = app.MapGroup("/products");
productsApi.MapGet("/", (ECommerceDbContext db) => {
    var res = db.Products.ToList();
    return Results.Ok(res);
});

productsApi.MapGet(
    "/{id}",
    (ECommerceDbContext db, int id) =>
        db.Products.Find(id) is Product product ? Results.Ok(product) : Results.NotFound()
);

productsApi.MapPost("/", (ECommerceDbContext db, Product product) =>
{
    db.Products.Add(product);
    db.SaveChanges();
    return Results.Created($"/products/{product.ProductId}", product);
});

var usersApi = app.MapGroup("/users");



app.Run();

[JsonSerializable(typeof(Product))]
[JsonSerializable(typeof(Product[]))]
[JsonSerializable(typeof(User))]
internal partial class AppJsonSerializerContext : JsonSerializerContext { }
