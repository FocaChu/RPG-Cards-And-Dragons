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

        public override int ObterCustoEfeito(IEfeitoDoenca efeito)
        {
            if (efeito is Necrose) return 30;
            if (efeito is Sensibilidade) return 40;
            if (efeito is Zumbificacao) return 40;
            return 0;
        }

        public override int ObterCustoTransmissao(ITipoTransmissao transmissao)
        {
            if (transmissao is TransmissaoAr) return 15;
            if (transmissao is TransmissaoTeleguiada) return 30;
            return 0;
        }

        public override List<IEfeitoDoenca> CriarEfeitos()
        {
            return new List<IEfeitoDoenca>
        {
            new Necrose(this),
            new Sensibilidade(this),
            new Zumbificacao(this)
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
