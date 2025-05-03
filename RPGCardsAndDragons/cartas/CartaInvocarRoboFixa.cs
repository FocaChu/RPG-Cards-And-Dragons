using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.Aliados;
using CardsAndDragons.Controllers;
using CardsAndDragonsJogo;

namespace CardsAndDragons.Cartas
{
    public class CartaInvocarRoboFixa : ICartaUsavel
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Raridade RaridadeCarta { get; set; }
        public TipoCarta TipoCarta { get; set; } = TipoCarta.Robo;
        public int Preco { get; set; }
        public int CustoVida { get; set; }
        public int CustoMana { get; set; }
        public int CustoStamina { get; set; }
        public int CustoOuro { get; set; }
        public List<string> Modelo { get; set; }

        private RoboAliado roboFixo;

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

            if (roboFixo == null)
            {
                TextoController.CentralizarTexto("Configure seu robô:");
                roboFixo = AliadoController.ConfigurarRobo();
            }

            // Clona o robô para não ser ponteiro
            var roboClone = new RoboAliado(roboFixo.Software, roboFixo.Hardware, roboFixo.Nome);

            batalha.Aliados.Add(roboClone);
            TextoController.CentralizarTexto($"O robô {roboClone.Nome} foi invocado!");

            this.Nome = $"BluePrint - {roboFixo.Nome}";

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
