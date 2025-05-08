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
    public class Caracol : InimigoRPG
    {
        public override int VidaMax => 60;

        public override int DanoBase => 10;

        public override bool EBoss => false;

        public override string Nome => "Caracol";

        public override List<string> Modelo => new List<string>()
        {
            //1234567890123456789012345 = 25
            @"                         ", //1 
            @"                         ", //2
            @"                         ", //3
            @"                         ", //4
            @"                         ", //5
            @"        .----.   @   @   ", //6
            @"       / .---.`.  \v/    ", //7
            @"      | | '\ \ \_/  )    ", //8
            @"     ,-\ `-.' /.'  /     ", //9
            @"    '---`----'----'      ", //10
             // preencher 10 linhas no total
        };

        /*
        
    .----.   @   @
   / .-"-.`.  \v/
   | | '\ \ \_/ )
 ,-\ `-.' /.'  /
'---`----'----'
        
        */

        public override int RecargaHabilidade => 3; // a cada 3 rodadas usa habilidade

        public override void Atacar(Batalha batalha, OInimigo self, ICriaturaCombatente alvo)
        {
            int DanoFinal = this.DanoBase + self.ModificadorDano;

            TextoController.CentralizarTexto($"{this.Nome} atacou {alvo.Nome} causando dano!");
            alvo.SofrerDano(DanoFinal, false);
        }

        public override void UsarHabilidade(Batalha batalha, OInimigo self, ICriaturaCombatente alvo)
        {
            int DanoFinal = (this.DanoBase * 2) + self.ModificadorDano;

            TextoController.CentralizarTexto($"{this.Nome} atacou {alvo.Nome} causando dano toxico!");
            alvo.SofrerDano(DanoFinal, false);
            CondicaoController.AplicarOuAtualizarCondicao(new Veneno(4, 2), alvo.Condicoes);
        }
    }
}
