using CultBook12.model.interfaces;

namespace CultBook12.model.usecases.clientes;

public class LoginClienteUseCase
{
    private readonly IClienteRepositorio repo;

    public LoginClienteUseCase(IClienteRepositorio repo)
    {
        this.repo = repo;
    }

    public void Executar(string login, string senha)
    {
        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(senha))
            throw new Exception("Login e senha são obrigatórios.");

        var cliente = repo.BuscarPorLogin(login.Trim());
        if (cliente == null)
            throw new Exception("Login não encontrado.");

        if (!cliente.VerificarSenha(senha))
            throw new Exception("Senha incorreta.");

        if (cliente.Logado)
            throw new Exception("Cliente já está logado.");

        cliente.Logado = true;

        // em repositório fake por referência, isso é opcional,
        // mas como sua interface tem Atualizar, fica padronizado:
        repo.Atualizar(cliente);
    }
}
