using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.Aliados;
using CardsAndDragons.Cartas;
using CardsAndDragonsJogo;

namespace CardsAndDragons.Controllers
{
    public static class CartaController
    {
        public static int MostrarOpcoes(List<List<string>> modelos, List<string> descricoes)
        {
            if (modelos.Count != descricoes.Count)
                throw new ArgumentException("A quantidade de modelos deve ser igual à quantidade de descrições.");

            int opcaoSelecionada = 0;
            bool selecionado = false;

            int larguraModelo = modelos[0][0].Length;
            int espacoEntre = 4;
            int numeroOpcoes = modelos.Count;
            int numeroLinhas = modelos[0].Count;

            int larguraTotal = numeroOpcoes * larguraModelo + (numeroOpcoes - 1) * espacoEntre;
            int margemEsquerda = (Console.WindowWidth - larguraTotal) / 2;

            while (!selecionado)
            {
                Console.Clear();
                Console.WriteLine("\n\n\n\n");
                TextoController.CentralizarTexto("Escolha uma das opções:\n");

                // Desenhar as opções
                for (int linha = 0; linha < numeroLinhas; linha++)
                {
                    Console.SetCursorPosition(margemEsquerda, Console.CursorTop);

                    for (int i = 0; i < numeroOpcoes; i++)
                    {
                        Console.ForegroundColor = (i == opcaoSelecionada) ? ConsoleColor.Red : ConsoleColor.Gray;
                        Console.Write(modelos[i][linha]);

                        if (i < numeroOpcoes - 1)
                            Console.Write(new string(' ', espacoEntre));
                    }
                    Console.WriteLine();
                }

                Console.ResetColor();
                Console.WriteLine();
                TextoController.CentralizarTexto(descricoes[opcaoSelecionada]);

                // Leitura da tecla
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        opcaoSelecionada = (opcaoSelecionada - 1 + numeroOpcoes) % numeroOpcoes;
                        break;
                    case ConsoleKey.RightArrow:
                        opcaoSelecionada = (opcaoSelecionada + 1) % numeroOpcoes;
                        break;
                    case ConsoleKey.Enter:
                        return opcaoSelecionada;
                }
            }

            return -1; // fallback, não deve acontecer
        }

        public static void MostrarCartas(List<ICartaUsavel> cartas, int opcao)
        {
            if (cartas.Count > 0)
            {
                int larguraModelo = cartas.First().Modelo[0].Length;
                int espacoEntre = 2;

                int larguraTotal = cartas.Count * larguraModelo + (cartas.Count - 1) * espacoEntre;
                int margemEsquerda = (Console.WindowWidth - larguraTotal) / 2;

                int qtdLinhas = cartas.First().Modelo.Count;

                for (int linha = 0; linha < qtdLinhas; linha++)
                {
                    Console.SetCursorPosition(margemEsquerda, Console.CursorTop); // centraliza a linha atual

                    for (int i = 0; i < cartas.Count; i++)
                    {
                        Console.ForegroundColor = (i == opcao) ? ConsoleColor.Red : ConsoleColor.Gray;

                        Console.Write(cartas[i].Modelo[linha]);

                        // Espaço entre cartas, exceto após a última
                        if (i < cartas.Count - 1)
                            Console.Write(new string(' ', espacoEntre));
                    }

                    Console.WriteLine();
                }

                Console.ResetColor();

                if (cartas[opcao] is CartaRecarregavel carta)
                {

                    CartaController.ExibisStatusCarta(carta);

                }
                else
                {
                    CartaController.ExibisStatusCarta(cartas[opcao]);
                }
            }
        }

        public static void ExibisStatusCarta(ICartaUsavel carta)
        {
            Console.ResetColor();

            TextoController.DefinirCorDaCarta(carta.RaridadeCarta);

            TextoController.CentralizarTexto($"Carta - {carta.Nome}");

            if(carta.TipoCarta == TipoCarta.Recarregavel)
            {
                TextoController.CentralizarTexto($"Efeito - {carta.Descricao}");
            }

            TextoController.CentralizarTexto($"Efeito - {carta.Descricao}");

            Console.ResetColor();

            Console.WriteLine();

            TextoController.CentralizarTexto("Custo:");
            if (carta.CustoVida > 0) TextoController.CentralizarTexto($" - {carta.CustoVida} de Vida");
            if (carta.CustoStamina > 0) TextoController.CentralizarTexto($" - {carta.CustoStamina} de Stamina");
            if (carta.CustoMana > 0) TextoController.CentralizarTexto($" - {carta.CustoMana} de Mana");
            if (carta.CustoOuro > 0) TextoController.CentralizarTexto($" - {carta.CustoOuro} de Ouro");
            Console.WriteLine("\n");
        }

        public static void ExibisStatusCarta(CartaRecarregavel carta)
        {
            Console.ResetColor();

            TextoController.DefinirCorDaCarta(carta.RaridadeCarta);

            TextoController.CentralizarTexto($"Carta - {carta.Nome}\n");

            TextoController.CentralizarTexto($"Cargas - {carta.CargasAtuais}/{carta.CargasMaximas}");
            TextoController.CentralizarTexto($"Efeito - {carta.Descricao}"); 

            TextoController.CentralizarTexto($"Efeito - {carta.Descricao}");

            Console.ResetColor();

            Console.WriteLine();

            TextoController.CentralizarTexto("Custo:");
            if (carta.CustoVida > 0) TextoController.CentralizarTexto($" - {carta.CustoVida} de Vida");
            if (carta.CustoStamina > 0) TextoController.CentralizarTexto($" - {carta.CustoStamina} de Stamina");
            if (carta.CustoMana > 0) TextoController.CentralizarTexto($" - {carta.CustoMana} de Mana");
            if (carta.CustoOuro > 0) TextoController.CentralizarTexto($" - {carta.CustoOuro} de Ouro");
            Console.WriteLine("\n");
        }

    }
}
