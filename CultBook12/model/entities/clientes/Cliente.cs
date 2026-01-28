namespace CultBook12.model.entities.clientes;

using System.Text;
using CultBook12.model.entities.pedidos;

public class Cliente
{
    public string Nome { get; set; }
    public string? Login { get; set; }
    private string Senha { get; set; } = "";
    public string? Email { get; set; }
    public string? Fone { get; set; }
    public bool Logado { get; set; }

    //ajustes para o lab011

    private const int LIMITE_ENDERECOS = 5;

    private List<Endereco> Enderecos { get; set; } = new();
    private List<Pedido> Pedidos { get; set; } = new();

    public Cliente(
        string nome,
        string login,
        string senha,
        string email,
        string fone,
        bool logado = false
    )
    {
        Nome = nome?.Trim() ?? "";
        Login = login?.Trim() ?? "";
        Senha = senha?.Trim() ?? "";
        Email = email?.Trim() ?? "";
        Fone = fone?.Trim() ?? "";
        Logado = logado;
    }

    public List<Endereco> GetEnderecos()
    {
        return Enderecos;
    }

    public List<Pedido> GetPedidos()
    {
        return Pedidos;
    }

    public void InserirEndereco(Endereco endereco)
    {
        if (Enderecos.Count >= LIMITE_ENDERECOS)
        {
            Console.WriteLine("Limite de endereços atingido.");
            return;
        }

        Enderecos.Add(endereco);
    }

    public void InserirPedido(Pedido pedido)
    {
        Pedidos.Add(pedido);
    }

    public bool VerificarSenha(string? senhaDigitada)
    {
        return Senha == (senhaDigitada ?? "");
    }

    public void AlterarSenha(string senhaAtual, string novaSenha)
    {
        if (!VerificarSenha(senhaAtual))
            throw new Exception("Senha atual incorreta.");

        if (string.IsNullOrWhiteSpace(novaSenha))
            throw new Exception("Nova senha inválida.");

        Senha = novaSenha.Trim();
    }

    public void VerificarLogin()
    {
        if (Logado)
        {
            Console.WriteLine("Cliente já está logado.");
        }
        else
        {
            //Simulação de processo de login
            Logado = true;
            Console.WriteLine($"Cliente {Nome} logado com sucesso.");
        }
    }

    public void Mostrar()
    {
        Console.WriteLine(ToString());
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("=== Cliente ===");
        sb.AppendLine($"Nome: {Nome}");
        sb.AppendLine($"Login: {Login}");
        sb.AppendLine($"Email: {Email}");
        sb.AppendLine($"Fone: {Fone}");
        sb.AppendLine();

        sb.AppendLine("Enderecos:");
        for (int i = 0; i < Enderecos.Count; i++)
        {
            sb.AppendLine($"--- Endereco {i + 1} ---");
            sb.AppendLine(Enderecos[i].ToString());
            sb.AppendLine();
        }

        sb.AppendLine("Pedidos:");
        for (int i = 0; i < Pedidos.Count; i++)
        {
            sb.AppendLine($"--- Pedido {i + 1} ---");
            sb.AppendLine(Pedidos[i].ToString());
            sb.AppendLine();
        }

        return sb.ToString().TrimEnd();
    }
}
