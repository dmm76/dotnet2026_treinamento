namespace CultBook07.model;

public class ServicoAutenticacao
{
    public void RealizarLogin(IAutenticavel usuario, string senhaTentativa)
    {
        if (usuario.ValidarSenha(senhaTentativa))
        {
            Console.WriteLine($"Login realizado com sucesso para o usuário: {usuario.Login}");
        }
        else
        {
            Console.WriteLine("Senha inválida. Acesso negado.");
        }
    }
}
