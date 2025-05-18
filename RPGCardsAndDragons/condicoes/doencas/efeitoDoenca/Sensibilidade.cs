using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.Condicoes;
using CardsAndDragons.Controllers;
using CardsAndDragons;
using RPGCardsAndDragons.doencas;
using CardsAndDragons.ClassesCondicoes;
using CardsAndDragonsJogo;
using System.Diagnostics.Eventing.Reader;

namespace RPGCardsAndDragons.condicoes.doencas.efeitoDoenca
{
    public class Sensibilidade : IEfeitoDoenca
    {
        public string Nome { get; set; } = "Sensibilidade";

        public string Descricao { get; set; } = "O hóspedeiro fica mais frágil a ataques.";

        public Sensibilidade() { }

        public Sensibilidade(Sensibilidade original)
        {
            Nome = original.Nome;
            Descricao = original.Descricao;
        }

        public void Aplicar(Batalha batalha, OInimigo alvo, int nivel)
        {
            batalha.Aplicadores.Add(new AplicadorCondicao(new ModificacaoDefesa(nivel * -1, 1), alvo));

            TextoController.CentralizarTexto($"{alvo.Nome} sofre de sintomas de sensibilidade");
        }
    }
}
