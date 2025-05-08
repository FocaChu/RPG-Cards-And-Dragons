using System.Collections.Generic;
using CardsAndDragons.Aliados;
using CardsAndDragonsJogo;

namespace CardsAndDragons.Inimigos
{
    public abstract class InimigoRPG
    {
        public abstract int VidaMax { get; }

        public abstract int DanoBase { get; }

        public abstract string Nome { get; }

        public abstract bool EBoss { get; }

        public abstract int Dificuldade { get; }

        public abstract List<string> Modelo { get; }

        public abstract int RecargaHabilidade { get; }

        //Mecanicas para combate

        public abstract void Atacar(Batalha batalha, ICriaturaCombatente self, ICriaturaCombatente alvo);

        public abstract void UsarHabilidade(Batalha batalha, ICriaturaCombatente self, ICriaturaCombatente alvo);

        public abstract void AoSofrerDano(ICriaturaCombatente agressor, int quantidade);

        public abstract void AoMorrer(Batalha batalha, ICriaturaCombatente self, ICriaturaCombatente alvo);

    }
}
