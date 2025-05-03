using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragonsJogo;

namespace CardsAndDragons.ClassesCondicoes
{
    //É a interface que vai dar forma e função a todas as condições temporarias do jogo!!!
    //Aqui é definido tudo, as condições temporarias elas vão ser ativadas durante o checape(que fica em CondicaoController) no checape todos os efeitos são ativados
    //Algumas condições elas não tem efeitos diretos, oque significa que no checape elas n fazem nada, mas fora dele elas fazem (Ex: Congelamento)
    //O nivel vai ser o poder de efeito dessa condição, e a duração quantos turnos dura(lembrando que o checapete acontece antes do turnos do inimigos e isso já conta como 1 turno)

    public interface ICondicaoTemporaria
    {
        string Nome { get; }

        int Nivel { get; set; }

        int Duracao { get; set; }

        void AplicarEfeito(Personagem jogador);

        void AplicarEfeito(ICriaturaCombatente criatura);

        void Atualizar();

        bool Expirou();
    }
}
