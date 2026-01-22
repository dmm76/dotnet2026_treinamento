namespace CultBook04.model;

public class Cliente
{
    public string Nome { get; set; }
    public string Login { get; set; }
    private string Senha { get; set; }
    public string Email { get; set; }
    public string Fone { get; set; }
    public bool Logado { get; set; }

    //ajustes para o lab04

    private Endereco[] enderecos;
    private Pedido[] pedidos;

    private int qtdEnderecos = 0;
    private int qtdPedidos = 0;

    public Cliente(string nome)
    {
        Nome = nome;
        Logado = false;
        enderecos = new Endereco[5]; //limite de 5 endereços por cliente
        pedidos = new Pedido[10]; //limite de 10 pedidos por cliente
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
        enderecos = new Endereco[5]; //limite de 5 endereços por cliente
        pedidos = new Pedido[10]; //limite de 10 pedidos por cliente
    }

    public Endereco[] GetEnderecos()
    {
        return enderecos;
    }

    public Pedido[] GetPedidos()
    {
        return pedidos;
    }

    public void InserirEndereco(Endereco endereco)
    {
        if (qtdEnderecos >= enderecos.Length)
        {
            Console.WriteLine("Limite de endereços atingido.");
            return;
        }
        enderecos[qtdEnderecos] = endereco;
        qtdEnderecos++;
    }

    public void InserirPedido(Pedido pedido)
    {
        if (qtdPedidos >= pedidos.Length)
        {
            Console.WriteLine("Limite de pedidos atingido.");
            return;
        }
        pedidos[qtdPedidos] = pedido;
        qtdPedidos++;
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
        for (int i = 0; i < qtdEnderecos; i++)
        {
            if (enderecos[i] != null)
                enderecos[i].Mostrar();
        }

        for (int i = 0; i < qtdPedidos; i++)
        {
            if (pedidos[i] != null)
                pedidos[i].Mostrar();
        }
    }
}
