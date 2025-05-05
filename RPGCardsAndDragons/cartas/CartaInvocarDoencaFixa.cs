using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.Aliados;
using CardsAndDragons.Controllers;
using CardsAndDragons;
using CardsAndDragonsJogo;
using RPGCardsAndDragons.doencas;

namespace RPGCardsAndDragons.cartas
{
    public class CartaInvocarDoencaFixa : ICartaUsavel
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Raridade RaridadeCarta { get; set; }
        public TipoCarta TipoCarta { get; set; } = TipoCarta.Doenca;
        public int Preco { get; set; }
        public int CustoVida { get; set; }
        public int CustoMana { get; set; }
        public int CustoStamina { get; set; }
        public int CustoOuro { get; set; }
        public List<string> Modelo { get; set; }

        private Doenca doenca;

        public bool Usar(Batalha batalha)
        {
            var jogador = batalha.Jogador;

            if (!TemRecursos(jogador))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                TextoController.CentralizarTexto("Você não tem recursos suficientes.");
                Console.ResetColor();
                return false;
            }

            ConsumirRecursos(jogador);

            if (doenca == null)
            {
                TextoController.CentralizarTexto("Desenvolva sua doença:");

                doenca = CondicaoController.IncubarDoenca();

                int CustoNovo = 0;

                foreach (var efeito in doenca.Efeitos)
                {
                    CustoNovo += doenca.Tipo.ObterCustoEfeito(efeito);
                }

                CustoNovo += doenca.Tipo.ObterCustoTransmissao(doenca.Transmissao);

                this.CustoMana = CustoNovo;
                this.CustoOuro = (CustoNovo / 10);
            }


            // Clona a doença para não ser ponteiro
            var doencaClone = new Doenca(doenca);

            var alvo = batalha.Inimigos[AlvoController.SelecionarAlvo(batalha.Inimigos)];

            CondicaoController.AplicarOuAtualizarCondicao(doencaClone, alvo.Condicoes);
            TextoController.CentralizarTexto($"{alvo.Nome} foi infectado...");

            this.Nome = $"{doenca.Tipo.Nome} - {doenca.Nome}";

            return true;
        }

        private bool TemRecursos(Personagem jogador)
        {
            return jogador.VidaAtual >= CustoVida &&
                   jogador.ManaAtual >= CustoMana &&
                   jogador.StaminaAtual >= CustoStamina &&
                   jogador.Ouros >= CustoOuro;
        }

        private void ConsumirRecursos(Personagem jogador)
        {
            jogador.VidaAtual -= CustoVida;
            jogador.ManaAtual -= CustoMana;
            jogador.StaminaAtual -= CustoStamina;
            jogador.Ouros -= CustoOuro;
        }
    }
}
