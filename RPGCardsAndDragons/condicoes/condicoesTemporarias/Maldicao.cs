using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.Controllers;
using CardsAndDragonsJogo;

namespace CardsAndDragons.ClassesCondicoes
{
    //MAIS INFORMAÇÕES NA INTERFACE DE CONDIÇAO TEMPORARIA 
    //Condição especial de Maldição
    //Resumo: O objetivo é acumular o maximo possivel, quando o nivel da maldição for igual a vida Atual do alvo, ele ira perder todos os pontos de vida(morrendo)
    //(maldição conta como temporaria)

    public class Maldicao : ICondicaoEmpilhavel
    {
        public string Nome => "Maldição";


        public int Nivel { get; set; }

        public int Duracao { get; set; } = int.MaxValue;

        public Maldicao(int nivel)
        {
            Nivel = nivel;
        }

        public override string ToString()
        {
            return $"{this.Nome} Nível: {this.Nivel} / Duração: ???";
        }

        //Pode ser aplicado tanto no jogador quando nos inimigos

        public void AplicarEfeito(Personagem jogador)
        {
            if (jogador.VidaAtual <= Nivel)
            {
                jogador.VidaAtual = 0;
                TextoController.CentralizarTexto($"{jogador.Nome} foi consumido pela Maldição!");
            }
        }

        public void AplicarEfeito(ICriaturaCombatente criatura)
        {
            if (criatura.VidaAtual <= Nivel)
            {
                criatura.SofrerDano(criatura.VidaAtual, true);
                TextoController.CentralizarTexto($"{criatura.Nome} foi consumido pela Maldição!");
            }
        }

        public void Fundir(ICondicaoTemporaria nova)
        {
            var novaMaldicao = nova as Maldicao;
            if (novaMaldicao == null) return;

            this.Nivel += novaMaldicao.Nivel;
        }

        public void Atualizar() { } // Não faz nada, ou seja, a maldição é permanente ao longo da batalha para tornar seu efeito viavel

        public bool Expirou() => false;
    }

}
