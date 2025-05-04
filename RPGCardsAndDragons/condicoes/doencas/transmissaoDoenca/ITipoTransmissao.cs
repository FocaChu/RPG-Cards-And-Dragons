using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons;
using CardsAndDragons.ClassesCondicoes;
using CardsAndDragons.Controllers;
using CardsAndDragonsJogo;

namespace RPGCardsAndDragons.doencas
{
    public interface ITipoTransmissao
    {
        string Nome { get; }

        string Descricao { get; }

        bool TentarTransmitir(Doenca doenca, List<OInimigo> alvos, int chance);
    }

}
