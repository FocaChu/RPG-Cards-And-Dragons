﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.Aliados;
using CardsAndDragons.Controllers;
using CardsAndDragons.Inimigos;
using CardsAndDragonsJogo;

namespace CardsAndDragons
{
    public class Esqueleto : InimigoRPG
    {
        public override int VidaMax => 30;

        public override int DanoBase => 10;

        public override bool EBoss => false;

        public override int Dificuldade => 1;

        public override string Nome => "Esqueleto";

        public override List<string> Modelo => new List<string>()
        {
            //1234567890123456789012345 = 25
            @"           ___           ", //1 
            @"          (o.o)          ", //2
            @"          _|=|_          ", //3
            @"        / .=|=. \        ", //4
            @"        \ .=|=. /        ", //5
            @"        (:(_=_):)        ", //6
            @"          || ||          ", //7
            @"          () ()          ", //8
            @"          || ||          ", //9
            @"         ==' '==         ", //10
             // preencher 10 linhas no total
        };

        public override int RecargaHabilidade => 3; // a cada 3 rodadas usa habilidade

        public override void Atacar(Batalha batalha, ICriaturaCombatente self, ICriaturaCombatente alvo)
        {
            int danoFinal = this.DanoBase + self.ModificadorDano;


            TextoController.CentralizarTexto($"{this.Nome} atacou {alvo.Nome} causando dano!");
            alvo.SofrerDano(self, danoFinal, false, true);
        }

        public override void UsarHabilidade(Batalha batalha, ICriaturaCombatente self, ICriaturaCombatente alvo)
        {
            int danoFinal = (this.DanoBase * 2) + self.ModificadorDano;


            TextoController.CentralizarTexto($"{this.Nome} atacou {alvo.Nome} causando dano critico!");
            alvo.SofrerDano(self, danoFinal, false, true);
        }

        public override void AoSofrerDano(ICriaturaCombatente agressor, int quantidade)
        {
            return;
        }

        public override void AoMorrer(Batalha batalha, ICriaturaCombatente self, ICriaturaCombatente alvo)
        {
            return;
        }
    }
}
