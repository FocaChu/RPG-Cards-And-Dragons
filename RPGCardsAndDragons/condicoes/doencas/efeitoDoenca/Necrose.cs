using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons;
using CardsAndDragonsJogo;
using RPGCardsAndDragons.doencas;

namespace RPGCardsAndDragons.condicoes.doencas.efeitoDoenca
{
    public class Necrose : IEfeitoDoenca
    {
        public string Nome { get; set; } = "Necrose";

        public string Descricao { get; set; } = "A criatura sofre dano igual a parte de sua vida máxima a cada turno.";

        public Necrose(TipoDoenca tipo){ }

        public Necrose(Necrose original)
        {
            Nome = original.Nome;
            Descricao = original.Descricao;
        }

        public void Aplicar(Batalha batalha, OInimigo alvo, int nivel)
        {
            int porcentagem = nivel * 10;
            double dano = alvo.VidaAtual * (porcentagem / 100.0);
            alvo.SofrerDano((int)dano, true);
        }
    }
}
