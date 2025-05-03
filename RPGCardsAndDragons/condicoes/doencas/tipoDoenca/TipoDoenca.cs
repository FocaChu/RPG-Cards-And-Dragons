using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragonsJogo;
using RPGCardsAndDragons.doencas;

namespace RPGCardsAndDragons.condicoes.doencas
{
    public abstract class TipoDoenca
    {
        public abstract string Nome { get; }

        public abstract string Descricao { get; }

        public abstract List<IEfeitoDoenca> CriarEfeitos();

        public abstract List<ITipoTransmissao> CriarTransmissoes();

        public abstract int ModificarCusto(IEfeitoDoenca efeito);
    }

}

