using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.ClassesCondicoes;
using CardsAndDragonsJogo;

namespace CardsAndDragons
{
    public enum TipoCriatura
    {
        Jogador,
        Inimigo,
        InimigoRevivido,
        Pet,
        CriaturaInvocada,
        Robo
    }
    public interface ICriaturaCombatente
    {
        string Nome { get; }

        TipoCriatura Tipo { get; }

        int VidaAtual { get; }

        int VidaMax { get; }

        List<string> Modelo { get; }

        int Escudo { get; set; }

        int ModificadorDefesa { get; set; }

        int ModificadorDano { get; set; }

        List<ICondicaoTemporaria> Condicoes { get; }

        void SofrerDano(ICriaturaCombatente agressor, int quantidade, bool foiCondicao, bool aoSofrerDano);

        void AoSofrerDano(ICriaturaCombatente agressor, int quantidade);

        void AoMorrer(Batalha batalha, ICriaturaCombatente agressor);

        void Curar(int quantidade);

        void RealizarTurno(Batalha batalha);
    }

}
