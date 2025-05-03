using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.ClassesDasCartas;
using CardsAndDragonsJogo;

namespace CardsAndDragons.ClassesEspecies
{
    public class ShapeShifter : EspecieRPG
    {
        public override string NomeEspecie => "ShapeShifter";

        public override string DescricaoEspecie => "Um vulto amorfo e inconstante com intenções dubias";

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
