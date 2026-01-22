using System;
using System.Collections.Generic;

namespace Aula01
{
    class Aluno
    {
        public string Nome { get; set; } = "";
        public int Idade { get; set; }
        public bool Ativo { get; set; }
    }

    class Program
    {
        static double MediaIdadeAtivos(List<Aluno> alunos)
        {
            int soma = 0;
            int quantidade = 0;

            foreach (var aluno in alunos)
            {
                if (aluno.Ativo)
                {
                    soma += aluno.Idade;
                    quantidade++;
                }
            }

            if (quantidade == 0) return 0;
            return (double)soma / quantidade;
        }

        static void Main()
        {
            var alunos = new List<Aluno>
            {
                new Aluno { Nome = "Douglas", Idade = 20, Ativo = true },
                new Aluno { Nome = "Mauricio", Idade = 22, Ativo = false },
                new Aluno { Nome = "Claudio", Idade = 29, Ativo = true },
                new Aluno { Nome = "Ana", Idade = 18, Ativo = true },
            };

            double media = MediaIdadeAtivos(alunos);

            Console.WriteLine(media.ToString("F2"));
        }
    }
}
