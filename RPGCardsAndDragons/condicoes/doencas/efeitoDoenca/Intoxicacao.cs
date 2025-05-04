using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons;
using CardsAndDragons.ClassesCondicoes;
using CardsAndDragons.Condicoes;
using CardsAndDragons.Controllers;
using CardsAndDragonsJogo;
using RPGCardsAndDragons.doencas;

namespace RPGCardsAndDragons.condicoes.doencas.efeitoDoenca
{
    public class Intoxicacao : IEfeitoDoenca
    {
        public string Nome { get; set; } = "Intoxicação";

        public string Descricao { get; set; } = "O hospédeiro sofre de efeitos de envenenamento.";

        public Intoxicacao(TipoDoenca tipo) { }

        public void Aplicar(Batalha batalha, ICriaturaCombatente alvo, int nivel)
        {
            batalha.Aplicadores.Add(new AplicadorDeCondicao(new Veneno(nivel, 3), alvo));

            TextoController.CentralizarTexto($"{alvo.Nome} sofre de intoxicação");
        }
    }
}
