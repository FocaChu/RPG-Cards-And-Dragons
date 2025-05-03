using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.Controllers;
using CardsAndDragonsJogo;

namespace CardsAndDragons.Cartas
{
    public class CartaRecarregavel : ICartaUsavel
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Raridade RaridadeCarta { get; set; }
        public TipoCarta TipoCarta { get; set; } = TipoCarta.Recarregavel;
        public int Preco { get; set; }
        public int CustoVida { get; set; }
        public int CustoMana { get; set; }
        public int CustoStamina { get; set; }
        public int CustoOuro { get; set; }
        public List<string> Modelo { get; set; }

        public int CargasMaximas { get; set; }
        public int cargasAtuais { get; set; }

        public Action<Batalha> EfeitoComCarga { get; set; }
        public Action<Batalha> EfeitoSemCarga { get; set; }

        private int ReguladorDeStatus(int valor, int valorMax)
        {
            if (valor < 0) return 0;
            //Se o valor (o status atual) for menor do que zero, o codigo breca e poem ele como zero, evitando coisas como -15 de vida.

            if (valor > valorMax) return valorMax;
            //Se o valor (o status atual) for maior que o limite maximo estipulado, o codigo breca e limita ele, impedido vc ter vida infinita por exemplo

            // Se estiver no intervalo permitido manda o valor normal
            return valor;
        }

        public int CargasAtuais
        {
            get => cargasAtuais;
            set => cargasAtuais = ReguladorDeStatus(value, CargasMaximas);
            //Pega o ouro atual, seta ela pelos parametros passados e limita ela pelo minimo e maximo
        }

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

            if (CargasAtuais > 0)
            {
                CargasAtuais--;
                EfeitoComCarga?.Invoke(batalha);
            }
            else
            {
                CargasAtuais = CargasMaximas;
                EfeitoSemCarga?.Invoke(batalha);
            }

            return true;
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
