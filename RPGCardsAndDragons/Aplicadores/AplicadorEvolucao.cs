using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.Cartas;
using CardsAndDragonsJogo;
using RPGCardsAndDragons.Aplicadores;

namespace RPGCardsAndDragons.cartas
{
    public class AplicadorEvolucao : IAplicador
    {
        public string Nomecarta { get; set; }

        public AplicadorEvolucao(string nomecarta)
        {
            this.Nomecarta = nomecarta;
        }

        public bool Aplicar(Batalha batalha)
        {
            var cartas = batalha.Jogador.BaralhoCompleto;

            foreach (var carta in cartas)
            {
                if (carta is CartaEvolutiva cartaEvoluida && cartaEvoluida.Nome == this.Nomecarta)
                {
                    cartaEvoluida.Evoluir();
                }
            }

            return true;
        }
    }
}
