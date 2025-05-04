using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons;
using CardsAndDragons.ClassesCondicoes;

namespace RPGCardsAndDragons.condicoes
{
    public class AplicadorDeCondicao
    {

        public ICondicaoTemporaria Condicao { get; set; }

        public ICriaturaCombatente Criatura { get; set; }

        public AplicadorDeCondicao(ICondicaoTemporaria condicao, ICriaturaCombatente criatura)
        {
            this.Condicao = condicao;
            this.Criatura = criatura;
        }

        public void AplicarCondicao()
        {
            if(Criatura.VidaAtual > 0)
            {
                Criatura.Condicoes.Add(Condicao);
            }
        }

    }
}
