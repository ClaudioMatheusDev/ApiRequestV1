using APIRequest.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APIRequest.Models;

[Table("Produtos")]
public class Produto
{
    [Key]
    public int ProdutoID { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(15, ErrorMessage ="O nome deve ter entre 5 e 15 caracteres", MinimumLength = 5)]
    [PrimeiraLetraMaiscula]
    public string? Nome { get; set; }

    [Required]
    [StringLength(10, ErrorMessage = "A descricao deve ter mo máximo {1} caracteres")]
    public string? Descricao { get; set; }

    [Required]
    [Column(TypeName="decimal(10,2)")]
    [Range(1, 10000, ErrorMessage = "O preço deve estar entre {1} e {2}")]
    public decimal Preco { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }
    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }

    public int CategoriaID { get; set; }

    [JsonIgnore]
    public Categoria? Categoria { get; set; }

}
