using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.Cartas;
using CardsAndDragons.Controllers;
using CardsAndDragonsJogo;
using RPGCardsAndDragons.condicoes.doencas.efeitoDoenca;
using RPGCardsAndDragons.doencas;

namespace RPGCardsAndDragons.Aplicadores
{
    public class AplicadorRessuireicao : IAplicador
    {
        public int IDMorto { get; set; }

        public AplicadorRessuireicao(int idMorto)
        {
            this.IDMorto = idMorto;
        }

        public bool Aplicar(Batalha batalha)
        {
            var mortos = batalha.InimigosDerrotados;

            foreach (var morto in mortos)
            {
                if (morto.ID == IDMorto)
                {
                    Console.Clear();

                    Console.WriteLine("\n\n\n");
                    AliadoController.ReviverInimigo(batalha, morto);

                    Console.ReadKey();

                    return true;
                }
            }
            return false;
        }

    }
}
