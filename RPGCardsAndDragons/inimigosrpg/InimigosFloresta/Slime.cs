using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.Aliados;
using CardsAndDragons.ClassesCondicoes;
using CardsAndDragons.Controllers;
using CardsAndDragons.Inimigos;
using CardsAndDragonsJogo;

namespace CardsAndDragons
{
    public class Slime : InimigoRPG
    {
        public override int VidaMax => 50;

        public override int DanoBase => 5;

        public override bool EBoss => false;

        public override int Dificuldade => 1;

        public override string Nome => "Slime";

        public override List<string> Modelo => new List<string>()
        {
            //1234567890123456789012345 = 25
            @"                         ", //1
            @"                         ", //2
            @"                         ", //3
            @"        XXXXXXXXX        ", //4
            @"      XXX       XXX      ", //5
            @"     XX           XX     ", //6
            @"     X  __     __  X     ", //7
            @"     X    ■   ■    X     ", //8
            @"     XX           XX     ", //9
            @"       XXXXXXXXXXX       ", //10
            // preencher 10 linhas no total
        };

        public override int RecargaHabilidade => 4; // a cada 4 rodadas usa habilidade


        public override void Atacar(Batalha batalha, ICriaturaCombatente self, ICriaturaCombatente alvo)
        {
            int danoFinal = this.DanoBase + self.ModificadorDano;

            TextoController.CentralizarTexto($"{self.Nome} atacou {alvo.Nome} causando dano!");
            alvo.SofrerDano(self, danoFinal, false, true);
        }

        public override void UsarHabilidade(Batalha batalha, ICriaturaCombatente self, ICriaturaCombatente alvo)
        {
            int danoFinal = this.DanoBase + self.ModificadorDano;

            TextoController.CentralizarTexto($"{this.Nome} atacou {alvo.Nome} com gosma toxica!");
            alvo.SofrerDano(self, danoFinal, false, true);
            CondicaoController.AplicarOuAtualizarCondicao(new Veneno(5, 2), alvo.Condicoes);
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
