using CultBook09.model.entities.clientes;

namespace CultBook09.model.interfaces;

public interface IClienteRepositorio
{
    List<Cliente> BuscarTodos();
    Cliente? BuscarPorLogin(string login);
    void Adicionar(Cliente cliente);
    void Atualizar(Cliente cliente);
    void RemoverPorLogin(string login);

    bool LoginExiste(string login);
}
