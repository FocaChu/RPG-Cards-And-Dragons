using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.ClassesDasCartas;
using CardsAndDragonsJogo;

namespace CardsAndDragons.ClassesClasses
{
    public class Doutor : ClasseRPG
    {
        public override int VidaMax => 110;

        public override int ManaMax => 110;

        public override int StaminaMax => 80;

        public override string NomeClasse => "Doutor";

        public override string DescricaoClasse => "Pode fabricar e incubar doenças.";

        //aqui ele sobrepoem o metodo herdado de ClasseRPG que vai ser chamado no construtor do personagem. Dentro desse metodo da pra configurar todas as cartas iniciais.
        //que a classe vai ter, basta chamar a fabrica de cartas e a função com a carta.
        public override List<ICartaUsavel> DefinirCartasIniciais()
        {
            return new List<ICartaUsavel>
    {
        FabricaDeCartas.CriarAtaqueMagico(), FabricaDeCartas.CriarAtaqueMagico(), FabricaDeCartas.CriarAtaqueMagico(), FabricaDeCartas.CriarAtaqueMagico(),
        FabricaDeCartas.CriarPocaoDeCura(), FabricaDeCartas.CriarPocaoDeCura(),
        FabricaDeCartas.CriarInvocarDoencaFixa(), FabricaDeCartas.CriarInvocarDoencaFixa(),

    };
        }
    }
}