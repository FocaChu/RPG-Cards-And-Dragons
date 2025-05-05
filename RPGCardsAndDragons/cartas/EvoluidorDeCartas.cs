using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.Cartas;
using CardsAndDragonsJogo;

namespace RPGCardsAndDragons.cartas
{
    public class EvoluidorDeCartas
    {
        public string Nomecarta { get; set; }

        public EvoluidorDeCartas(string nomecarta)
        {
            this.Nomecarta = nomecarta;
        }

        public void Evoluir(List<ICartaUsavel> cartas)
        {
            foreach (var carta in cartas)
            {
                if (carta is CartaEvolutiva cartaEvoluida && cartaEvoluida.Nome == this.Nomecarta)
                {
                    cartaEvoluida.Evoluir();
                }
            }
        }
    }
}
