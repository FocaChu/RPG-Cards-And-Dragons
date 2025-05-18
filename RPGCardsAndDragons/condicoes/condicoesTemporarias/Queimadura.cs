using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.Aliados;
using CardsAndDragons.Controllers;
using CardsAndDragonsJogo;

namespace CardsAndDragons.ClassesCondicoes
{
    //MAIS INFORMAÇÕES NA INTERFACE DE CONDIÇAO TEMPORARIA
    //Condição especial de queimadura
    //Resumo: Reduz a cura recebida

    public class Queimadura : ICondicaoEmpilhavel
    {
        public string Nome => "Queimadura";

        public int Nivel { get; set; }

        public int Duracao { get; set; }

        public Queimadura(int nivel, int duracao)
        {
            Nivel = nivel;
            Duracao = duracao;
        }

        public override string ToString()
        {
            return $"{this.Nome} Nível: {this.Nivel} / Duração: {this.Duracao}";
        }


        //Aplicavel tanto a inimigos quanto a jogadores

        public void AplicarEfeito(Personagem jogador)
        {
            if (Nivel > 0 && Duracao > 0)
            {
                TextoController.CentralizarTexto($"{jogador.Nome} sofre de {Nivel} queimaduras!");
            }
            else
            {
                TextoController.CentralizarTexto($"As queimaduras de {jogador.Nome} começaram a curar");
            }
        }

        public void AplicarEfeito(ICriaturaCombatente criatura)
        {
            if (criatura is RoboAliado robo)
            {
                TextoController.CentralizarTexto($"{criatura.Nome} é imune a essa condicao");
                this.Duracao = 0;
            }
            else if (Nivel > 0 && Duracao > 0)
            {
                TextoController.CentralizarTexto($"{criatura.Nome} sofre {Nivel} de queimaduras!");
            }
            else
            {
                TextoController.CentralizarTexto($"As queimaduras de {criatura.Nome} começaram a curar");
            }
        }

        //ao ser atualizada perde 1 turno de duração
        public void Atualizar() => Duracao--;

        public bool Expirou() => Duracao <= 0;


        //Metodo da interface que permite vc acumular a condição(de forma correta)
        public void Fundir(ICondicaoTemporaria nova)
        {
            var novaQueimadura = nova as Queimadura;
            if (novaQueimadura == null) return;

            this.Nivel += novaQueimadura.Nivel;
            this.Duracao = Math.Max(this.Duracao, novaQueimadura.Duracao);
        }
    }


}
