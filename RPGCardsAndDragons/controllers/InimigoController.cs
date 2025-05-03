using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.ClassesCondicoes;
using CardsAndDragonsJogo;

namespace CardsAndDragons.Controllers
{
    public static class InimigoController
    {

        public static void ExibirInimigo(OInimigo inimigo)
        {
            Console.Clear();

            Console.WriteLine("\n\n\n");
            TextoController.CentralizarTexto($"================================  < Analisando {inimigo.Nome} > ================================\n\n");

            TextoController.CentralizarTexto($"Vida: {inimigo.VidaAtual}/{inimigo.VidaMax}\n");

            CondicaoController.ExibirCondicoes(inimigo);

            for (int linha = 0; linha < inimigo.Modelo.Count; linha++)
            {
                TextoController.CentralizarTexto(inimigo.Modelo[linha]);
            }
        }
    }
}
