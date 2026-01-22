namespace CultBook07.model;

public class Cliente : IAutenticavel
{
    private string _senha;

    public string Nome { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }

    public Cliente(string nome, string login, string email, string senha)
    {
        Nome = nome;
        Login = login;
        Email = email;
        _senha = senha;
    }

    public void SetSenha(string novaSenha)
    {
        _senha = novaSenha;
    }

    public bool ValidarSenha(string senha)
    {
        return _senha == senha;
    }
}
