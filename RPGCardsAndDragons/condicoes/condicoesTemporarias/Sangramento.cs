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
    //Condição especial de Sangramento
    //Resumo: Causa dano igual ao nivel todo checape e acumula, tambem causa dano ao alvo sempre que ele ataca

    public class Sangramento : ICondicaoEmpilhavel
    {
        public string Nome => "Sangramento";

        public int Nivel { get; set; }

        public int Duracao { get; set; }

        public Sangramento(int nivel, int duracao)
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
            if (Nivel > 0 && Duracao > 0)
            {
                jogador.VidaAtual -= Nivel;
                TextoController.CentralizarTexto($"{jogador.Nome} sofre {Nivel} de dano por Sangramento!");
                Nivel -= jogador.Regeneracao;
            }
            else
            {
                TextoController.CentralizarTexto($"O sangramento de {jogador.Nome} foi estancado");
            }
        }

        public void AplicarEfeito(ICriaturaCombatente criatura)
        {
            if(criatura is RoboAliado robo)
            {
                TextoController.CentralizarTexto($"{criatura.Nome} é imune a essa condicao");
                this.Duracao = 0;
            }
            else if(Nivel > 0 && Duracao > 0)
            {
                criatura.SofrerDano(criatura, Nivel, true, false);
                TextoController.CentralizarTexto($"{criatura.Nome} sofre {Nivel} de dano por Sangramento!");
            }
        }


        //ao ser atualizada perde nivel e duração
        public void Atualizar()
        {
            Duracao--;
            Nivel--;
        }

        public bool Expirou()
        {
            return Duracao <= 0;
        }

        //Metodo da interface que permite vc acumular a condição(de forma correta)
        public void Fundir(ICondicaoTemporaria nova)
        {
            var novoSangramento = nova as Sangramento;
            if (novoSangramento == null) return;

            this.Nivel += novoSangramento.Nivel;
            this.Duracao = Math.Max(this.Duracao, novoSangramento.Duracao);
        }
    }

}
