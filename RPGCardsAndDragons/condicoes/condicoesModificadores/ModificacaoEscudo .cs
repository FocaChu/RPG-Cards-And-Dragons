using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.ClassesCondicoes;
using CardsAndDragons.Controllers;

namespace CardsAndDragons.Condicoes
{
    public class ModificacaoEscudo : ICondicaoEmpilhavel
    {
        public string Nome => "Modificação de Escudo";

        public int Nivel { get; set; }

        public int Duracao { get; set; }

        public ModificacaoEscudo(int nivel, int duracao)
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
            jogador.Escudo += Nivel;
        }

        public void AplicarEfeito(ICriaturaCombatente criatura)
        {
            criatura.Escudo += Nivel;
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
            var novoModificadorEscudo = nova as ModificacaoEscudo;
            if (novoModificadorEscudo == null) return;

            this.Nivel += novoModificadorEscudo.Nivel;
            this.Duracao = Math.Max(this.Duracao, novoModificadorEscudo.Duracao);
        }
    }
}
