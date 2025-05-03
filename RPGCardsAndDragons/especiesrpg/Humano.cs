using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.ClassesDasCartas;
using CardsAndDragonsJogo;

namespace CardsAndDragons
{
    public class Humano : EspecieRPG
    {
        public override string NomeEspecie => "Humano";

        public override string DescricaoEspecie => "Versatil e estavel. Resiliente a adversidades.";

        public override List<ICartaUsavel> DefinirCartasIniciais()
        {
            return new List<ICartaUsavel>
    {
        FabricaDeCartas.CriarFlechada(), FabricaDeCartas.CriarFlechada(), FabricaDeCartas.CriarFlechada(), FabricaDeCartas.CriarFlechada(),
        FabricaDeCartas.CriarPocaoDeCura(), FabricaDeCartas.CriarPocaoDeCura(),
        FabricaDeCartas.CriarBemMunido(), FabricaDeCartas.CriarBemMunido(),
        FabricaDeCartas.CriarSaraivada(),
        FabricaDeCartas.CriarDisparoPerfurante()
    };
        }
    }
}
