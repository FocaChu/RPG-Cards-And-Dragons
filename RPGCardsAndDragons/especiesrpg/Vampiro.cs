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
                FabricaDeCartas.CriarDrenarSangue(), FabricaDeCartas.CriarDrenarSangue(), FabricaDeCartas.CriarDrenarSangue(),
                FabricaDeCartas.CriarSangueExplosivo(),
                FabricaDeCartas.CriarMordidaVampirica(),
            };
        }
    }
}
