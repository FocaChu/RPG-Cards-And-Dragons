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
        public static int MostrarOpcoes(List<string> opcaoUm, List<string> opcaoDois, string descricaoUm, string descricaoDois)
        {
            int option = 0;
            bool selecionado = false;

            int larguraModelo = opcaoUm[0].Length;
            int espacoEntre = 4;

            int larguraTotal = 2 * larguraModelo + (2 - 1) * espacoEntre;
            int margemEsquerda = (Console.WindowWidth - larguraTotal) / 2;

            int numeroLinhas = opcaoUm.Count;

            while (!selecionado)
            {
                Console.Clear();
                Console.WriteLine("\n\n\n\n");
                TextoController.CentralizarTexto("Escolha uma das opções:");

                // Desenhar as duas opções
                for (int linha = 0; linha < numeroLinhas; linha++)
                {
                    Console.SetCursorPosition(margemEsquerda, Console.CursorTop); // centralizar na tela

                    for (int i = 0; i < 2; i++)
                    {
                        Console.ForegroundColor = (i == option) ? ConsoleColor.Red : ConsoleColor.Gray;

                        if (i == 0)
                            Console.Write(opcaoUm[linha]);
                        else
                            Console.Write(opcaoDois[linha]);

                        if (i < 1)
                            Console.Write(new string(' ', espacoEntre)); // espaço entre as cartas
                    }
                    Console.WriteLine();
                }
                Console.ResetColor();
                Console.WriteLine("\n");
                if (option == 0) TextoController.CentralizarTexto(descricaoUm);
                if (option == 1) TextoController.CentralizarTexto(descricaoDois);

                Console.ResetColor();

                // Captura da tecla pressionada
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        option = (option == 0) ? 1 : 0;
                        break;
                    case ConsoleKey.RightArrow:
                        option = (option == 0) ? 1 : 0;
                        break;
                    case ConsoleKey.Enter:
                        return option;
                    case ConsoleKey.Escape:
                        return -1;
                }
            }

            return -1;
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
