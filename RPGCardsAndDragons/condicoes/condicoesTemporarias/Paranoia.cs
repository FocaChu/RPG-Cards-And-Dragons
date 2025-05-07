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
    //Condição especial de atordoamento
    //Resumo: Não faz nada diretamente, não acumula, n tem como passar parametros. Pq? Dentro do turno dos inimigos, existe um codigo de CondicaoController chamado VerificarCondicao
    //Esse codigo ve se o inimigo ta com uma condicao(nesse caso atordoamento), se sim ele não ataca. Ou seja, ela em si n faz nada, mas a presença dela sim.

    public class Paranoia : ICondicaoTemporaria
    {
        public string Nome => "Paranoia";

        public int Nivel { get; set; }

        public int Duracao { get; set; }

        public Paranoia(int nivel, int duracao)
        {
            Nivel = nivel;
            Duracao = duracao;
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
        }

        public void Atualizar() => Duracao--;

        public bool Expirou() => Duracao <= 0;
    }


}
