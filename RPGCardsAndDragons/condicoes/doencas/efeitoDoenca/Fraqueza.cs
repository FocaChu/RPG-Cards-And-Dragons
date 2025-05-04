using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.Condicoes;
using CardsAndDragons.Controllers;
using CardsAndDragons;
using RPGCardsAndDragons.doencas;
using CardsAndDragonsJogo;

namespace RPGCardsAndDragons.condicoes.doencas.efeitoDoenca
{
    public class Fraqueza : IEfeitoDoenca
    {
        public string Nome { get; set; } = "Fraqueza";

        public string Descricao { get; set; } = "O hospedeiro causa menos dano em seus golpes.";

        public Fraqueza(TipoDoenca tipo) { }

        public  void Aplicar(Batalha batalha, ICriaturaCombatente alvo, int nivel)
        {
            batalha.Aplicadores.Add(new AplicadorDeCondicao(new ModificacaoDano(nivel * -1, 3), alvo));

            TextoController.CentralizarTexto($"{alvo.Nome} sofre de sintomas de fraqueza");
        }
    }
}
