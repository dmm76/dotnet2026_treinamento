using CultBook11.model.entities.clientes;

namespace CultBook11.model.interfaces;

public interface IClienteRepositorio
{
    List<Cliente> BuscarTodos();
    Cliente? BuscarPorLogin(string login);
    void Adicionar(Cliente cliente);
    void Atualizar(Cliente cliente);
    void RemoverPorLogin(string login);

    bool LoginExiste(string login);
}
