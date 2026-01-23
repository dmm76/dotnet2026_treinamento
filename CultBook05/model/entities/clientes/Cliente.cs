namespace CultBook05.model.entities.clientes;

using CultBook05.model.entities.pedidos;

public class Cliente
{
    public string Nome { get; set; }
    public string? Login { get; set; }
    private string? Senha { get; set; }
    public string? Email { get; set; }
    public string? Fone { get; set; }
    public bool Logado { get; set; }

    //ajustes para o lab04

    private Endereco[] Enderecos;
    private Pedido[] Pedidos;

    private int _qtdEnderecos = 0;
    private int _qtdPedidos = 0;

    public Cliente(string nome)
    {
        Nome = nome;
        Logado = false;
        Enderecos = new Endereco[5]; //limite de 5 endereços por cliente
        Pedidos = new Pedido[10]; //limite de 10 Pedidos por cliente
    }

    public Cliente(
        string nome,
        string login,
        string senha,
        string email,
        string fone,
        bool logado = false
    )
    {
        Nome = nome;
        Login = login;
        Senha = senha;
        Email = email;
        Fone = fone;
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
        Console.WriteLine(
            $"Nome: {Nome} | Login: {Login} | Email: {Email} | Fone: {Fone} | Logado: {Logado}"
        );
        Console.WriteLine("=== ENDEREÇOS ===");
        for (int i = 0; i < _qtdEnderecos; i++)
        {
            if (Enderecos[i] != null)
                Enderecos[i].Mostrar();
        }

        for (int i = 0; i < _qtdPedidos; i++)
        {
            if (Pedidos[i] != null)
                Pedidos[i].Mostrar();
        }
    }

    public override string ToString()
    {
        string resultado =
            "=== Cliente ==="
            + Environment.NewLine
            + $"Nome: {Nome}"
            + Environment.NewLine
            + $"Login: {Login}"
            + Environment.NewLine
            + $"Senha: {Senha}"
            + Environment.NewLine
            + $"Email: {Email}"
            + Environment.NewLine
            + $"Fone: {Fone}"
            + Environment.NewLine;

        resultado += "Enderecos:" + Environment.NewLine;
        for (int i = 0; i < _qtdEnderecos; i++)
        {
            resultado += $"--- Endereco {i + 1} ---" + Environment.NewLine;
            resultado += Enderecos[i].ToString() + Environment.NewLine;
        }

        resultado += "Pedidos:" + Environment.NewLine;
        for (int i = 0; i < _qtdPedidos; i++)
        {
            resultado += $"--- Pedido {i + 1} ---" + Environment.NewLine;
            resultado += Pedidos[i].ToString() + Environment.NewLine;
        }

        return resultado.TrimEnd();
    }
}
