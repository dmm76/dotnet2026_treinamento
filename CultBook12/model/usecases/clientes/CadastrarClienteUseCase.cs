using CultBook12.model.entities.clientes;
using CultBook12.model.interfaces;

namespace CultBook12.model.usecases.clientes;

public class CadastrarClienteUseCase
{
    private readonly IClienteRepositorio _repo;

    public CadastrarClienteUseCase(IClienteRepositorio repo)
    {
        _repo = repo;
    }

    public string? Executar(string nome, string login, string? senha, string? email, string? fone)
    {
        // 1) Validação básica
        if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(login))
            throw new Exception("Nome e Login são obrigatórios.");

        // 2) Normalização
        nome = nome.Trim();
        login = login.Trim();

        // 3) Regra de negócio: login único
        if (_repo.LoginExiste(login))
            throw new Exception("Esse login já existe. Escolha outro.");

        // 4) Senha opcional
        string? senhaGerada = null;

        if (string.IsNullOrWhiteSpace(senha))
        {
            senhaGerada = GerarSenhaAleatoria(8);
            senha = senhaGerada;
        }
        else
        {
            senha = senha.Trim();
        }

        // 5) Criação da entidade
        var cliente = new Cliente(nome, login, senha, (email ?? "").Trim(), (fone ?? "").Trim());

        // 6) Persistência
        _repo.Adicionar(cliente);

        // 7) Retorna a senha gerada (se houver)
        return senhaGerada;
    }

    private static string GerarSenhaAleatoria(int tamanho)
    {
        const string caracteres =
            "abcdefghijklmnopqrstuvwxyz" + "ABCDEFGHIJKLMNOPQRSTUVWXYZ" + "0123456789" + "!@#$%&*";

        Random random = new Random();
        char[] senha = new char[tamanho];

        for (int i = 0; i < tamanho; i++)
        {
            int indice = random.Next(caracteres.Length);
            senha[i] = caracteres[indice];
        }

        return new string(senha);
    }
}
