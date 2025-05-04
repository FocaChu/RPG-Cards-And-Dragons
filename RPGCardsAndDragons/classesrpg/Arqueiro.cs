using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.ClassesDasCartas;
using CardsAndDragonsJogo;

namespace CardsAndDragons.ClassesClasses
{
    public class Arqueiro : ClasseRPG
    {
        public override int VidaMax => 120;

        public override int ManaMax => 70;

        public override int StaminaMax => 110;

        public override string NomeClasse => "Arqueiro";

        public override string DescricaoClasse => "Possui ataques em aréa e otimo para combos de ataque.";

        //aqui ele sobrepoem o metodo herdado de ClasseRPG que vai ser chamado no construtor do personagem. Dentro desse metodo da pra configurar todas as cartas iniciais.
        //que a classe vai ter, basta chamar a fabrica de cartas e a função com a carta.
        public override List<ICartaUsavel> DefinirCartasIniciais()
        {
            return new List<ICartaUsavel>
    {
        FabricaDeCartas.CriarFlechada(), FabricaDeCartas.CriarFlechada(), FabricaDeCartas.CriarFlechada(), FabricaDeCartas.CriarFlechada(),
        FabricaDeCartas.CriarPocaoDeCura(), 
        FabricaDeCartas.CriarBemMunido(), FabricaDeCartas.CriarBemMunido(),
        FabricaDeCartas.CriarSaraivada(),
        FabricaDeCartas.CriarDisparoPerfurante()
    };
        }
    }
}