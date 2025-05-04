using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons;
using CardsAndDragons.ClassesCondicoes;
using CardsAndDragons.Controllers;
using RPGCardsAndDragons.doencas;

namespace RPGCardsAndDragons.condicoes.doencas.efeitoDoenca
{
    public class Intoxicacao : IEfeitoDoenca
    {
        public string Nome { get; set; } = "Intoxicação";

        public string Descricao { get; set; } = "O hospédeiro sofre de efeitos de envenenamento.";

        public Intoxicacao(TipoDoenca tipo) { }

        public void Aplicar(ICriaturaCombatente alvo, int nivel)
        {
            CondicaoController.AplicarOuAtualizarCondicao(new Veneno(nivel, 1), alvo.Condicoes);
            TextoController.CentralizarTexto($"{alvo.Nome} sofre de intoxicação");
        }
    }
}
