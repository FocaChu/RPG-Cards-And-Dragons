using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.ClassesDasCartas;
using CardsAndDragonsJogo;

namespace CardsAndDragons.ClassesEspecies
{
    public class Elfo : EspecieRPG
    {
        public override string NomeEspecie => "Elfo";

        public override string DescricaoEspecie => "Agil e habilidoso. Grande amigo da natureza.";

        public override List<ICartaUsavel> DefinirCartasIniciais()
        {
            return new List<ICartaUsavel>
            {
                FabricaDeCartas.CriarChicotada(), FabricaDeCartas.CriarChicotada(), FabricaDeCartas.CriarChicotada(),
                FabricaDeCartas.CriarMixDeErvas(),
                FabricaDeCartas.CriarFrenesiImpetuoso(),
            };
        }
    }
}
