using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons;

namespace CardsAndDragonsJogo
{
    //Enum pra definir a raridade de cada carta
    public enum Raridade
    {
        Comum = 0,
        Rara = 1,
        Lendaria = 2,
        Profana = 3,
    }

    public enum TipoCarta
    {
        Generica = 0,
        Robo = 1,
        Recarregavel = 2,
        Doenca = 3,
        Evolutiva = 4,
    }

    //Interface que define todas as cartas do jogo. Desde de genericas até futuras cartas mais especiais
    public interface ICartaUsavel
    {
        string Nome { get; }

        string Descricao { get; }

        Raridade RaridadeCarta { get; }

        TipoCarta TipoCarta { get; }

        int Preco { get; }

        int CustoVida { get; }

        int CustoMana { get; }

        int CustoStamina { get; }

        int CustoOuro { get; }

        List<string> Modelo { get; }

        bool Usar(Batalha batalha);
    }

}
