using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.Inimigos;

namespace RPGCardsAndDragons.fases
{

    public abstract class BiomaJogo
    {

        public abstract string Nome { get; }

        public abstract string Descricao { get; }

        public abstract int Dificuldade { get; }

        public abstract List<string> DefinirModelo();

        public abstract List<InimigoRPG> DefinirInimigos();

        public abstract List<InimigoRPG> DefinirChefes();

    }
}
