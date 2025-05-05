using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons;
using CardsAndDragons.ClassesCondicoes;
using CardsAndDragons.Controllers;
using CardsAndDragons.Inimigos;
using RPGCardsAndDragons.cartas;
using RPGCardsAndDragons.condicoes;

namespace CardsAndDragonsJogo
{
    public class Batalha
    {
        public Personagem Jogador { get; set; }

        public List<OInimigo> Inimigos { get; set; }

        public List<OInimigo> InimigosDerrotados { get; set; }

        public List<AplicadorDeCondicao> Aplicadores { get; set; }

        public List<EvoluidorDeCartas> Evoluidores { get; set; }

        public List<ICriaturaCombatente> Aliados { get; set; }


        public int rodadaAtual = 1;

        public Batalha(Personagem jogadorAtual, List<OInimigo> inimigosDaFase)
        {
            Jogador = jogadorAtual;
            Inimigos = inimigosDaFase;

            InimigosDerrotados = new List<OInimigo>();
            Aliados = new List<ICriaturaCombatente>();

            Aplicadores = new List<AplicadorDeCondicao>();
            Evoluidores = new List<EvoluidorDeCartas>();

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

                if (!CondicaoController.VerificarCondicao<Atordoamento>(aliado.Condicoes))
                {
                    aliado.RealizarTurno(this);

                    Console.WriteLine();
                    System.Threading.Thread.Sleep(800); // Tempo pra ler o ataque

                    CondicaoController.SangrarFerida(aliado);
                }
                else
                {
                    TextoController.CentralizarTexto($"{aliado.Nome} está atordoado e não pode agir...\n");
                    List<ICondicaoTemporaria> condicoes = new List<ICondicaoTemporaria>();
                    foreach (var condicao in aliado.Condicoes)
                    {
                        if (condicao is Atordoamento atordoamento)
                        {
                            atordoamento.Duracao--;
                            if (atordoamento.Duracao <= 0)
                            {
                                condicoes.Add(condicao);
                                TextoController.CentralizarTexto($"{aliado.Nome} se libertou do atordoamento!");
                            }
                        }
                    }
                    aliado.Condicoes.RemoveAll(c => condicoes.Contains(c));
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

                if (!CondicaoController.VerificarCondicao<Atordoamento>(inimigo.Condicoes))
                {
                    inimigo.RealizarTurno(this);

                    Console.WriteLine();
                    System.Threading.Thread.Sleep(800); // Tempo pra ler o ataque

                    CondicaoController.SangrarFerida(inimigo);
                }
                else
                {
                    TextoController.CentralizarTexto($"{inimigo.Nome} está atordoado e não pode agir...\n");
                    List<ICondicaoTemporaria> condicoes = new List<ICondicaoTemporaria>();
                    foreach (var condicao in inimigo.Condicoes)
                    {
                        if (condicao is Atordoamento atordoamento)
                        {
                            atordoamento.Duracao--;
                            if (atordoamento.Duracao <= 0)
                            {
                                condicoes.Add(condicao);
                                TextoController.CentralizarTexto($"{inimigo.Nome} se libertou do atordoamento!");
                            }
                        }
                    }
                    inimigo.Condicoes.RemoveAll(c => condicoes.Contains(c));
                }
            }
            rodadaAtual++;
        }
    }
}
