using CultBook08.model.entities.clientes;
using CultBook08.model.interfaces;

namespace CultBook08.model.usecases.clientes;

public class CadastrarClienteUseCase
{
    private readonly IClienteRepositorio repo;

    public CadastrarClienteUseCase(IClienteRepositorio repo)
    {
        this.repo = repo;
    }

    public void Executar(string nome, string login, string senha, string email, string fone)
    {
        if (
            string.IsNullOrWhiteSpace(nome)
            || string.IsNullOrWhiteSpace(login)
            || string.IsNullOrWhiteSpace(senha)
        )
            throw new Exception("Nome, Login e Senha são obrigatórios.");

        if (repo.LoginExiste(login))
            throw new Exception("Esse login já existe. Escolha outro.");

        var cliente = new Cliente(
            nome.Trim(),
            login.Trim(),
            senha.Trim(),
            (email ?? "").Trim(),
            (fone ?? "").Trim()
        );

        repo.Adicionar(cliente);
    }
}
