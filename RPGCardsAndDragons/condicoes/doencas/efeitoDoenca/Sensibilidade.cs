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

namespace RPGCardsAndDragons.condicoes.doencas.efeitoDoenca
{
    public class Sensibilidade : IEfeitoDoenca
    {
        public string Nome { get; set; } = "Sensibilidade";

        public string Descricao { get; set; } = "O hóspedeiro fica mais frágil a ataques.";

        public Sensibilidade(TipoDoenca tipo) { }

        public void Aplicar(Batalha batalha, ICriaturaCombatente alvo, int nivel)
        {
            batalha.Aplicadores.Add(new AplicadorDeCondicao(new ModificacaoDefesa(nivel * -1, 3), alvo));

            TextoController.CentralizarTexto($"{alvo.Nome} sofre de sintomas de sensibilidade");
        }
    }
}
