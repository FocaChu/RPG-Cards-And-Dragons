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
    public class TipoParasita : TipoDoenca
    {
        public override string Nome
        {
            get { return "Parasita"; }
        }

        public override string Descricao
        {
            get { return "Parasitas que invadem e danificam o hospedeiro."; }
        }

        public override int ObterCustoEfeito(IEfeitoDoenca efeito)
        {
            if (efeito is Necrose) return +30;
            if (efeito is Fraqueza) return +20;
            if (efeito is Sensibilidade) return +30;
            return 0;
        }
        public override int ObterCustoTransmissao(ITipoTransmissao transmissao)
        {
            if (transmissao is TransmissaoTeleguiada) return 20;
            return 0;
        }

        public override List<IEfeitoDoenca> CriarEfeitos()
        {
            return new List<IEfeitoDoenca>
        {
            new Necrose(this),
            new Fraqueza(this),
            new Sensibilidade(this)
        };
        }

        public override List<ITipoTransmissao> CriarTransmissoes()
        {
            return new List<ITipoTransmissao>
        {
            new TransmissaoTeleguiada()
        };
        }
    }


}
