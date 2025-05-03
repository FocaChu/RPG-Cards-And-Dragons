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
    //Condição especial de Silencio
    //Resumo: Não faz nada diretamente, não acumula, n tem como passar parametros. Pq? Dentro de OInimigo, onde verifica se ele pode ou não usar sua habilidade, existe um metodo do CondicaoControler
    //Esse metodo chamado VerificarSilencio ve se o inimigo ta silenciado, se sim ele não pode atacar com a habildiade especial nesse turno. Ou seja, ela em si n faz nada, mas a presença dela sim.

    public class Silencio : ICondicaoTemporaria
    {
        public string Nome => "Silêncio";

        public int Nivel { get; set; }

        public int Duracao { get; set; }

        public Silencio()
        {
            Nivel = 1;
            Duracao = 2;
        }

        public override string ToString()
        {
            return $"{this.Nome} Nível: {this.Nivel} / Duração: {this.Duracao}";
        }

        public void AplicarEfeito(Personagem jogador)
        {
        }

        public void AplicarEfeito(ICriaturaCombatente criatura)
        {
            TextoController.CentralizarTexto($"{criatura.Nome} foi silênciado!");
        }

        public void Atualizar() => Duracao--;

        public bool Expirou() => Duracao <= 0;
    }
}
