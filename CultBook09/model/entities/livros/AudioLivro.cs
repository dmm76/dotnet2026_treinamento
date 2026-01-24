namespace CultBook09.model.entities.livros;

using System.Text;

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
        decimal preco,
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

    //Obrigatoria a implementação do método abstrato da classe base Livro
    public override decimal CalcularPrecoTotal()
    {
        return Preco;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(base.ToString());
        sb.AppendLine($"Tempo de Duração: {TempoDuracao} minutos");
        sb.AppendLine($"Narrador: {Narrador}");
        return sb.ToString();
    }
}
