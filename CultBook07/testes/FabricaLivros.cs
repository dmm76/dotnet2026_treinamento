using CultBook05.model;

namespace CultBook05.testes;

public class FabricaLivros
{
    private static List<Livro> livros = new List<Livro>()
    {
        new LivroFisico(
            "978-85-01-00001-1",
            "O Senhor dos Algoritmos",
            "Uma jornada épica pelo mundo da lógica e da programação.",
            "J. R. R. Code",
            12,
            89.90,
            "algoritmos.jpg",
            2025,
            "Tecnologia",
            peso: 0.45,
            valorFrete: 12.00
        ),
        new LivroFisico(
            "978-85-01-00002-2",
            "Clean Code na Prática",
            "Boas práticas para escrever código limpo e sustentável.",
            "Robert C. Martin",
            8,
            99.90,
            "cleancode.jpg",
            2025,
            "Tecnologia",
            peso: 0.55,
            valorFrete: 14.00
        ),
        new LivroFisico(
            "978-85-01-00003-3",
            "Estruturas de Dados Descomplicadas",
            "Entenda listas, pilhas e filas de forma simples.",
            "Douglas Monquero",
            15,
            79.90,
            "estruturas.jpg",
            2025,
            "Tecnologia",
            peso: 0.40,
            valorFrete: 11.00
        ),
        new Ebook(
            "978-85-01-00004-4",
            "O Código da Vinci Digital",
            "Mistério e tecnologia se encontram nesta obra moderna.",
            "Dan Brown Jr.",
            999,
            59.90,
            "vinci.jpg",
            2025,
            "Ficção",
            tamanhoMB: 6.80
        ),
        new LivroFisico(
            "978-85-01-00005-5",
            "Java para Humanos",
            "Aprenda Java com exemplos claros e objetivos.",
            "James Gosling",
            20,
            69.90,
            "java.jpg",
            2025,
            "Tecnologia",
            peso: 0.50,
            valorFrete: 12.50
        ),
        new LivroFisico(
            "978-85-01-00006-6",
            "C# Essencial",
            "Fundamentos da linguagem C# para iniciantes.",
            "Anders Hejlsberg",
            10,
            74.90,
            "csharp.jpg",
            2025,
            "Tecnologia",
            peso: 0.48,
            valorFrete: 12.00
        ),
        new Ebook(
            "978-85-01-00007-7",
            "O Fantasma do Bug",
            "Uma história sobre erros que assombram desenvolvedores.",
            "Ada Lovelace",
            999,
            49.90,
            "bug.jpg",
            2025,
            "Ficção",
            tamanhoMB: 4.20
        ),
        new LivroFisico(
            "978-85-01-00008-8",
            "Arquitetura Limpa",
            "Como projetar sistemas robustos e escaláveis.",
            "Robert C. Martin",
            9,
            109.90,
            "arquitetura.jpg",
            2025,
            "Tecnologia",
            peso: 0.65,
            valorFrete: 16.00
        ),
        new Ebook(
            "978-85-01-00009-9",
            "Lógica de Programação",
            "O primeiro passo para quem quer aprender a programar.",
            "Paulo Silveira",
            999,
            54.90,
            "logica.jpg",
            2025,
            "Educação",
            tamanhoMB: 5.10
        ),
        new LivroFisico(
            "978-85-01-00010-0",
            "O Último Commit",
            "Suspense no mundo do desenvolvimento de software.",
            "Linus Torvalds",
            4,
            64.90,
            "commit.jpg",
            2025,
            "Ficção",
            peso: 0.42,
            valorFrete: 11.50
        ),
        new AudioLivro(
            "978-85-01-99999-1",
            "Clean Code (Áudio)",
            "Versão narrada do clássico.",
            "Robert C. Martin",
            5,
            119.90,
            "cleancode-audio.jpg",
            2025,
            "Tecnologia",
            480,
            "Narrador X"
        ),
    };

    public static List<Livro> GetLivros()
    {
        return livros;
    }
}
