using CultBook05.infra.data.factory;
using CultBook05.model.entities.clientes;

namespace CultBook05.model.usecases.clientes;

public class CadastrarClienteUseCase
{
    public void Executar(string nome, string login, string senha, string email, string fone)
    {
        if (
            string.IsNullOrWhiteSpace(nome)
            || string.IsNullOrWhiteSpace(login)
            || string.IsNullOrWhiteSpace(senha)
        )
            throw new Exception("Nome, Login e Senha são obrigatórios.");

        if (FabricaClientes.LoginExiste(login))
            throw new Exception("Esse login já existe. Escolha outro.");

        var cliente = new Cliente(
            nome.Trim(),
            login.Trim(),
            senha.Trim(),
            (email ?? "").Trim(),
            (fone ?? "").Trim()
        );

        if (!FabricaClientes.Inserir(cliente))
            throw new Exception("Não foi possível cadastrar: limite de clientes atingido.");
    }
}
