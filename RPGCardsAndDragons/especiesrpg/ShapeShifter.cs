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
                FabricaDeCartas.CriarMimico(), FabricaDeCartas.CriarMimico(), FabricaDeCartas.CriarMimico(),
                FabricaDeCartas.CriarSilenciar(), 
                FabricaDeCartas.CriarInseguranca(),
            };
        }
    }
}
