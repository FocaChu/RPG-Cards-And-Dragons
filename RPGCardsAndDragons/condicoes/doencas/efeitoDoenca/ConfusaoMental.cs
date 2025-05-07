using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.Controllers;
using CardsAndDragons;
using CardsAndDragonsJogo;
using RPGCardsAndDragons.doencas;
using CardsAndDragons.ClassesCondicoes;

namespace RPGCardsAndDragons.condicoes.doencas.efeitoDoenca
{
    public class ConfusaoMental : IEfeitoDoenca
    {
        public string Nome { get; set; } = "Confusão Mental";

        public string Descricao { get; set; } = "O hospédeiro sofre de efeitos de paranóia.";

        public ConfusaoMental(TipoDoenca tipo) { }

        public ConfusaoMental(ConfusaoMental original)
        {
            Nome = original.Nome;
            Descricao = original.Descricao;
        }

        public void Aplicar(Batalha batalha, OInimigo alvo, int nivel)
        {
            batalha.Aplicadores.Add(new AplicadorCondicao(new Paranoia(nivel, 2), alvo));

            TextoController.CentralizarTexto($"{alvo.Nome} sofre de efeitos de paranóia");
        }
    }

}
