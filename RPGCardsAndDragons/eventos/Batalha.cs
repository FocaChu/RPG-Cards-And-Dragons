using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons;
using CardsAndDragons.Controllers;
using CardsAndDragons.Inimigos;

namespace CardsAndDragonsJogo
{
    public class Batalha
    {
        public Personagem Jogador { get; set; }

        public List<OInimigo> Inimigos { get; set; }

        public List<OInimigo> InimigosDerrotados { get; set; }

        public List<ICriaturaCombatente> Aliados { get; set; }


        public int rodadaAtual = 1;

        public Batalha(Personagem jogadorAtual, List<OInimigo> inimigosDaFase)
        {
            Jogador = jogadorAtual;
            Inimigos = inimigosDaFase;
            
            InimigosDerrotados = new List<OInimigo>();
            Aliados = new List<ICriaturaCombatente>();

            AliadoController.AcidionarRobosABatalha(this);
        }

        //faz os aliados atacarem
        public void TurnoAliados()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n");
            TextoController.CentralizarTexto("============================== !TURNO DOS ALIADOS! ==============================\n");
            Console.ResetColor();

            foreach (var aliado in Aliados)
            {
                Console.WriteLine();
                TextoController.CentralizarTexto($"{aliado.Nome} está se preparando para agir...");
                Console.WriteLine();
                System.Threading.Thread.Sleep(200); // Delayzinho dramático

                if (!CondicaoController.VerificarCongelamento(aliado))
                {
                    aliado.RealizarTurno(this);

                    Console.WriteLine();
                    System.Threading.Thread.Sleep(800); // Tempo pra ler o ataque

                    CondicaoController.SangrarFerida(aliado);
                }
                else
                {
                    TextoController.CentralizarTexto($"\n{aliado.Nome} está congelado e não pode agir...\n");
                }
            }
        }

        //Faz os inimigos atacarem o jogador
        public void TurnoInimigos()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\n");
            TextoController.CentralizarTexto("============================== !TURNO DOS INIMIGOS! ==============================\n");
            Console.ResetColor();

            foreach (var inimigo in Inimigos)
            {
                Console.WriteLine();
                TextoController.CentralizarTexto($"{inimigo.Nome} está se preparando para agir...");
                Console.WriteLine();
                System.Threading.Thread.Sleep(200); // Delayzinho dramático

                if (!CondicaoController.VerificarCongelamento(inimigo))
                {
                    inimigo.RealizarTurno(this);

                    Console.WriteLine();
                    System.Threading.Thread.Sleep(800); // Tempo pra ler o ataque

                    CondicaoController.SangrarFerida(inimigo);
                }
                else
                {
                    TextoController.CentralizarTexto($"\n{inimigo.Nome} está congelado e não pode agir...\n");
                }
            }
            rodadaAtual++;
        }
    }
}
