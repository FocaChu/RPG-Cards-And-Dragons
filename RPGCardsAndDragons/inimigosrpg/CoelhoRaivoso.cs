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
    public class CoelhoRaivoso : InimigoRPG
    {
        public override int VidaMax => 40;

        public int VidaAtual { get; set; }

        public override int DanoBase => 5;

        public override Bioma BiomaDeOrigem => Bioma.Floresta;

        public override bool EBoss => false;

        public override string Nome => "Coelho Raivoso";

        public override List<string> Modelo => new List<string>()
        {
            //1234567890123456789012345 = 25
            @"                         ", //1 
            @"                         ", //2
            @"                         ", //3
            @"           ((`\          ", //4
            @"            \\ \         ", //5
            @"         ___ \\ '--._    ", //6
            @"       .'`   `'    o  )  ", //7
            @"      /    \   '. __.'   ", //8
            @"     _|    /_  \ \_\_    ", //9
            @"    {_\______\-'\__\_\   ", //10
             // preencher 10 linhas no total
        };

        public override int CooldownHabilidade => 2; // a cada 2 rodadas usa habilidade

        public int DanoRaivoso;

        public override void Atacar(Batalha batalha, OInimigo self)
        {
            int DanoFinal = this.DanoBase + self.ModificadorDano;

            var alvo = AlvoController.EscolherAlvoAleatorioDosAliados(batalha);

            TextoController.CentralizarTexto($"{this.Nome} atacou {alvo.Nome} causando dano!");
            alvo.SofrerDano(DanoFinal, false);
        }

        public override bool PodeUsarHabilidade(int rodadaAtual)
        {
            return rodadaAtual % CooldownHabilidade == 0;
        }

        public override void UsarHabilidade(Batalha batalha, OInimigo self)
        {
            int DanoFinal = (this.DanoBase * 2) + self.ModificadorDano + this.DanoRaivoso;

            var alvo = AlvoController.EscolherAlvoAleatorioDosAliados(batalha);

            TextoController.CentralizarTexto($"{this.Nome} atacou {alvo.Nome} com raiva!");
            alvo.SofrerDano(DanoFinal, false);
            if (this.DanoRaivoso > 5)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                TextoController.CentralizarTexto($"Coelho Assassino está com sede de sangue!");
                CondicaoController.AplicarOuAtualizarCondicao(new Sangramento(2, 1), alvo.Condicoes);
                Console.ResetColor();

            }
            this.DanoRaivoso++;
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
            int DanoFinal = (this.DanoBase * 2) + self.ModificadorDano + this.DanoRaivoso;

            var alvo = AlvoController.EscolherInimigoAleatorio(batalha.Inimigos);

            TextoController.CentralizarTexto($"{this.Nome} atacou {alvo.Nome} com raiva!\n");

            alvo.SofrerDano(DanoFinal, false);

            if (this.DanoRaivoso > 5)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                TextoController.CentralizarTexto($"Coelho Assassino está com sede de sangue!\n");
                CondicaoController.AplicarOuAtualizarCondicao(new Sangramento(2, 1), alvo.Condicoes);
                Console.ResetColor();

            }
            this.DanoRaivoso++;
        }
    }
}
