using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.Aliados;
using CardsAndDragons.ClassesCondicoes;
using CardsAndDragons.Condicoes;
using CardsAndDragons.Controllers;
using CardsAndDragons.Inimigos;
using CardsAndDragonsJogo;

namespace CardsAndDragons
{
    public class ToupeiraGrande : InimigoRPG
    {
        public override int VidaMax => 80;

        public override int DanoBase => 10;

        public override bool EBoss => false;

        public override int Dificuldade => 2;

        public override string Nome => "Toupeira Grande";

        public override List<string> Modelo => new List<string>()
        {
            //1234567890123456789012345 = 25
            @"                         ", //1
            @"                         ", //2
            @"                         ", //3
            @"                         ", //4
            @"        sou uma          ", //5
            @"       toupeira          ", //6
            @"                         ", //7
            @"                         ", //8
            @"                         ", //9
            @"                         ", //10
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

            TextoController.CentralizarTexto($"{this.Nome} atacou {alvo.Nome} com suas garras!");
            alvo.SofrerDano(self, danoFinal, false, true);
            CondicaoController.AplicarOuAtualizarCondicao(new ModificacaoDefesa(-2, 2), alvo.Condicoes);
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
