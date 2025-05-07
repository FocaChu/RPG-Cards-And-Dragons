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
    public class DorDeGarganta : IEfeitoDoenca
    {
        public string Nome { get; set; } = "Dor de Garganta";

        public string Descricao { get; set; } = "O hospédeiro sofre de efeitos de silêncio.";

        public DorDeGarganta(TipoDoenca tipo) { }

        public DorDeGarganta(DorDeGarganta original)
        {
            Nome = original.Nome;
            Descricao = original.Descricao;
        }

        public void Aplicar(Batalha batalha, OInimigo alvo, int nivel)
        {

            int chance = (int)(nivel * 10) / 2;

            int opcao = BatalhaController.GerarRNG(chance);

            if (opcao == 0)
            {
                batalha.Aplicadores.Add(new AplicadorCondicao(new Silencio(1, 2), alvo));
                TextoController.CentralizarTexto($"{alvo.Nome} sofre de efeitos de paranóia");
            }
        }
    }
}
