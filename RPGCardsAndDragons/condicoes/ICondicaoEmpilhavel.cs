using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsAndDragons.ClassesCondicoes
{

    //Esssa interface ela serve pra condições especiais que acumulam(veneno sangramento etc) ela poem o metodo fundir, que junta os niveis e poem a maior duração dentre os dois efeitos
    //Sem ele sempre que vc fosse aplicar uma condição especial no inimigo, ele iria estar com aquele efetio aplicado duas vezes
    //Ex: Veneno Nivel 2 Duração 1, Veneno Nivel 4 Duração 2 (vc levaria dano de dois venenos diferentes)

    public interface ICondicaoEmpilhavel : ICondicaoTemporaria
    {
        void Fundir(ICondicaoTemporaria nova);
    }

}
