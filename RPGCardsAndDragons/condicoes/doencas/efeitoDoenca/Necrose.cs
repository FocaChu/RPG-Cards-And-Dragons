using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons;
using RPGCardsAndDragons.doencas;

namespace RPGCardsAndDragons.condicoes.doencas.efeitoDoenca
{
    public class Necrose : IEfeitoDoenca
    {
        public string Nome { get; set; } = "Necrose";

        public string Descricao { get; set; } = "A criatura sofre dano igual a parte de sua vida máxima a cada turno.";

        public Necrose(TipoDoenca tipo){ }

        public void Aplicar(ICriaturaCombatente alvo, int nivel)
        {
            int porcentagem = nivel * 10;
            double dano = alvo.VidaAtual * (porcentagem / 100.0);
            alvo.SofrerDano((int)dano, true);
        }
    }
}
