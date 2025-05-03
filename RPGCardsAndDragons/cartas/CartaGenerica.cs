using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons;
using CardsAndDragons.Controllers;

namespace CardsAndDragonsJogo.ClassesCartas
{
    //Nossa carta generica, toda carta que não faz nada de muito complexo ou especial é uma carta generica. Mesmo que seus atributos sejam altos, se ele ainda é simples, é generica
    public class CartaGenerica : ICartaUsavel
    {
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public Raridade RaridadeCarta { get; set; }

        public TipoCarta TipoCarta { get; set; } = TipoCarta.Generica;

        public int Preco { get; set; }

        public int CustoVida { get; set; }

        public int CustoMana { get; set; }

        public int CustoStamina { get; set; }

        public int CustoOuro { get; set; }

        public List<string> Modelo { get; set; }

        public Action<Batalha> Efeito { get; set; }

        public bool Usar(Batalha batalha)
        {
            //verifica se o jogador tem recurso pra usar a carta
            if (!TemRecursos(batalha.Jogador))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                TextoController.CentralizarTexto("! Você não tem recursos suficientes !");
                Console.ResetColor();
                return false;
            }

            //consume os recursos
            ConsumirRecursos(batalha.Jogador);

            Efeito?.Invoke(batalha); // Aplica o efeito, SE existir

            return true;
        }


        //Codigos que veem se vc tem recursos que chega e depois cobra eles
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

