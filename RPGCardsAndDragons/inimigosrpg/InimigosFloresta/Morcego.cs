using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.Aliados;
using CardsAndDragons.ClassesCondicoes;
using CardsAndDragons.Controllers;
using CardsAndDragons.Inimigos;
using CardsAndDragonsJogo;

namespace CardsAndDragons
{
    public class Morcego : InimigoRPG
    {
        public override int VidaMax => 50;

        public override int DanoBase => 10;

        public override bool EBoss => false;

        public override int Dificuldade => 1;

        public override string Nome => "Morcego";

        public override List<string> Modelo => new List<string>()
        {

            @"                         ", //1 
            @"      /\         /\      ", //2
            @"     /  \ (\_/) /  \     ", //3
            @"    / .''.(o.o).''. \    ", //4
            @"   /.' _/ / * \ \_ '.\   ", //5
            @"  /` .' `\\___//´ '. ´\  ", //6
            @"  \.-'   \(/ \)/   '-./  ", //7
            @"   \      ¨   ¨      /   ", //8
            @"                         ", //9
            @"                         ", //10

        // preencher 10 linhas no total
        };
        public override int RecargaHabilidade => 4; // a cada 4 rodadas usa habilidade

        public override void Atacar(Batalha batalha, ICriaturaCombatente self, ICriaturaCombatente alvo)
        {
            int danoFinal = this.DanoBase + self.ModificadorDano;


            TextoController.CentralizarTexto($"{this.Nome} atacou {alvo.Nome} causando de dano!");
            alvo.SofrerDano(self, danoFinal, false, true);
        }

        public override void UsarHabilidade(Batalha batalha, ICriaturaCombatente self, ICriaturaCombatente alvo)
        {
            int danoFinal = this.DanoBase + self.ModificadorDano;

            TextoController.CentralizarTexto($"{this.Nome} mordeu causando dano em {alvo.Nome}!");
            alvo.SofrerDano(self, danoFinal, false, true);
            CondicaoController.AplicarOuAtualizarCondicao(new Sangramento(2, 2), alvo.Condicoes);
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