using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons;
using CardsAndDragons.ClassesCondicoes;
using CardsAndDragonsJogo;

namespace RPGCardsAndDragons.doencas
{
    public interface ICondicaoContagiosa : ICondicaoTemporaria
    {
        string Descricao { get; set; }

        ITipoTransmissao Transmissao { get; set; }

        List<IEfeitoDoenca> Efeitos { get; set; }

        bool TentarTransmitir(List<OInimigo> alvos, int chance, Doenca clone);
    }

}
