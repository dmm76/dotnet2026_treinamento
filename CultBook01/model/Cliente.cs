namespace CultBook01.model;

class Cliente
{
    private string Nome { get; set; }
    private string Login { get; set; }
    private string Senha { get; set; }
    private string Email { get; set; }
    private string Fone { get; set; }
    private bool Logado { get; set; }

    public Cliente(string nome)
    {
        Nome = nome;
        Logado = false;
    }

    public Cliente(string nome, string login, string senha, string email, string fone, bool logado = false)
    {
        Nome = nome;
        Login = login;
        Senha = senha;
        Email = email;
        Fone = fone;
        Logado = logado;
    }

    public void Mostrar()
    {
        Console.WriteLine($"Nome: {Nome} | Login: {Login} | Senha: {Senha} | Email: {Email} | Fone: {Fone} | Logado: {Logado}");
    }

    public void VerificarLogin()
    {
        //Logado = true;
        if (!Logado)
        {
            
            Console.WriteLine($"Usuário: {Nome} não está logado");
        }
        else
        {
            
            Console.WriteLine($"Usuário: {Nome} já está logado");
        }
    }
}
