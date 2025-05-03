using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.ClassesDasCartas;
using CardsAndDragonsJogo;

namespace CardsAndDragons.ClassesEspecies
{
    public class Anao : EspecieRPG
    {
        public override string NomeEspecie => "Anão";

        public override string DescricaoEspecie => "Pequeno e robusto, acosumado a lidar com fortunas";

        public override List<ICartaUsavel> DefinirCartasIniciais()
        {
            return new List<ICartaUsavel>
    {
        FabricaDeCartas.CriarEscudo()
    };
        }
    }
}
