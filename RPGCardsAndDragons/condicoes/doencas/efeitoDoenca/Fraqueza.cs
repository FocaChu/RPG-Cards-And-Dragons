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

        public Fraqueza(Fraqueza original)
        {
            Nome = original.Nome;
            Descricao = original.Descricao;
        }

        public  void Aplicar(Batalha batalha, OInimigo alvo, int nivel)
        {
            batalha.Aplicadores.Add(new AplicadorCondicao(new ModificacaoDano(nivel * -1, 1), alvo));

            TextoController.CentralizarTexto($"{alvo.Nome} sofre de sintomas de fraqueza");
        }
    }
}
