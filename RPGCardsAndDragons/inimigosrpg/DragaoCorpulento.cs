using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.Aliados;
using CardsAndDragons.ClassesClasses;
using CardsAndDragons.ClassesCondicoes;
using CardsAndDragons.Controllers;
using CardsAndDragons.Inimigos;
using CardsAndDragonsJogo;

namespace CardsAndDragons
{
    public class DragaoCompulento : InimigoRPG
    {
        public override int VidaMax => 250;

        public int VidaAtual { get; set; }

        public override int DanoBase => 20;

        public override Bioma BiomaDeOrigem => Bioma.Floresta;

        public override bool EBoss => true;

        public override string Nome => "Dragão Corpulento";

        public override List<string> Modelo => new List<string>()
        {
            //12345678901234567890123456789012345678901234 = 44
            @"     / {      /|\}!{/|\      } \     ", //1 
            @"     } }\.   ( (.¨^¨.) )   ./{ {     ", //2 
            @"    / {   \.  (d\   /b)  ./   } \    ", //3 
            @"    } }     \_|\~   ~/|_/    { {     ", //4
            @"   / /        | )   ( |        \ \   ", //5
            @"  { {        _)(,   ,)(_        } }  ", //6
            @"   } }      //  `';'`  \\      { {   ", //7
            @"  / /      //     (     \\      \ \  ", //8
            @" { {      {(     -=)     )}      } } ", //9
            @"   \\   /'/    /-=|\-\    \`\   //'  ", //10
            @"     `\{  |   ( -===- )   |  }/'     ", //11
            @"       `  _\   \-===-/   /_  '       ", //12
            @"         (_(_(_)'-=-'(_)_)_)         ", //13
             // preencher 10 linhas no total
        };

        /*
         

    / {      /|\}!{/|\      } \
    } }     ( (."^".) )     { {
   / {       (d\   /b)       } \
   } }       |\~   ~/|       { {
  / /        | )   ( |        \ \
 { {        _)(,   ,)(_        } }
  } }      //  `";"`  \\      { {
 / /      //     (     \\      \ \
{ {      {(     -=)     )}      } }
  \\   /'/    /-=|\-\    \`\   //'
    `\{  |   ( -===- )   |  }/'
      `  _\   \-===-/   /_  '
        (_(_(_)'-=-'(_)_)_)
        
        */

        public override int CooldownHabilidade => 4; // a cada 2 rodadas usa habilidada


        public override void Atacar(Batalha batalha, OInimigo self)
        {
            int danoFinal = this.DanoBase + self.ModificadorDano;

            var alvo = AlvoController.EscolherAlvoAleatorioDosAliados(batalha);

            TextoController.CentralizarTexto($"{this.Nome} investiu contra {alvo.Nome} causando dano!");
            alvo.SofrerDano(danoFinal, false);
        }

        public override bool PodeUsarHabilidade(int rodadaAtual)
        {
            return rodadaAtual % CooldownHabilidade == 0;
        }

        public override void UsarHabilidade(Batalha batalha, OInimigo self)
        {
            Random rng = new Random();

            //faz o rng do golpe do boss

            int chance = 50;

            int opcaoGolpe = BatalhaController.GerarRNG(chance);

            if (opcaoGolpe == 1)
            {
                int danoFinal = (this.DanoBase * 2) + self.ModificadorDano;

                TextoController.CentralizarTexto($"{this.Nome} atacou {batalha.Jogador.Nome} com sua calda!\n");

                int indice = rng.Next(batalha.Jogador.Mao.Count);

                var carta = batalha.Jogador.Mao[indice];

                batalha.Jogador.BaralhoDescarte.Add(carta);
                batalha.Jogador.Mao.RemoveAt(indice);

                TextoController.CentralizarTexto($"{batalha.Jogador.Nome} perdeu a carta {carta.Nome} de sua mão!");

                batalha.Jogador.SofrerDano(danoFinal, false);
            }
            else
            {
                var alvo = AlvoController.EscolherAlvoAleatorioDosAliados(batalha);

                TextoController.CentralizarTexto($"{this.Nome} atacou {alvo.Nome} mordendo com suas presas");

                int chanceSucesso = rng.Next(100);

                int danoFinal = (chance < 50) ? this.DanoBase : this.DanoBase * 2;

                danoFinal += self.ModificadorDano;

                alvo.SofrerDano(danoFinal, false);
            }

        }

        public override void AtacarComoAliado(Batalha batalha, InimigoRevivido self)
        {
            throw new NotImplementedException();
        }

        public override void UsarHabilidadeComoAliado(Batalha batalha, InimigoRevivido self)
        {
            throw new NotImplementedException();
        }
    }
}
