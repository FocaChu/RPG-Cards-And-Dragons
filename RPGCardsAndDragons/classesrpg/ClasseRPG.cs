﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragonsJogo;

namespace CardsAndDragons
{

    // Classe base para todas as ClassesRPG !!!
    public abstract class ClasseRPG
    {
        public abstract int VidaMax { get; }

        public abstract int ManaMax { get; }

        public abstract int StaminaMax { get; }

        public abstract string NomeClasse { get; }

        public abstract string DescricaoClasse { get; }

        //Essa função vai ser chamada no cosntrutor do personagem. Toda classe que herda de classeRPG precisa ter ele preenchido com as cartas iniciasi dela
        public abstract List<ICartaUsavel> DefinirCartasIniciais();
    }

    /* 
        Isso aqui basicamente faz com que os objetos(as classes) que são derivadas(que herdam da classe base)
        sejam tratadas como a classe base(a classe ClasseRPG)

        Isso faz com que a gente crie uma classe base e atraves do construtor coloque dentro dela uma classe derivada
        ex: "ClasseRPG classeExemplo = new Mago();"
        isso aqui está criando uma ClasseRPG, só que com as informações de um Mago
     
        Isso permite que o Personagem use qualquer ClasseRPG sem saber se é Guerreiro, Mago ou qualquer outra classe
        Deixando mais generico
     */

}
