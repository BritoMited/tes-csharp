var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//GET - http://localhost:5189/
app.MapGet("/", () => "Minha primeira API em C#");

//GET - http://localhost:5189/api/produto/listar
app.MapGet("/api/produto/listar", () => "Listagem de Produtos");

//POST - http://localhost:5189/api/produto/cadastrar
app.MapPost("/api/produto/cadastrar", () => "Cadastrando os Produtos");

app.Run();
