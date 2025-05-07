using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.Controllers;
using CardsAndDragons;
using CardsAndDragonsJogo;
using RPGCardsAndDragons.doencas;

namespace RPGCardsAndDragons.condicoes.doencas.transmissaoDoenca
{
    public class TransmissaoAr : ITipoTransmissao
    {
        public string Nome => "Transmissão pelo Ar";
        public string Descricao => "Pode transmitir para qualquer inimigo no campo de batalha";

        public bool TentarTransmitir(Doenca doenca, Batalha batalha, int chance)
        {
            // Verifica se há alvos disponíveis para transmitir
            var alvosViaveis = batalha.Inimigos.Where(alvo =>
                alvo.Condicoes.All(cond => !(cond is ICondicaoContagiosa contagiosa && cond.Nome == doenca.Nome))
            ).ToList();

            if (alvosViaveis.Count == 0)
            {
                TextoController.CentralizarLinha("Não há alvos viáveis para infecção.");
                return false;
            }

            // Checa o RNG de infecção
            int transmissao = BatalhaController.GerarRNG(chance - 5);

            if (transmissao == 0)
            {
                var alvo = AlvoController.EscolherInimigoAleatorio(alvosViaveis);

                // Aplica a infecção
                batalha.Aplicadores.Add(new AplicadorCondicao(new Doenca(doenca), alvo));

                Console.WriteLine();
                TextoController.CentralizarLinha($"{alvo.Nome} foi infectado pelo patógeno {doenca.Nome}\n");

                return true;
            }

            return false;
        }

    }
}
