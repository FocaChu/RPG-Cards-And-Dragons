using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.ClassesDasCartas;
using CardsAndDragonsJogo;

namespace CardsAndDragons.ClassesClasses
{
    public class Mago : ClasseRPG
    {
        public override int VidaMax => 90;

        public override int ManaMax => 150;

        public override int StaminaMax => 60;

        public override string NomeClasse => "Mago";

        public override string DescricaoClasse => "Possue fetiços poderosos e versáteis para controle de grupo.";

        //aqui ele sobrepoem o metodo herdado de ClasseRPG que vai ser chamado no construtor do personagem. Dentro desse metodo da pra configurar todas as cartas iniciais.
        //que a classe vai ter, basta chamar a fabrica de cartas e a função com a carta.
        public override List<ICartaUsavel> DefinirCartasIniciais()
        {
            return new List<ICartaUsavel>
    {
        FabricaDeCartas.CriarAtaqueMagico(), FabricaDeCartas.CriarAtaqueMagico(), FabricaDeCartas.CriarAtaqueMagico(), FabricaDeCartas.CriarAtaqueMagico(),
        FabricaDeCartas.CriarPocaoDeCura(), 
        FabricaDeCartas.CriarFogoMagico(), FabricaDeCartas.CriarFogoMagico(),
        FabricaDeCartas.CriarFeiticoDeGelo(),FabricaDeCartas.CriarLivroDeFeiços(),
    };
        }
    }
}