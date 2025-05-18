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
    public class TipoVirus : TipoDoenca
    {
        public override string Nome => "Vírus";

        public override string Descricao =>
            "Vírus rápidos e mutáveis que atacam com sintomas intensos e variados.";

        public override int ObterCustoEfeito(IEfeitoDoenca efeito)
        {
            if (efeito is Intoxicacao) return 20;
            if (efeito is ConfusaoMental) return 35;
            if (efeito is Hemorragia) return 25;
            return 0;
        }

        public override int ObterCustoTransmissao(ITipoTransmissao transmissao)
        {
            if (transmissao is TransmissaoAr) return 25;
            return 0;
        }

        public override List<IEfeitoDoenca> CriarEfeitos()
        {
            return new List<IEfeitoDoenca>
        {
            new Intoxicacao(),
            new ConfusaoMental(),
            new Hemorragia()
        };
        }

        public override List<ITipoTransmissao> CriarTransmissoes()
        {
            return new List<ITipoTransmissao>
        {
            new TransmissaoAr(),
        };
        }
    }


}
