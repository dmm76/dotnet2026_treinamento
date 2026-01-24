using CultBook08.model.entities.clientes;
using CultBook08.model.interfaces;

namespace CultBook08.infra.data.repositorios;

public class ClienteRepositorioFake : IClienteRepositorio
{
    private readonly List<Cliente> clientes = new();

    public List<Cliente> BuscarTodos() => clientes;

    public Cliente? BuscarPorLogin(string login)
    {
        if (string.IsNullOrWhiteSpace(login))
            return null;

        return clientes.FirstOrDefault(c =>
            !string.IsNullOrWhiteSpace(c.Login)
            && c.Login.Equals(login, StringComparison.OrdinalIgnoreCase)
        );
    }

    public bool LoginExiste(string login) => BuscarPorLogin(login) != null;

    public void Adicionar(Cliente cliente)
    {
        clientes.Add(cliente);
    }

    public void Atualizar(Cliente cliente)
    {
        // Como é fake, normalmente você atualiza o objeto direto (referência),
        // mas aqui vai uma versão segura procurando pelo login:
        if (string.IsNullOrWhiteSpace(cliente.Login))
            return;

        var atual = BuscarPorLogin(cliente.Login);
        if (atual == null)
            return;

        atual.Nome = cliente.Nome;
        atual.Email = cliente.Email;
        atual.Fone = cliente.Fone;
        atual.Logado = cliente.Logado;
        // senha é private no seu model, então não dá pra atualizar daqui (ok por enquanto)
    }

    public void RemoverPorLogin(string login)
    {
        var c = BuscarPorLogin(login);
        if (c != null)
            clientes.Remove(c);
    }
}
