using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.ClassesDasCartas;
using CardsAndDragonsJogo;

namespace CardsAndDragons.ClassesClasses
{
    public class Engenheiro : ClasseRPG
    {
        public override int VidaMax => 100;

        public override int ManaMax => 100;

        public override int StaminaMax => 100;

        public override string NomeClasse => "Engenheiro";

        public override string DescricaoClasse => "Pode invocar robôs para ajudar em combate.";

        //aqui ele sobrepoem o metodo herdado de ClasseRPG que vai ser chamado no construtor do personagem. Dentro desse metodo da pra configurar todas as cartas iniciais.
        //que a classe vai ter, basta chamar a fabrica de cartas e a função com a carta.
        public override List<ICartaUsavel> DefinirCartasIniciais()
        {
            return new List<ICartaUsavel>
    {
        FabricaDeCartas.CriarAtaqueMagico(), FabricaDeCartas.CriarAtaqueMagico(), FabricaDeCartas.CriarAtaqueMagico(), FabricaDeCartas.CriarAtaqueMagico(),
        FabricaDeCartas.CriarPocaoDeCura(), FabricaDeCartas.CriarPocaoDeCura(),
        FabricaDeCartas.CriarInvocarRoboFixo(), FabricaDeCartas.CriarInvocarRoboFixo(),
        FabricaDeCartas.CriarRoboTemporario(), FabricaDeCartas.CriarReparos() ,

    };
        }
    }
}