using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.ClassesDasCartas;
using CardsAndDragonsJogo;

namespace CardsAndDragons.ClassesEspecies
{
    public class Vampiro : EspecieRPG
    {
        public override string NomeEspecie => "Vampiro";

        public override string DescricaoEspecie => "Sedutor e de fome voraz. Otimo hemomante";

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
