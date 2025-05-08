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
    //Condição especial de veneno
    //Resumo: Causa dano igual ao nivel todo checape e acumula

    public class Veneno : ICondicaoEmpilhavel
    {
        public string Nome => "Veneno";

        public int Nivel { get; set; }

        public int Duracao { get; set; }

        public Veneno(int nivel, int duracao)
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
                jogador.VidaAtual -= Nivel;
                TextoController.CentralizarTexto($"{jogador.Nome} sofre {Nivel} de Veneno!");
            }
            else
            {
                TextoController.CentralizarTexto($"O envenenamento de {jogador.Nome} enfraqueceu");
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
                criatura.SofrerDano(criatura, Nivel, true, false);
                TextoController.CentralizarTexto($"{criatura.Nome} sofre {Nivel} de Veneno!");
            }
        }

        //ao ser atualizada perde 1 turno de duração
        public void Atualizar() => Duracao--;

        public bool Expirou() => Duracao <= 0;


        //Metodo da interface que permite vc acumular a condição(de forma correta)
        public void Fundir(ICondicaoTemporaria nova)
        {
            var novoVeneno = nova as Veneno;
            if (novoVeneno == null) return;

            this.Nivel += novoVeneno.Nivel;
            this.Duracao = Math.Max(this.Duracao, novoVeneno.Duracao);
        }
    }


}
