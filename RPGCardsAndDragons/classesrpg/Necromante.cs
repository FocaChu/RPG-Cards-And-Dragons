using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.ClassesDasCartas;
using CardsAndDragonsJogo;

namespace CardsAndDragons.ClassesClasses
{
    public class Necromante : ClasseRPG
    {
        public override int VidaMax => 120;

        public override int ManaMax => 110;

        public override int StaminaMax => 70;

        public override string NomeClasse => "Necromante";

        public override string DescricaoClasse => "Pode trazer inimigos mortos de volta a vida como aliados.";

        //aqui ele sobrepoem o metodo herdado de ClasseRPG que vai ser chamado no construtor do personagem. Dentro desse metodo da pra configurar todas as cartas iniciais.
        //que a classe vai ter, basta chamar a fabrica de cartas e a função com a carta.
        public override List<ICartaUsavel> DefinirCartasIniciais()
        {
            return new List<ICartaUsavel>
    {
        FabricaDeCartas.CriarSortearDestino(), FabricaDeCartas.CriarSortearDestino(), FabricaDeCartas.CriarSortearDestino(), FabricaDeCartas.CriarSortearDestino(),
        FabricaDeCartas.CriarPocaoDeCura(),
        FabricaDeCartas.CriarRessureicao(), FabricaDeCartas.CriarRessureicao(),
        FabricaDeCartas.CriarCuidadosPosMortem(), FabricaDeCartas.CriarSacrificarServo()
    };
        }
    }
}