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
    public class DragaoLagarto : InimigoRPG
    {
        public override int VidaMax => 325;

        public int VidaAtual { get; set; }

        public override int DanoBase => 30;

        public override bool EBoss => true;

        public override int Dificuldade => 3;

        public override string Nome => "Dragão Lagarto";

        public override List<string> Modelo => new List<string>()
        {
            //12345678901234567890123456789012345678901234 = 44
            @"                                     ", //1 
            @"                                     ", //2 
            @"                                     ", //3 
            @"                                     ", //4
            @"                                     ", //5
            @"                                     ", //6
            @"     sou um dragão lagarto           ", //7
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

        public override int RecargaHabilidade => 5; // a cada 5 rodadas usa habilidada


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

                TextoController.CentralizarTexto($"{this.Nome}  colide com {alvo.Nome} usando suas escamas");
                alvo.SofrerDano(self, danoFinal, false, true);

                self.Escudo += 10;
            }
            else
            {
                TextoController.CentralizarTexto($"{this.Nome}  bate com força no chão e uma estalactite acerta{alvo.Nome}");

                int danoFinal = 30;

                alvo.SofrerDano(self, danoFinal, false, false);

                CondicaoController.AplicarOuAtualizarCondicao(new ModificacaoDano(-3, 2), batalha.Jogador.Condicoes);

                if (batalha.Aliados.Count > 0)
                {
                    foreach (var aliado in batalha.Aliados)
                    {
                        CondicaoController.AplicarOuAtualizarCondicao(new ModificacaoDano(-3, 2), aliado.Condicoes);
                    }
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

