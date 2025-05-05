using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.Controllers;
using CardsAndDragonsJogo;

namespace CardsAndDragons.Cartas
{
    public class CartaEvolutiva : ICartaUsavel
    {
        public string Nome { get; set; }
        public string NomeEvolucao { get; set; }

        public string Descricao { get; set; }
        public string DescricaoEvolucao { get; set; }

        public Raridade RaridadeCarta { get; set; }
        public Raridade RaridadeEvolucao { get; set; }

        public TipoCarta TipoCarta { get; set; } = TipoCarta.Evolutiva;

        public int Preco { get; set; }

        public int CustoVida { get; set; }
        public int CustoVidaEvolucao { get; set; }

        public int CustoMana { get; set; }
        public int CustoManaEvolucao { get; set; }

        public int CustoStamina { get; set; }
        public int CustoStaminaEvolucao { get; set; }

        public int CustoOuro { get; set; }
        public int CustoOuroEvolucao { get; set; }

        public List<string> Modelo { get; set; }
        public List<string> ModeloEvolucao { get; set; }

        public bool Evoluiu { get; set; }
        public int VezesUso { get; set; }
        public int usosAteEvoluir { get; set; }

        public Action<Batalha> EfeitoEvolucao { get; set; }
        public Action<Batalha> EfeitoPadrao { get; set; }

        public bool Usar(Batalha batalha)
        {
            if (!TemRecursos(batalha.Jogador))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                TextoController.CentralizarTexto("! Você não tem recursos suficientes !");
                Console.ResetColor();
                return false;
            }

            ConsumirRecursos(batalha.Jogador);

            if (Evoluiu)
            {
                EfeitoEvolucao?.Invoke(batalha);
            }
            else
            {
                VezesUso++;

                EfeitoPadrao?.Invoke(batalha);

                if(usosAteEvoluir < 99)
                {
                    if (VezesUso >= usosAteEvoluir)
                    {
                        Evoluir();
                        TextoController.CentralizarTexto($"A carta {Nome} evoluiu para {NomeEvolucao}!");
                        VezesUso = 0;
                    }
                } 
            }

            return true;
        }

        public void Evoluir()
        {
            Evoluiu = true;
            this.Nome = NomeEvolucao;
            this.Modelo = ModeloEvolucao;
            this.Descricao = DescricaoEvolucao;
            this.RaridadeCarta = RaridadeEvolucao;

            this.CustoVida = CustoVidaEvolucao;
            this.CustoMana = CustoManaEvolucao;
            this.CustoStamina = CustoStaminaEvolucao;
            this.CustoOuro = CustoOuroEvolucao;
        }

        private bool TemRecursos(Personagem jogador) =>
            jogador.VidaAtual >= CustoVida &&
            jogador.ManaAtual >= CustoMana &&
            jogador.StaminaAtual >= CustoStamina &&
            jogador.Ouros >= CustoOuro;

        private void ConsumirRecursos(Personagem jogador)
        {
            jogador.VidaAtual -= CustoVida;
            jogador.ManaAtual -= CustoMana;
            jogador.StaminaAtual -= CustoStamina;
            jogador.Ouros -= CustoOuro;
        }
    }

}
