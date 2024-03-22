var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Produto> produtos = new List<Produto>();
produtos.Add(new Produto("Celular","Android"));
produtos.Add(new Produto("Celular","Android"));
produtos.Add(new Produto("Celular","Android"));
produtos.Add(new Produto("Celular","Android"));

//GET - http://localhost:5189/
app.MapGet("/", () => "Minha primeira API em C#");

//GET - http://localhost:5189/api/produto/listar
app.MapGet("/api/produto/listar", () => produtos);

//POST - http://localhost:5189/api/produto/cadastrar
app.MapPost("/api/produto/cadastrar", () => "Cadastrando os Produtos");

app.Run();

record Produto(string Nome, string Descricao);
