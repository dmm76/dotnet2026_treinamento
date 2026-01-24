namespace CultBook08.model.dtos;

public class LivroDetalheDto
{
    public string Isbn { get; set; } = "";
    public string Titulo { get; set; } = "";
    public string Descricao { get; set; } = "";
    public string Autor { get; set; } = "";
    public int Estoque { get; set; }
    public decimal Preco { get; set; }
    public string Figura { get; set; } = "";
    public int DataCadastro { get; set; }
    public string Categoria { get; set; } = "";

    // extras (opcionais)
    public string Tipo { get; set; } = "";
    public double? TamanhoMB { get; set; }
    public int? TempoDuracao { get; set; }
    public string? Narrador { get; set; }
    public double? Peso { get; set; }
    public decimal? ValorFrete { get; set; }
}
