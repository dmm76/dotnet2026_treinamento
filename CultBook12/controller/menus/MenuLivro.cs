namespace CultBook12.controller.menus;

using System;
using CultBook12.model.entities.clientes;
using CultBook12.model.entities.livros;
using CultBook12.model.entities.pedidos;
using CultBook12.model.interfaces;
using CultBook12.model.usecases.livros;

public static class MenuLivro
{
    public static void OpcaoBuscarLivros(
        ListarLivrosUseCase listarLivrosUc,
        GerarRelatorioLivrosUseCase relatorioUc
    )
    {
        var livros = listarLivrosUc.Executar();
        var relatorio = relatorioUc.Executar(livros);

        Console.WriteLine(relatorio);
        Console.ReadKey();
    }

    public static Pedido? OpcaoInserirLivro(
        Cliente? clienteLogado,
        Pedido? pedidoAtual,
        InserirLivroCarrinhoUseCase inserirLivroCarrinhoUc
    )
    {
        try
        {
            if (clienteLogado == null || !clienteLogado.Logado)
            {
                Console.WriteLine("Você precisa estar logado para inserir livros no carrinho.");
                Console.ReadKey();
                return pedidoAtual;
            }

            Console.Write("ISBN: ");
            var isbn = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(isbn))
            {
                Console.WriteLine("ISBN inválido.");
                Console.ReadKey();
                return pedidoAtual;
            }

            Console.Write("Quantidade: ");
            var qtdStr = Console.ReadLine();

            if (!int.TryParse(qtdStr, out int qtd) || qtd <= 0)
            {
                Console.WriteLine("Quantidade inválida.");
                Console.ReadKey();
                return pedidoAtual;
            }

            pedidoAtual = inserirLivroCarrinhoUc.Executar(clienteLogado, pedidoAtual, isbn, qtd);

            Console.WriteLine("Livro inserido com sucesso!");
            Console.WriteLine($"Total: {pedidoAtual.GetValorTotal():C}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.ReadKey();
        return pedidoAtual;
    }

    public static void OpcaoRemoverLivro(
        Cliente? clienteLogado,
        Pedido? pedidoAtual,
        RemoverLivroCarrinhoUseCase removerUc
    )
    {
        try
        {
            if (clienteLogado == null || !clienteLogado.Logado)
                throw new Exception("Você precisa estar logado para remover itens do carrinho.");

            if (pedidoAtual == null || pedidoAtual.GetQtdItens() == 0)
                throw new Exception("Carrinho vazio.");

            Console.Clear();
            Console.WriteLine("=== REMOVER LIVRO DO CARRINHO ===");
            pedidoAtual.Mostrar();

            Console.Write("\nDigite o ISBN do livro para remover (1 unidade): ");
            string? isbn = Console.ReadLine();

            removerUc.Executar(pedidoAtual, isbn);

            Console.WriteLine("Livro removido do carrinho!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Console.ReadKey();
        }
    }

    // ============================
    // CATÁLOGO (CRUD)
    // ============================

    public static void OpcaoCadastrarLivroCatalogo(CadastrarLivroUseCase cadastrarUc)
    {
        try
        {
            Console.Clear();
            Console.WriteLine("=== CADASTRAR LIVRO (CATÁLOGO) ===");
            Console.WriteLine("Tipo: 1) Físico  2) Ebook  3) ÁudioLivro");
            Console.Write("Escolha: ");
            var tipoStr = Console.ReadLine();

            if (!int.TryParse(tipoStr, out int tipo) || tipo < 1 || tipo > 3)
                throw new Exception("Tipo inválido.");

            Console.Write("ISBN: ");
            var isbn = (Console.ReadLine() ?? "").Trim();

            Console.Write("Título: ");
            var titulo = (Console.ReadLine() ?? "").Trim();

            Console.Write("Descrição: ");
            var descricao = (Console.ReadLine() ?? "").Trim();

            Console.Write("Autor: ");
            var autor = (Console.ReadLine() ?? "").Trim();

            Console.Write("Estoque: ");
            if (!int.TryParse(Console.ReadLine(), out int estoque))
                throw new Exception("Estoque inválido.");

            Console.Write("Preço: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal preco))
                throw new Exception("Preço inválido.");

            Console.Write("Figura: ");
            var figura = (Console.ReadLine() ?? "").Trim();

            Console.Write("Data cadastro (int): ");
            if (!int.TryParse(Console.ReadLine(), out int dataCadastro))
                throw new Exception("DataCadastro inválida.");

            Console.Write("Categoria: ");
            var categoria = (Console.ReadLine() ?? "").Trim();

            Livro livro = tipo switch
            {
                1 => CriarFisico(
                    isbn,
                    titulo,
                    descricao,
                    autor,
                    estoque,
                    preco,
                    figura,
                    dataCadastro,
                    categoria
                ),
                2 => CriarEbook(
                    isbn,
                    titulo,
                    descricao,
                    autor,
                    estoque,
                    preco,
                    figura,
                    dataCadastro,
                    categoria
                ),
                3 => CriarAudio(
                    isbn,
                    titulo,
                    descricao,
                    autor,
                    estoque,
                    preco,
                    figura,
                    dataCadastro,
                    categoria
                ),
                _ => throw new Exception("Tipo inválido."),
            };

            cadastrarUc.Executar(livro);

            Console.WriteLine("✅ Livro cadastrado no catálogo com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ {ex.Message}");
        }
        finally
        {
            Console.ReadKey();
        }
    }

    public static void OpcaoAtualizarLivroCatalogo(
        ILivroRepositorio repo,
        AtualizarLivroUseCase atualizarUc
    )
    {
        try
        {
            Console.Clear();
            Console.WriteLine("=== ATUALIZAR LIVRO (CATÁLOGO) ===");

            Console.Write("ISBN do livro: ");
            var isbn = (Console.ReadLine() ?? "").Trim();

            if (string.IsNullOrWhiteSpace(isbn))
                throw new Exception("ISBN inválido.");

            var existente = repo.BuscarPorIsbn(isbn);
            if (existente == null)
                throw new Exception("Livro não encontrado.");

            Console.WriteLine($"\nTipo: {existente.GetType().Name}");
            Console.WriteLine("Deixe em branco para manter o valor atual.\n");

            // comuns
            string titulo = PerguntarOuManter("Título", existente.Titulo);
            string descricao = PerguntarOuManter("Descrição", existente.Descricao);
            string autor = PerguntarOuManter("Autor", existente.Autor);
            int estoque = PerguntarIntOuManter("Estoque", existente.Estoque);
            decimal preco = PerguntarDecimalOuManter("Preço", existente.Preco);
            string figura = PerguntarOuManter("Figura", existente.Figura);
            int dataCadastro = PerguntarIntOuManter("DataCadastro", existente.DataCadastro);
            string categoria = PerguntarOuManter("Categoria", existente.Categoria);

            Livro atualizado = existente switch
            {
                LivroFisico f => new LivroFisico(
                    isbn,
                    titulo,
                    descricao,
                    autor,
                    estoque,
                    preco,
                    figura,
                    dataCadastro,
                    categoria,
                    peso: PerguntarDoubleOuManter("Peso (kg)", f.Peso),
                    valorFrete: PerguntarDecimalOuManter("Valor frete", f.ValorFrete)
                ),

                Ebook e => new Ebook(
                    isbn,
                    titulo,
                    descricao,
                    autor,
                    estoque,
                    preco,
                    figura,
                    dataCadastro,
                    categoria,
                    tamanhoMB: PerguntarDoubleOuManter("Tamanho (MB)", e.TamanhoMB)
                ),

                AudioLivro a => new AudioLivro(
                    isbn,
                    titulo,
                    descricao,
                    autor,
                    estoque,
                    preco,
                    figura,
                    dataCadastro,
                    categoria,
                    tempoDuracao: PerguntarIntOuManter("Tempo (min)", a.TempoDuracao),
                    narrador: PerguntarOuManter("Narrador", a.Narrador)
                ),

                _ => throw new Exception("Tipo de livro não suportado para atualização."),
            };

            atualizarUc.Executar(atualizado);

            Console.WriteLine("\n✅ Livro atualizado com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n❌ {ex.Message}");
        }
        finally
        {
            Console.ReadKey();
        }
    }

    public static void OpcaoRemoverLivroCatalogo(RemoverLivroUseCase removerUc)
    {
        try
        {
            Console.Clear();
            Console.WriteLine("=== REMOVER LIVRO (CATÁLOGO) ===");

            Console.Write("ISBN: ");
            var isbn = (Console.ReadLine() ?? "").Trim();

            removerUc.Executar(isbn);

            Console.WriteLine("\n✅ Livro removido do catálogo!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n❌ {ex.Message}");
        }
        finally
        {
            Console.ReadKey();
        }
    }

    // ============================
    // HELPERS
    // ============================

    private static Livro CriarFisico(
        string isbn,
        string titulo,
        string descricao,
        string autor,
        int estoque,
        decimal preco,
        string figura,
        int dataCadastro,
        string categoria
    )
    {
        Console.Write("Peso (kg): ");
        if (!double.TryParse(Console.ReadLine(), out double peso))
            throw new Exception("Peso inválido.");

        Console.Write("Valor frete: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal frete))
            throw new Exception("Frete inválido.");

        return new LivroFisico(
            isbn,
            titulo,
            descricao,
            autor,
            estoque,
            preco,
            figura,
            dataCadastro,
            categoria,
            peso,
            frete
        );
    }

    private static Livro CriarEbook(
        string isbn,
        string titulo,
        string descricao,
        string autor,
        int estoque,
        decimal preco,
        string figura,
        int dataCadastro,
        string categoria
    )
    {
        Console.Write("Tamanho (MB): ");
        if (!double.TryParse(Console.ReadLine(), out double tamanho))
            throw new Exception("TamanhoMB inválido.");

        return new Ebook(
            isbn,
            titulo,
            descricao,
            autor,
            estoque,
            preco,
            figura,
            dataCadastro,
            categoria,
            tamanho
        );
    }

    private static Livro CriarAudio(
        string isbn,
        string titulo,
        string descricao,
        string autor,
        int estoque,
        decimal preco,
        string figura,
        int dataCadastro,
        string categoria
    )
    {
        Console.Write("Tempo de duração (min): ");
        if (!int.TryParse(Console.ReadLine(), out int tempo))
            throw new Exception("Tempo de duração inválido.");

        Console.Write("Narrador: ");
        var narrador = (Console.ReadLine() ?? "").Trim();

        return new AudioLivro(
            isbn,
            titulo,
            descricao,
            autor,
            estoque,
            preco,
            figura,
            dataCadastro,
            categoria,
            tempo,
            narrador
        );
    }

    private static string PerguntarOuManter(string label, string atual)
    {
        Console.Write($"{label} ({atual}): ");
        var s = Console.ReadLine();
        return string.IsNullOrWhiteSpace(s) ? atual : s.Trim();
    }

    private static int PerguntarIntOuManter(string label, int atual)
    {
        Console.Write($"{label} ({atual}): ");
        var s = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(s))
            return atual;
        if (!int.TryParse(s, out int v))
            throw new Exception($"{label} inválido.");
        return v;
    }

    private static decimal PerguntarDecimalOuManter(string label, decimal atual)
    {
        Console.Write($"{label} ({atual}): ");
        var s = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(s))
            return atual;
        if (!decimal.TryParse(s, out decimal v))
            throw new Exception($"{label} inválido.");
        return v;
    }

    private static double PerguntarDoubleOuManter(string label, double atual)
    {
        Console.Write($"{label} ({atual}): ");
        var s = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(s))
            return atual;
        if (!double.TryParse(s, out double v))
            throw new Exception($"{label} inválido.");
        return v;
    }
}
