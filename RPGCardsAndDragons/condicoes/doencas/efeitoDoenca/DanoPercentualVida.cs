using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons;
using RPGCardsAndDragons.doencas;

namespace RPGCardsAndDragons.condicoes.doencas.efeitoDoenca
{
    public class DanoPercentualVida : EfeitoDoencaBase
    {
        public DanoPercentualVida(TipoDoenca tipo) : base(tipo, 10) { }

        public override void Aplicar(ICriaturaCombatente alvo, int nivel)
        {
            int porcentagem = nivel * 10;
            double dano = alvo.VidaAtual * (porcentagem / 100.0);
            alvo.SofrerDano((int)dano, true);
        }
    }
}
