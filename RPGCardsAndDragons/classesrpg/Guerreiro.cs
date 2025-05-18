using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragonsJogo.ClassesCartas;
using CardsAndDragonsJogo;
using CardsAndDragons.ClassesDasCartas;

namespace CardsAndDragons.ClassesClasses
{
    public class Guerreiro : ClasseRPG
    {
        public override int VidaMax => 150;

        public override int ManaMax => 50;

        public override int StaminaMax => 100;

        public override string NomeClasse => "Guerreiro";

        public override string DescricaoClasse => "Possui alta resistência e força bruta";

        //aqui ele sobrepoem o metodo herdado de ClasseRPG que vai ser chamado no construtor do personagem. Dentro desse metodo da pra configurar todas as cartas iniciais.
        //que a classe vai ter, basta chamar a fabrica de cartas e a função com a carta.
        public override List<ICartaUsavel> DefinirCartasIniciais()
        {
            return new List<ICartaUsavel>
    {
        FabricaDeCartas.CriarEspadada(), FabricaDeCartas.CriarEspadada(), FabricaDeCartas.CriarEspadada(), FabricaDeCartas.CriarEspadada(),
        FabricaDeCartas.CriarPocaoDeCura(),
        FabricaDeCartas.CriarEscudo(), FabricaDeCartas.CriarEscudo(),
        FabricaDeCartas.CriarEspadaEEscudo(),
        FabricaDeCartas.CriarGolpeEmpoderado(),

    };
        }

    }
}
