using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons;
using CardsAndDragons.ClassesCondicoes;
using CardsAndDragons.Controllers;
using CardsAndDragonsJogo;
using RPGCardsAndDragons.Aplicadores;

namespace RPGCardsAndDragons.condicoes
{
    public class AplicadorCondicao : IAplicador
    {

        public ICondicaoTemporaria Condicao { get; set; }

        public ICriaturaCombatente Criatura { get; set; }

        public AplicadorCondicao(ICondicaoTemporaria condicao, ICriaturaCombatente criatura)
        {
            this.Condicao = condicao;
            this.Criatura = criatura;
        }

        public bool Aplicar(Batalha batalha)
        {
            if (Criatura.VidaAtual > 0)
            {
                CondicaoController.AplicarOuAtualizarCondicao(Condicao, Criatura.Condicoes);
            }
            return true;
        }

    }
}
