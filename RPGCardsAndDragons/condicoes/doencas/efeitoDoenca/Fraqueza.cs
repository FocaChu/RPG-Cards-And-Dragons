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
    public class Fraqueza : IEfeitoDoenca
    {
        public string Nome { get; set; } = "Fraqueza";

        public string Descricao { get; set; } = "O hospedeiro causa menos dano em seus golpes.";

        public Fraqueza(TipoDoenca tipo) { }

        public  void Aplicar(ICriaturaCombatente alvo, int nivel)
        {
            CondicaoController.AplicarOuAtualizarCondicao(new ModificacaoDano(nivel * -1, 1), alvo.Condicoes);
            TextoController.CentralizarTexto($"{alvo.Nome} sofre de sintomas de fraqueza");
        }
    }
}
