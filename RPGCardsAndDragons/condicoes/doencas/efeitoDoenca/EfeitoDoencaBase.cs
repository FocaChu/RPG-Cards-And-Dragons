using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons;
using RPGCardsAndDragons.doencas;

namespace RPGCardsAndDragons.condicoes.doencas.efeitoDoenca
{
    public abstract class EfeitoDoencaBase : IEfeitoDoenca
    {
        public int Custo { get; set; }

        public EfeitoDoencaBase(TipoDoenca tipo, int custoBase)
        {
            Custo = custoBase + tipo.ModificarCusto(this);
        }

        public abstract void Aplicar(ICriaturaCombatente alvo, int nivel);
    }
}
