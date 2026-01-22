namespace CultBook05.model;

public class AudioLivro : Livro
{
    public int TempoDuracao { get; set; }
    public string Narrador { get; set; }

    public AudioLivro(
        string isbn,
        string titulo,
        string descricao,
        string autor,
        int estoque,
        double preco,
        string figura,
        int dataCadastro,
        string categoria,
        int tempoDuracao,
        string narrador
    )
        : base(isbn, titulo, descricao, autor, estoque, preco, figura, dataCadastro, categoria)
    {
        TempoDuracao = tempoDuracao;
        Narrador = narrador;
    }

    public override string ToString()
    {
        return $"Tipo: ÁudioLivro\n"
            + base.ToString()
            + "\n"
            + $"Duração (min): {TempoDuracao}\n"
            + $"Narrador: {Narrador}"
            + $"\n------------------------------\n";
    }
}
