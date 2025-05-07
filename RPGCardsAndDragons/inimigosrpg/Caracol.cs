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

        public override Bioma BiomaDeOrigem => Bioma.Floresta;

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

        public override int CooldownHabilidade => 3; // a cada 3 rodadas usa habilidade

        public override void Atacar(Batalha batalha, OInimigo self, int nivelParanoia)
        {
            int DanoFinal = this.DanoBase + self.ModificadorDano;

            ICriaturaCombatente alvo;

            if (nivelParanoia > 0)
            {
                int chance = nivelParanoia * 10;

                int opcao = BatalhaController.GerarRNG(chance);

                if (opcao == 0)
                {
                    alvo = AlvoController.EscolherInimigoAleatorio(batalha.Inimigos);
                    TextoController.CentralizarTexto($"{this.Nome} está em panico e confuso!");
                }
                else alvo = AlvoController.EscolherAlvoAleatorioDosAliados(batalha);
            }
            else
            {
                alvo = AlvoController.EscolherAlvoAleatorioDosAliados(batalha);

            }

            TextoController.CentralizarTexto($"{this.Nome} atacou {alvo.Nome} causando dano!");
            alvo.SofrerDano(DanoFinal, false);
        }

        public override bool PodeUsarHabilidade(int rodadaAtual)
        {
            return rodadaAtual % CooldownHabilidade == 0;
        }

        public override void UsarHabilidade(Batalha batalha, OInimigo self, int nivelParanoia)
        {
            int DanoFinal = (this.DanoBase * 2) + self.ModificadorDano;

            ICriaturaCombatente alvo;

            if (nivelParanoia > 0)
            {
                int chance = nivelParanoia * 10;

                int opcao = BatalhaController.GerarRNG(chance);

                if (opcao == 0)
                {
                    alvo = AlvoController.EscolherInimigoAleatorio(batalha.Inimigos);
                    TextoController.CentralizarTexto($"{this.Nome} está em panico e confudiu {alvo.Nome} com uma ameaça!");
                }
                else alvo = AlvoController.EscolherAlvoAleatorioDosAliados(batalha);
            }
            else
            {
                alvo = AlvoController.EscolherAlvoAleatorioDosAliados(batalha);

            }

            TextoController.CentralizarTexto($"{this.Nome} atacou {alvo.Nome} causando dano toxico!");
            alvo.SofrerDano(DanoFinal, false);
            CondicaoController.AplicarOuAtualizarCondicao(new Veneno(4, 2), alvo.Condicoes);
        }

        public override void AtacarComoAliado(Batalha batalha, InimigoRevivido self)
        {
            int DanoFinal = this.DanoBase + self.ModificadorDano;

            var alvo = AlvoController.EscolherInimigoAleatorio(batalha.Inimigos);

            TextoController.CentralizarTexto($"{this.Nome} atacou {alvo.Nome} causando dano!");

            alvo.SofrerDano(DanoFinal, false);
        }

        public override void UsarHabilidadeComoAliado(Batalha batalha, InimigoRevivido self)
        {
            int DanoFinal = (this.DanoBase * 2) + self.ModificadorDano;

            var alvo = AlvoController.EscolherInimigoAleatorio(batalha.Inimigos);

            TextoController.CentralizarTexto($"{this.Nome} atacou {alvo.Nome} causando dano toxico!");

            alvo.SofrerDano(DanoFinal, false);

            CondicaoController.AplicarOuAtualizarCondicao(new Veneno(4, 2), alvo.Condicoes);
        }


    }
}
