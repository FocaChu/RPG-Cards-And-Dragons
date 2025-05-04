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

        public override Bioma BiomaDeOrigem => Bioma.Floresta;

        public override bool EBoss => false;

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
        public override int CooldownHabilidade => 4; // a cada 4 rodadas usa habilidade

        public override void Atacar(Batalha batalha, OInimigo self)
        {
            int DanoFinal = this.DanoBase + self.ModificadorDano;

            var alvo = AlvoController.EscolherAlvoAleatorioDosAliados(batalha);

            TextoController.CentralizarTexto($"{this.Nome} atacou {alvo.Nome} causando {DanoFinal} de dano!");
            alvo.SofrerDano(DanoFinal, false);
        }
        public override bool PodeUsarHabilidade(int rodadaAtual)
        {
            return rodadaAtual % CooldownHabilidade == 0;
        }
        public override void UsarHabilidade(Batalha batalha, OInimigo self)
        {
            int DanoFinal = this.DanoBase + self.ModificadorDano;

            var alvo = AlvoController.EscolherAlvoAleatorioDosAliados(batalha);

            TextoController.CentralizarTexto($"{this.Nome} mordeu e causou {DanoFinal} de dano em {alvo.Nome}!");
            alvo.SofrerDano(DanoFinal, false);
            CondicaoController.AplicarOuAtualizarCondicao(new Sangramento(2, 2), alvo.Condicoes);
        }

        public override void AtacarComoAliado(Batalha batalha, InimigoRevivido self)
        {
            int DanoFinal = this.DanoBase + self.ModificadorDano;
                
            var alvo = AlvoController.EscolherInimigoAleatorio(batalha.Inimigos);

            alvo.SofrerDano(DanoFinal, false);
            TextoController.CentralizarTexto($"{this.Nome} atacou {alvo.Nome} causando dano!");
        }

        public override void UsarHabilidadeComoAliado(Batalha batalha, InimigoRevivido self)
        {
            int DanoFinal = this.DanoBase + self.ModificadorDano;

            var alvo = AlvoController.EscolherInimigoAleatorio(batalha.Inimigos);

            TextoController.CentralizarTexto($"{this.Nome} mordeu e causou dano em {alvo.Nome}!");
            alvo.SofrerDano(DanoFinal, false);

            CondicaoController.AplicarOuAtualizarCondicao(new Sangramento(2, 2), alvo.Condicoes);
        }
    }
}