using API.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

//Registrar o serviço de banco de dados na aplicação
builder.Services.AddDbContext<AppDataContext>();

builder.Services.AddCors(options =>
    options.AddPolicy("Acesso total",
    configs => configs
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod())
);

var app = builder.Build();

//EndPoints - Funcionalidades
//GET: http://localhost:5225/
app.MapGet("/", () => "Minha primeira API em C# com watch");

//GET: http://localhost:5225/api/produto/listar
app.MapGet("/api/produto/listar",
    ([FromServices] AppDataContext ctx) =>
{
    if (ctx.Produtos.Any())
    {
        return Results.Ok(ctx.Produtos.ToList());
    }
    return Results.NotFound("Tabela vazia!");
});

//GET: http://localhost:5225/api/produto/buscar/id_do_produto
app.MapGet("/api/produto/buscar/{id}", (string id,
    [FromServices] AppDataContext ctx) =>
{
    //Expressão lambda em c#
    Produto? produto =
        ctx.Produtos.FirstOrDefault(x => x.Id == id);
    if (produto is null)
    {
        return Results.NotFound("Produto não encontrado!");
    }
    return Results.Ok(produto);
});

//POST: http://localhost:5225/api/produto/cadastrar
app.MapPost("/api/produto/cadastrar",
    ([FromBody] Produto produto,
    [FromServices] AppDataContext ctx) =>
{
    //Adicionar o produto dentro do banco de dados
    ctx.Produtos.Add(produto);
    ctx.SaveChanges();
    return Results.Created("", produto);
});

app.UseCors("Acesso total");
app.Run();

