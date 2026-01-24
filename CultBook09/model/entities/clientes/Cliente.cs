namespace CultBook09.model.entities.clientes;

using System.Text;
using CultBook09.model.entities.pedidos;

public class Cliente
{
    public string Nome { get; set; }
    public string? Login { get; set; }
    private string Senha { get; set; } = "";
    public string? Email { get; set; }
    public string? Fone { get; set; }
    public bool Logado { get; set; }

    //ajustes para o lab04

    private Endereco[] Enderecos;
    private Pedido[] Pedidos;

    private int _qtdEnderecos = 0;
    private int _qtdPedidos = 0;

    // public Cliente(string nome)
    // {
    //     Nome = nome;
    //     Logado = false;
    //     Enderecos = new Endereco[5]; //limite de 5 endereços por cliente
    //     Pedidos = new Pedido[10]; //limite de 10 Pedidos por cliente
    // }

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
        Enderecos = new Endereco[5]; //limite de 5 endereços por cliente
        Pedidos = new Pedido[10]; //limite de 10 Pedidos por cliente
    }

    public Endereco[] GetEnderecos()
    {
        return Enderecos;
    }

    public Pedido[] GetPedidos()
    {
        return Pedidos;
    }

    public void InserirEndereco(Endereco endereco)
    {
        if (_qtdEnderecos >= Enderecos.Length)
        {
            Console.WriteLine("Limite de endereços atingido.");
            return;
        }
        Enderecos[_qtdEnderecos] = endereco;
        _qtdEnderecos++;
    }

    public void InserirPedido(Pedido pedido)
    {
        if (_qtdPedidos >= Pedidos.Length)
        {
            Console.WriteLine("Limite de Pedidos atingido.");
            return;
        }
        Pedidos[_qtdPedidos] = pedido;
        _qtdPedidos++;
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
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("=== Cliente ===");
        sb.AppendLine($"Nome: {Nome}");
        sb.AppendLine($"Login: {Login}");
        sb.AppendLine($"Email: {Email}");
        sb.AppendLine($"Fone: {Fone}");
        sb.AppendLine();

        sb.AppendLine("Enderecos:");
        for (int i = 0; i < _qtdEnderecos; i++)
        {
            sb.AppendLine($"--- Endereco {i + 1} ---");
            sb.AppendLine(Enderecos[i].ToString());
            sb.AppendLine();
        }

        sb.AppendLine("Pedidos:");
        for (int i = 0; i < _qtdPedidos; i++)
        {
            sb.AppendLine($"--- Pedido {i + 1} ---");
            sb.AppendLine(Pedidos[i].ToString());
            sb.AppendLine();
        }

        Console.WriteLine(sb.ToString().TrimEnd());
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
        for (int i = 0; i < _qtdEnderecos; i++)
        {
            sb.AppendLine($"--- Endereco {i + 1} ---");
            sb.AppendLine(Enderecos[i].ToString());
            sb.AppendLine();
        }

        sb.AppendLine("Pedidos:");
        for (int i = 0; i < _qtdPedidos; i++)
        {
            sb.AppendLine($"--- Pedido {i + 1} ---");
            sb.AppendLine(Pedidos[i].ToString());
            sb.AppendLine();
        }

        return sb.ToString().TrimEnd();
    }
}
