namespace CultBook07.model;

public interface IAutenticavel
{
    string Login { get; }
    bool ValidarSenha(string senha);
}
