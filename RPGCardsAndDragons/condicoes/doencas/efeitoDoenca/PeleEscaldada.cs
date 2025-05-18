using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.ClassesCondicoes;
using CardsAndDragons.Controllers;
using CardsAndDragons;
using CardsAndDragonsJogo;
using RPGCardsAndDragons.doencas;

namespace RPGCardsAndDragons.condicoes.doencas.efeitoDoenca
{
    public class PeleEscaldada : IEfeitoDoenca
    {
        public string Nome { get; set; } = "Pele Escaldada";

        public string Descricao { get; set; } = "O hospédeiro sofre de efeitos de queimadura.";

        public PeleEscaldada() { }

        public PeleEscaldada(PeleEscaldada original)
        {
            Nome = original.Nome;
            Descricao = original.Descricao;
        }

        public void Aplicar(Batalha batalha, OInimigo alvo, int nivel)
        {

            int valor = (int)nivel / 2;
            batalha.Aplicadores.Add(new AplicadorCondicao(new Queimadura(valor, 2), alvo));

            TextoController.CentralizarTexto($"{alvo.Nome} sofre de síndrome de pele escaldada");
        }
    }

}
