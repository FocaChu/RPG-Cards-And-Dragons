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
    public class Sensibilidade : IEfeitoDoenca
    {
        public string Nome { get; set; } = "Sensibilidade";

        public string Descricao { get; set; } = "O hóspedeiro fica mais frágil a ataques.";

        public Sensibilidade(TipoDoenca tipo) { }

        public void Aplicar(ICriaturaCombatente alvo, int nivel)
        {
            CondicaoController.AplicarOuAtualizarCondicao(new ModificacaoDefesa(nivel * -1, 1), alvo.Condicoes);
            TextoController.CentralizarTexto($"{alvo.Nome} sofre de sintomas de sensibilidade");
        }
    }
}
