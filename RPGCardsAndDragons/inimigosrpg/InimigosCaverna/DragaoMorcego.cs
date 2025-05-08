using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.Aliados;
using CardsAndDragons.ClassesClasses;
using CardsAndDragons.ClassesCondicoes;
using CardsAndDragons.Condicoes;
using CardsAndDragons.Controllers;
using CardsAndDragons.Inimigos;
using CardsAndDragonsJogo;

namespace CardsAndDragons
{
    public class DragaoMorcego : InimigoRPG
    {
        public override int VidaMax => 270;

        public int VidaAtual { get; set; }

        public override int DanoBase => 20;

        public override bool EBoss => true;

        public override int Dificuldade => 3;

        public override string Nome => "Dragão Morcego";

        public override List<string> Modelo => new List<string>()
        {
            //12345678901234567890123456789012345678901234 = 44
            @"                                     ", //1 
            @"                                     ", //2 
            @"                                     ", //3 
            @"                                     ", //4
            @"                                     ", //5
            @"                                     ", //6
            @"     sou um dragão morcego           ", //7
            @"                                     ", //8
            @"                                     ", //9
            @"                                     ", //10
            @"                                     ", //11
            @"                                     ", //12
            @"                                     ", //13
             // preencher 10 linhas no total
        };

        /*
         

        
        */

        public override int RecargaHabilidade => 4; // a cada 5 rodadas usa habilidada


        public override void Atacar(Batalha batalha, ICriaturaCombatente self, ICriaturaCombatente alvo)
        {
            int danoFinal = this.DanoBase + self.ModificadorDano;


            TextoController.CentralizarTexto($"{this.Nome} investiu contra {alvo.Nome} causando dano!");
            alvo.SofrerDano(self, danoFinal, false, true);
        }

        public override void UsarHabilidade(Batalha batalha, ICriaturaCombatente self, ICriaturaCombatente alvo)
        {
            Random rng = new Random();

            //faz o rng do golpe do boss

            int chance = 50;

            int opcaoGolpe = BatalhaController.GerarRNG(chance);

            if (opcaoGolpe == 1)
            {
                int danoFinal = (this.DanoBase * 2) + self.ModificadorDano;

                if (alvo is Personagem jogador)
                {
                    TextoController.CentralizarTexto($"{this.Nome} atacou {batalha.Jogador.Nome} com sua suas presas e bebeu seu sangue!\n");

                    int staminaTotal = jogador.StaminaAtual;
                    jogador.StaminaAtual -= 10;
                    int staminaPerdida = staminaTotal - 10;

                    TextoController.CentralizarTexto($"{batalha.Jogador.Nome} perdeu {staminaPerdida} de sua stamina e está sangrando!");

                    alvo.SofrerDano(self, danoFinal, false, true);
                    CondicaoController.AplicarOuAtualizarCondicao(new Sangramento(5, 2), batalha.Jogador.Condicoes);
                }
                else
                {
                    TextoController.CentralizarTexto($"{this.Nome} atacou {batalha.Jogador.Nome} com sua suas presas e bebeu seu sangue!\n");
                    alvo.SofrerDano(self, danoFinal, false, true);
                    CondicaoController.AplicarOuAtualizarCondicao(new Sangramento(5, 2), batalha.Jogador.Condicoes);
                }

            }
            else
            {
                TextoController.CentralizarTexto($"{this.Nome} atacou {alvo.Nome} dando um talho");

                int danoFinal = (this.DanoBase + self.ModificadorDano) * 2;

                alvo.SofrerDano(self, danoFinal, false, true);

                int critou = BatalhaController.GerarRNG(20);

                if (critou == 0)
                {
                    TextoController.CentralizarTexto($"{alvo.Nome} teve suas defesas danificadas");
                    CondicaoController.AplicarOuAtualizarCondicao(new ModificacaoDefesa(-3, 2), batalha.Jogador.Condicoes);
                }
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
