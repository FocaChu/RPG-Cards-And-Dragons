using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.ClassesDasCartas;
using CardsAndDragons.Controllers;
using CardsAndDragonsJogo;

namespace CardsAndDragons
{
    public class Loja
    {
        public List<ICartaUsavel> CartasAVenda { get; set; }

        public bool CuraDisponivel { get; set; }

        public Personagem Jogador { get; set; }

        public Loja(Personagem jogador)
        {
            this.Jogador = jogador;
            this.CartasAVenda = GerarEstoque();
            this.CuraDisponivel = true;
        }

        public List<ICartaUsavel> GerarEstoque()
        {
            List<ICartaUsavel> todasCartas = LojaController.ObterTodasCartasDisponiveis(this);

            List<ICartaUsavel> estoque = new List<ICartaUsavel>();

            Random rng = new Random();

            for (int i = 0; i < 5; i++)
            {
                int index = rng.Next(todasCartas.Count);
                estoque.Add(todasCartas[index]);
            }

            return estoque;
        }
    }
}


