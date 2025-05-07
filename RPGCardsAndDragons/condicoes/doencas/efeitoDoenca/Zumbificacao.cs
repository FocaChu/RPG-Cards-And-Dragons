using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.Condicoes;
using CardsAndDragons.Controllers;
using CardsAndDragons;
using CardsAndDragonsJogo;
using RPGCardsAndDragons.doencas;
using RPGCardsAndDragons.Aplicadores;

namespace RPGCardsAndDragons.condicoes.doencas.efeitoDoenca
{
    public class Zumbificacao : IEfeitoDoenca
    {
        public string Nome { get; set; } = "Zumbificação";

        public string Descricao { get; set; } = "O hospedeiro revive como um aliado após morrer.";

        public bool Zumbificado { get; set; } = false;

        public Zumbificacao(TipoDoenca tipo) { }

        public Zumbificacao(Fraqueza original)
        {
            Nome = original.Nome;
            Descricao = original.Descricao;
        }

        public void Aplicar(Batalha batalha, OInimigo alvo, int nivel)
        {
            if (Zumbificado == false)
            {
                batalha.Aplicadores.Add(new AplicadorRessuireicao(alvo.ID));


                TextoController.CentralizarTexto($"{alvo.Nome} parece estranho...");
            }

            Zumbificado = true;
        }
    }
}
