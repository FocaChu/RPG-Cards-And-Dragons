using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.ClassesCondicoes;
using CardsAndDragons.Controllers;

namespace CardsAndDragons.Condicoes
{
    public class ModificacaoDano : ICondicaoEmpilhavel
    {
        public string Nome => "Modificação de Dano";

        public int Nivel { get; set; }

        public int Duracao { get; set; }

        public ModificacaoDano(int nivel, int duracao)
        {
            Nivel = nivel;
            Duracao = duracao;
        }

        public override string ToString()
        {
            return $"{this.Nome} Nível: {this.Nivel} / Duração: {this.Duracao}";
        }

        //Pode ser aplicada tanto a jogadores quando inimigos

        public void AplicarEfeito(Personagem jogador)
        {
            jogador.ModificadorDano += Nivel;
        }

        public void AplicarEfeito(ICriaturaCombatente criatura)
        {
            criatura.ModificadorDano += Nivel;
        }


        //ao ser atualizada perde nivel e duração
        public void Atualizar()
        {
            Duracao--;
        }

        public bool Expirou()
        {
            return Duracao <= 0 || Nivel == 0;
        }

        //Metodo da interface que permite vc acumular a condição(de forma correta)
        public void Fundir(ICondicaoTemporaria nova)
        {
            var novoModificadorDano = nova as ModificacaoDano;
            if (novoModificadorDano == null) return;

            this.Nivel += novoModificadorDano.Nivel;
            this.Duracao = Math.Max(this.Duracao, novoModificadorDano.Duracao);
        }
    }
}
