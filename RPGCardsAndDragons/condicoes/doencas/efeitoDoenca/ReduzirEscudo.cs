using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.Condicoes;
using CardsAndDragons.Controllers;
using CardsAndDragons;
using RPGCardsAndDragons.doencas;

namespace RPGCardsAndDragons.condicoes.doencas.efeitoDoenca
{
    public class ReduzirEscudo : EfeitoDoencaBase
    {
        public ReduzirEscudo(TipoDoenca tipo) : base(tipo, 10) { }

        public override void Aplicar(ICriaturaCombatente alvo, int nivel)
        {
            CondicaoController.AplicarOuAtualizarCondicao(new ModificacaoEscudo(nivel * -1, 1), alvo.Condicoes);
            TextoController.CentralizarTexto($"{alvo.Nome} sofre de sintomas de fadiga");
        }
    }
}
