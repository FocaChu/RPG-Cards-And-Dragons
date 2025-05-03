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
    public class TipoFungo : TipoDoenca
    {
        public override string Nome
        {
            get { return "Fungo"; }
        }

        public override string Descricao
        {
            get { return "Fungos que enfraquecem a criatura ao longo do tempo."; }
        }

        public override int ModificarCusto(IEfeitoDoenca efeito)
        {
            if (efeito is DanoPercentualVida) return -2;
            if (efeito is ReduzirEscudo) return +3;
            return 0;
        }

        public override List<IEfeitoDoenca> CriarEfeitos()
        {
            return new List<IEfeitoDoenca>
        {
            new DanoPercentualVida(this),
            new ReduzirEscudo(this)
        };
        }

        public override List<ITipoTransmissao> CriarTransmissoes()
        {
            return new List<ITipoTransmissao>
        {
            new TransmissaoAr(),
            new TransmissaoTeleguiada()
        };
        }
    }


}
