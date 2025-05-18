using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGCardsAndDragons.condicoes.doencas.efeitoDoenca;
using RPGCardsAndDragons.condicoes.doencas.transmissaoDoenca;
using RPGCardsAndDragons.doencas;

namespace RPGCardsAndDragons.condicoes.doencas.tipoDoenca
{
    public class TipoBacteria : TipoDoenca
    {
        public override string Nome => "Bactéria";

        public override string Descricao =>
            "Organismos versáteis que se adaptam e desgastam o corpo com múltiplas fraquezas.";

        public override int ObterCustoEfeito(IEfeitoDoenca efeito)
        {
            if (efeito is PeleEscaldada) return 20;
            if (efeito is Fraqueza) return 25;
            if (efeito is Hemorragia) return 20;
            return 0;
        }

        public override int ObterCustoTransmissao(ITipoTransmissao transmissao)
        {
            if (transmissao is TransmissaoAr) return 10;
            return 0;
        }

        public override List<IEfeitoDoenca> CriarEfeitos()
        {
            return new List<IEfeitoDoenca>
        {
            new PeleEscaldada(),
            new Fraqueza(),
            new Hemorragia()
        };
        }

        public override List<ITipoTransmissao> CriarTransmissoes()
        {
            return new List<ITipoTransmissao>
        {
            new TransmissaoAr()
        };
        }
    }

}
