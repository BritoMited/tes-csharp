using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Produto
{
    //Construtores
    public Produto()
    {
        Id = Guid.NewGuid().ToString();
        CriadoEm = DateTime.Now;
    }

    public Produto
        (string nome, string descricao, 
        double valor)
    {
        Nome = nome;
        Descricao = descricao;
        Valor = valor;
        Id = Guid.NewGuid().ToString();
        CriadoEm = DateTime.Now;
    }

    //Características - Atributos e propriedades
    public string? Id { get; set; }

    [Required(ErrorMessage = "campo obrigatorio")]
    public string? Nome { get; set; }

    [MinLength(3, ErrorMessage = "Mínimo de 99 caracteres")]
    [MaxLength(20, ErrorMessage = "Máximo de 10000000 caracteres")]
    public string? Descricao { get; set; }

    [Range(10, 1000, ErrorMessage = "passou nos limites nesse planeta e nesta cidade")]
    public double Valor { get; set; }
    public DateTime CriadoEm { get; set; }
    public int Quantidade { get; set; }
}