using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragonsJogo;

namespace RPGCardsAndDragons.Aplicadores
{
    public interface IAplicador
    {
        bool Aplicar(Batalha batalha);
    }
}
