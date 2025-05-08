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

        public abstract List<string> Modelo { get; }

        public abstract int RecargaHabilidade { get; }

        //Mecanicas para combate

        public abstract void Atacar(Batalha batalha, OInimigo self, ICriaturaCombatente alvo);

        public abstract void UsarHabilidade(Batalha batalha, OInimigo self, ICriaturaCombatente alvo);

    }
}
