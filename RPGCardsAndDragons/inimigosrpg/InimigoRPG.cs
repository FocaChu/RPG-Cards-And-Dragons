using System.Collections.Generic;
using CardsAndDragons.Aliados;
using CardsAndDragonsJogo;

namespace CardsAndDragons.Inimigos
{
    public enum Bioma
    {
        Floresta = 1,
        Caverna = 2,
    }

    public abstract class InimigoRPG
    {
        public abstract int VidaMax { get; }

        public abstract int DanoBase { get; }

        public abstract string Nome { get; }

        public abstract Bioma BiomaDeOrigem { get; }

        public abstract bool EBoss { get; }

        public abstract List<string> Modelo { get; }

        public abstract int CooldownHabilidade { get; }

        //Mecanicas para combate

        public abstract bool PodeUsarHabilidade(int rodadaAtual);

        public abstract void Atacar(Batalha batalha, OInimigo self);

        public abstract void UsarHabilidade(Batalha batalha, OInimigo self);

        public abstract void AtacarComoAliado(Batalha batalha, InimigoRevivido self);

        public abstract void UsarHabilidadeComoAliado(Batalha batalha, InimigoRevivido self);

    }
}
