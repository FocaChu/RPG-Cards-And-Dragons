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
    public class DragaoMagro : InimigoRPG
    {
        public override int VidaMax => 200;

        public int VidaAtual { get; set; }

        public override int DanoBase => 25;

        public override bool EBoss => true;

        public override int Dificuldade => 2;

        public override string Nome => "Dragão Lagarto";

        public override List<string> Modelo => new List<string>()
        {
            //12345678901234567890123456789012345678901234 = 44
            @"        ___===-_.  (    ) .___-===__        ", //1 
            @"      -########//  |\^^/|  \\########-      ", //2 
            @"   _/#########//   (@::@)   \\#########\_   ", //3 
            @"  /#########((      \\//    ))###########\  ", //4
            @" |############\\   _(oo)_   //############| ", //5
            @" |##############\\/      \//##############| ", //6
            @" |#######/\######(        )######/\#######| ", //7
            @" |#/\#/\/  \#/\##\   /\   /##/\#/  \/\#/\#| ", //8
            @" |/  V  `   V  \#\| |  | |/#/  V   '  V  \| ", //9
            @"     `      `   / | |  | | \   '      '   ' ", //10
            @"               (  | |  | |  )               ", //11
            @"              __\ | |  | | /__              ", //12
            @"             (vvv(VVV)(VVV)vvv)             ", //13
             // preencher 10 linhas no total
        };

        /*
         
       ___===-_.  (    ) .___-===__
     -########//  |\^^/|  \\########-
  _/#########//   (@::@)   \\#########\_
 /#########((      \\//    ))###########\
|############\\   _(oo)_   //############|
|##############\\/      \//##############|
|#######/\######(        )######/\#######|
|#/\#/\/  \#/\##\   /\   /##/\#/  \/\#/\#|
|/  V  `   V  \#\| |  | |/#/  V   '  V  \|
    `      `   / | |  | | \   '      '   '   
              (  | |  | |  )
             __\ | |  | | /__
            (vvv(VVV)(VVV)vvv)
        
        */

        public override int RecargaHabilidade => 5; // a cada 2 rodadas usa habilidada


        public override void Atacar(Batalha batalha, ICriaturaCombatente self, ICriaturaCombatente alvo)
        {
            int danoFinal = this.DanoBase + self.ModificadorDano;



            TextoController.CentralizarTexto($"{this.Nome} investiu contra {alvo.Nome} causando dano!");
            alvo.SofrerDano(self, danoFinal, false, true);
        }

        public override void UsarHabilidade(Batalha batalha, ICriaturaCombatente self, ICriaturaCombatente alvo)
        {
            //faz o rng do golpe do boss
            int chance = 50;


            int opcaoGolpe = BatalhaController.GerarRNG(chance);

            if (opcaoGolpe == 1)
            {
                int danoFinal = (this.DanoBase * 2) + self.ModificadorDano;

                TextoController.CentralizarTexto($"{this.Nome} atacou {alvo.Nome} com suas garras");

                alvo.Escudo = alvo.Escudo / 2;

                alvo.ModificadorDefesa = alvo.ModificadorDefesa / 2;

                alvo.SofrerDano(self, danoFinal, false, true);
                CondicaoController.AplicarOuAtualizarCondicao(new Sangramento(3, 3), alvo.Condicoes);
            }
            else
            {
                int danoFinal = (this.DanoBase * 2) + self.ModificadorDano;

                TextoController.CentralizarTexto($"{this.Nome} atacou {alvo.Nome} mordendo com suas presas");

                this.VidaAtual += 10;

                alvo.SofrerDano(self, danoFinal, false, true);
                CondicaoController.AplicarOuAtualizarCondicao(new Veneno(3, 3), alvo.Condicoes);
            }
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

