using System;
using System.Collections.Generic;

class Program
{
    private const int limit = 15;

    static void Main(string[] args)
    {
        char[,] labirinto = new char[limit, limit];
        criaLabirinto(labirinto);
        mostrarLabirinto(labirinto, limit, limit);
        buscarQueijo(labirinto, 1, 1);
        Console.ReadKey();
    }

    static void mostrarLabirinto(char[,] array, int l, int c)
    {
        for (int i = 0; i < l; i++)
        {
            Console.WriteLine();
            for (int j = 0; j < c; j++)
            {
                Console.Write($" {array[i, j]} ");
            }
        }
        Console.WriteLine();
    }

    static void criaLabirinto(char[,] meuLab)
    {
        Random random = new Random();
        for (int i = 0; i < limit; i++)
        {
            for (int j = 0; j < limit; j++)
            {
                meuLab[i, j] = random.Next(4) == 1 ? '|' : '.';
            }
        }

        for (int i = 0; i < limit; i++)
        {
            meuLab[0, i] = '*';
            meuLab[limit - 1, i] = '*';
            meuLab[i, 0] = '*';
            meuLab[i, limit - 1] = '*';
        }

        int x = random.Next(limit);
        int y = random.Next(limit);
        meuLab[x, y] = 'Q';
    }

    static void buscarQueijo(char[,] meuLab, int i, int j)
    {
        Stack<(int, int)> minhaPilha = new Stack<(int, int)>();
        minhaPilha.Push((i, j));

        while (minhaPilha.Count > 0)
        {
            var (x, y) = minhaPilha.Pop();

            if (x < 0 || x >= limit || y < 0 || y >= limit || meuLab[x, y] == '|' || meuLab[x, y] == '*' || meuLab[x, y] == 'v')
                continue;

            if (meuLab[x, y] == 'Q')
            {
                Console.Clear();
                mostrarLabirinto(meuLab, limit, limit);
                Console.WriteLine("Encontrou o queijo!");
                return;
            }

            meuLab[x, y] = 'v';

            minhaPilha.Push((x, y + 1)); // direita
            minhaPilha.Push((x + 1, y)); // baixo
            minhaPilha.Push((x, y - 1)); // esquerda
            minhaPilha.Push((x - 1, y)); // cima

            System.Threading.Thread.Sleep(200);
            Console.Clear();
            mostrarLabirinto(meuLab, limit, limit);
        }

        Console.WriteLine("É impossível achar o queijo.");
    }
}
