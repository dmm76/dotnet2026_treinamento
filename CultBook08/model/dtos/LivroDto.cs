namespace CultBook08.model.dtos;

public class LivroDto
{
    public string Isbn { get; set; } = "";
    public string Titulo { get; set; } = "";
    public string Autor { get; set; } = "";
    public decimal Preco { get; set; }
    public int Estoque { get; set; }
    public string Categoria { get; set; } = "";
}
