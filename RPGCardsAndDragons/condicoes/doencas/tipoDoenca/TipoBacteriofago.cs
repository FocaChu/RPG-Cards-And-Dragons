using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGCardsAndDragons.condicoes.doencas.efeitoDoenca;
using RPGCardsAndDragons.condicoes.doencas.transmissaoDoenca;
using RPGCardsAndDragons.doencas;

namespace RPGCardsAndDragons.condicoes.doencas.tipoDoenca
{
    public class TipoBacteriofago : TipoDoenca
    {
        public override string Nome => "Bacteriófago";

        public override string Descricao =>
            "Um vírus extremamente agressivo e violento ao hospedeiro.";

        public override int ObterCustoEfeito(IEfeitoDoenca efeito)
        {
            if (efeito is Necrose) return 10;
            if (efeito is Hemorragia) return 20;
            if (efeito is PeleEscaldada) return 20;
            if (efeito is ConfusaoMental) return 30;
            return 0;
        }

        public override int ObterCustoTransmissao(ITipoTransmissao transmissao)
        {
            if (transmissao is TransmissaoTeleguiada) return 25;
            return 0;
        }

        public override List<IEfeitoDoenca> CriarEfeitos()
        {
            return new List<IEfeitoDoenca>
        {
            new Necrose(),
            new Hemorragia(),
            new PeleEscaldada(),
            new ConfusaoMental()
        };
        }

        public override List<ITipoTransmissao> CriarTransmissoes()
        {
            return new List<ITipoTransmissao>
        {
            new TransmissaoTeleguiada(),
        };
        }
    }

}
