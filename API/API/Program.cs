using System.ComponentModel.DataAnnotations;
using API.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

//Registrar o serviço de banco de dados na aplicação
builder.Services.AddDbContext<AppDataContext>();

var app = builder.Build();

List<Produto> produtos = new List<Produto>();
produtos.Add(new Produto("Celular", "IOS", 4000));
produtos.Add(new Produto("Celular", "Android", 2500));
produtos.Add(new Produto("Televisão", "LG", 2000));
produtos.Add(new Produto("Notebook", "Avell", 5000));

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
app.MapGet("/api/produto/buscar/{id}", ([FromRoute] string id, [FromServices] AppDataContext ctx) =>
{   
    //Expressão lambda em c#
    Produto? produto = ctx.Produtos.FirstOrDefault(x => x.Id == id);
    if (produto is null)
    {
        return Results.NotFound("Produto não encontrado!");
    }
    return Results.Ok(produto);
});

//POST: http://localhost:5225/api/produto/cadastrar
app.MapPost("/api/produto/cadastrar", ([FromBody] Produto produto, [FromServices] AppDataContext ctx) =>
{
    //Adicionar o produto dentro do banco de dados

    // validacao dos atributos do produto

    List<ValidationResult> erros = new List<ValidationResult>();

    if(!Validator.TryValidateObject(produto, new ValidationContext(produto), erros, true)){
        return Results.BadRequest(erros);
    }
    //Regra de negocio - nao permitir produtos com o mesmo nome


    Produto? produtoBuscado = ctx.Produtos.FirstOrDefault(x => x.Nome == produto.Nome);

    if(produtoBuscado == null){
        ctx.Produtos.Add(produto);
        ctx.SaveChanges();
        return Results.Created("", produto);
    }
        return Results.BadRequest("ja tem esse ai man");
   
});


//DELETE: http://localhost:5225/api/produto/deletar/id
app.MapDelete("/api/produto/deletar/{id}", ([FromRoute] string id, [FromServices] AppDataContext ctx) => 
{
    Produto? produto = ctx.Produtos.Find(id);
    if (produto is null)
    {
        return Results.NotFound("Produto não encontrado!");
    }
    ctx.Produtos.Remove(produto);
    ctx.SaveChanges();
    return Results.Ok("produto deletado!");
});

//PUT: http://localhost:5225/api/produto/alterar/id
app.MapPut("/api/produto/alterar/{id}", ([FromRoute] string id,[FromBody] Produto produtoAlterado,[FromServices] AppDataContext ctx) => 
{
    Produto? produto = ctx.Produtos.Find(id);
    if (produto is null)
    {
        return Results.NotFound("Produto não encontrado!");
    }

    produto.Nome = produtoAlterado.Nome;
    produto.Descricao = produtoAlterado.Descricao;
    produto.Quantidade = produtoAlterado.Quantidade;
    produto.Valor = produtoAlterado.Valor;

    ctx.Produtos.Update(produto);
    ctx.SaveChanges();
    return Results.Ok("produto atualized!");
});

app.Run();

