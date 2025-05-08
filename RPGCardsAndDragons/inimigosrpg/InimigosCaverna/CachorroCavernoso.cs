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
    public class CachorroCavernoso : InimigoRPG
    {
        public override int VidaMax => 75;

        public override int DanoBase => 15;

        public override bool EBoss => false;

        public override int Dificuldade => 2;

        public override string Nome => "Cachorro Cavernoso";

        public override List<string> Modelo => new List<string>()
        {
            //1234567890123456789012345 = 25
            @"                         ", //1
            @"                         ", //2
            @"                         ", //3
            @"                         ", //4
            @"                         ", //5
            @"         sou um          ", //6
            @"      cachorro           ", //7
            @"                         ", //8
            @"                         ", //9
            @"                         ", //10
            // preencher 10 linhas no total
        };

        public override int RecargaHabilidade => 2; // a cada 4 rodadas usa habilidade

        public override void Atacar(Batalha batalha, ICriaturaCombatente self, ICriaturaCombatente alvo)
        {
            int danoFinal = this.DanoBase + self.ModificadorDano;

            TextoController.CentralizarTexto($"{self.Nome} atacou {alvo.Nome} causando dano!");
            alvo.SofrerDano(self, danoFinal, false, true);
        }

        public override void UsarHabilidade(Batalha batalha, ICriaturaCombatente self, ICriaturaCombatente alvo)
        {
            int danoFinal = this.DanoBase + self.ModificadorDano;

            TextoController.CentralizarTexto($"{this.Nome} mordeu {alvo.Nome} e fica aagitado!");
            alvo.SofrerDano(self, danoFinal, false, true);
            CondicaoController.AplicarOuAtualizarCondicao(new Sangramento(2, 2), alvo.Condicoes);
            CondicaoController.AplicarOuAtualizarCondicao(new ModificacaoDano(2, 2), self.Condicoes);
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
