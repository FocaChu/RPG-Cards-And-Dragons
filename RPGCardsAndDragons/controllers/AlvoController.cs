using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragonsJogo;

namespace CardsAndDragons.Controllers
{
    public static class AlvoController
    {
        private static Random rng = new Random();

        public static int SelecionarAlvo(List<OInimigo> inimigos)
        {
            int option = 0;
            bool selecionado = false;

            while (!selecionado)
            {
                Console.Clear();
                BatalhaController.MostrarInimigos(inimigos, option);

                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (option > 0) option--;
                        else option = inimigos.Count - 1; // <- Corrigido: agora circula só nos inimigos
                        break;
                    case ConsoleKey.RightArrow:
                        if (option < inimigos.Count - 1) option++;
                        else option = 0; // <- Corrigido também
                        break;
                    case ConsoleKey.Enter:
                        return option;
                }
            }
            return -1;
        }

        public static int SelecionarAlvo(List<ICriaturaCombatente> aliados)
        {
            int option = 0;
            bool selecionado = false;

            while (!selecionado)
            {
                Console.Clear();

                Console.ResetColor();
                Console.WriteLine("\n\n\n\n");
                TextoController.CentralizarTexto(@"=========================================   ~ ALIADOS ~   =======================================" + "\n\n");

                BatalhaController.MostrarCriaturas(aliados, option, ConsoleColor.Green, ConsoleColor.Yellow);

                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (option > 0) option--;
                        else option = aliados.Count - 1;
                        break;
                    case ConsoleKey.RightArrow:
                        if (option < aliados.Count - 1) option++;
                        else option = 0;
                        break;
                    case ConsoleKey.Enter:
                        return option;
                }
            }
            return -1;
        }

        public static ICriaturaCombatente EscolherAlvoAleatorioDosAliados(Batalha batalha)
        {
            var alvos = new List<ICriaturaCombatente> { batalha.Jogador };
            alvos.AddRange(batalha.Aliados);

            int indice = rng.Next(alvos.Count);
            return alvos[indice];
        }

        public static ICriaturaCombatente EscolherAlvoAliadoAleatorio(List<ICriaturaCombatente> aliados)
        {
            int indice = rng.Next(aliados.Count);
            return aliados[indice];
        }

        public static OInimigo EscolherInimigoAleatorio(List<OInimigo> inimigos)
        {
            int indiceAleatorio = rng.Next(inimigos.Count);

            var alvo = inimigos[indiceAleatorio];

            return alvo;
        }

    }
}
