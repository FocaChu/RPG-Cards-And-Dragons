using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.ClassesCondicoes;
using CardsAndDragons.Controllers;
using CardsAndDragons.Inimigos;
using CardsAndDragonsJogo;
using CardsAndDragonsJogo.ClassesCartas;
using RPGCardsAndDragons.Aplicadores;
using RPGCardsAndDragons.cartas;
using RPGCardsAndDragons.condicoes;
using RPGCardsAndDragons.controllers;
using RPGCardsAndDragons.fases;

namespace CardsAndDragons
{
    public static class BatalhaController
    {
        static Random rng = new Random();

        public static BiomaJogo DefinirBioma(BiomaJogo biomaAtual)
        {
            List<BiomaJogo> biomasDisponiveis = BuscaController.ObterTodosOsBiomasDisponiveis()
             .Where(fase => fase.Dificuldade == biomaAtual.Dificuldade + 1)
            .ToList();

            BiomaJogo faseEscolhida = biomasDisponiveis[rng.Next(biomasDisponiveis.Count)];

            return faseEscolhida;
        }

        public static int MenuBatalha(Batalha batalha)
        {
            int acao = 0;
            bool selecionado = false;
            int totalOpcoes = batalha.Inimigos.Count + 1;

            Console.CursorVisible = false;

            string[] opcoesCombate = { "Usar Carta", "Analisar", "Ver Inventário", "Passar Turno" };

            while (!selecionado)
            {
                Console.Clear();

                MostrarTelaCombate(batalha.Inimigos, batalha.Aliados);

                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("\n");
                TextoController.CentralizarTexto(@"======================================  !ESCOLHA SUA AÇÃO!  ====================================" + "\n\n");

                for (int i = 0; i < opcoesCombate.Length; i++)
                {
                    Console.ForegroundColor = (i == acao) ? ConsoleColor.DarkGray : ConsoleColor.White;
                    TextoController.CentralizarTexto($"{(i == acao ? ">>" : "  ")} {opcoesCombate[i]}");
                }
                Console.ResetColor();
                Console.WriteLine("\n\n");
                TextoController.CentralizarTexto("============================================================================================================");

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (acao > 0) acao--;
                        else acao = opcoesCombate.Length - 1;
                        break;

                    case ConsoleKey.DownArrow:
                        if (acao < opcoesCombate.Length - 1) acao++;
                        else acao = 0;
                        break;

                    case ConsoleKey.Enter:
                        selecionado = true;
                        break;
                }
            }
            return acao;
        }

        public static void MostrarTelaCombate(List<OInimigo> inimigos, List<ICriaturaCombatente> aliados, int? optionAlvoAliado = null)
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\n");
            TextoController.CentralizarTexto(@"========================================   !COMBATE!   =======================================" + "\n");

            // Mostra Inimigos em cima (em cinza/vermelho)
            MostrarCriaturas(inimigos.Cast<ICriaturaCombatente>().ToList(), null, ConsoleColor.Gray, ConsoleColor.Red);

            Console.WriteLine("\n");
            TextoController.CentralizarTexto(" -- Aliados --\n");

            // Mostra Aliados em verde 
            MostrarCriaturas(aliados, optionAlvoAliado, ConsoleColor.Green, ConsoleColor.Yellow);
        }


        public static void MostrarCriaturas(List<ICriaturaCombatente> criaturas, int? option = null, ConsoleColor corPadrao = ConsoleColor.Gray, ConsoleColor corSelecionado = ConsoleColor.Red)
        {
            if (criaturas == null || criaturas.Count == 0) return;

            int totalOpcoes = criaturas.Count;
            int larguraModelo = criaturas.First().Modelo[0].Length;
            int espacoEntre = 4;
            int larguraTotal = totalOpcoes * larguraModelo + (totalOpcoes - 1) * espacoEntre;
            int margemEsquerda = (Console.WindowWidth - larguraTotal) / 2;

            int numeroLinhas = criaturas.First().Modelo.Count;

            for (int linha = 0; linha < numeroLinhas; linha++)
            {
                Console.SetCursorPosition(margemEsquerda, Console.CursorTop);
                for (int i = 0; i < totalOpcoes; i++)
                {
                    Console.ForegroundColor = (option.HasValue && i == option.Value) ? corSelecionado : corPadrao;
                    Console.Write(criaturas[i].Modelo[linha]);
                    if (i < totalOpcoes - 1)
                        Console.Write(new string(' ', espacoEntre));
                }
                Console.WriteLine();
            }

            // Linha de Vida
            Console.SetCursorPosition(margemEsquerda, Console.CursorTop);
            for (int i = 0; i < totalOpcoes; i++)
            {
                Console.ForegroundColor = (option.HasValue && i == option.Value) ? corSelecionado : corPadrao;

                var criatura = criaturas[i];
                string vidaTexto = $"PS: {criatura.VidaAtual}/{criatura.VidaMax}";
                vidaTexto = vidaTexto.PadLeft((larguraModelo + espacoEntre) / 2 + vidaTexto.Length / 2).PadRight(larguraModelo);

                Console.Write(vidaTexto);

                if (i < totalOpcoes - 1)
                    Console.Write(new string(' ', espacoEntre));
            }
            Console.WriteLine();

            Console.ResetColor();
        }


        public static void MostrarInimigos(List<OInimigo> inimigos, int option)
        {
            int totalOpcoes = inimigos.Count;
            int larguraModelo = inimigos.First().Modelo[0].Length;
            int espacoEntre = 4; // aumentei um pouquinho pra não ficar colado

            int larguraTotal = totalOpcoes * larguraModelo + (totalOpcoes - 1) * espacoEntre;
            int margemEsquerda = (Console.WindowWidth - larguraTotal) / 2;

            int numeroLinhas = inimigos.First().Modelo.Count;

            Console.ResetColor();
            Console.WriteLine("\n\n\n\n");
            TextoController.CentralizarTexto(@"========================================   !COMBATE!   =======================================" + "\n\n");

            // desenha os modelos dos inimigos
            for (int linha = 0; linha < numeroLinhas; linha++)
            {
                Console.SetCursorPosition(margemEsquerda, Console.CursorTop); // começa do meio
                for (int i = 0; i < totalOpcoes; i++)
                {
                    Console.ForegroundColor = (i == option) ? ConsoleColor.Red : ConsoleColor.Gray;
                    Console.Write(inimigos[i].Modelo[linha]);

                    if (i < totalOpcoes - 1)
                        Console.Write(new string(' ', espacoEntre)); // espaçamento entre blocos
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            // desenha a linha de vida
            Console.SetCursorPosition(margemEsquerda, Console.CursorTop);

            for (int i = 0; i < totalOpcoes; i++)
            {
                Console.ForegroundColor = (i == option) ? ConsoleColor.Red : ConsoleColor.Gray;

                var inimigo = inimigos[i];
                string vidaTexto = $"PS: {inimigo.VidaAtual}/{inimigo.VidaMax}";
                vidaTexto = vidaTexto.PadLeft((larguraModelo + espacoEntre) / 2 + vidaTexto.Length / 2).PadRight(larguraModelo);

                Console.Write(vidaTexto);

                if (i < totalOpcoes - 1)
                    Console.Write(new string(' ', espacoEntre)); // espaço entre inimigos
            }
            Console.WriteLine();

            Console.ResetColor();
        }

        public static List<ICriaturaCombatente> ObterAlvosAliados(Batalha batalha)
        {
            var alvos = new List<ICriaturaCombatente> { batalha.Jogador };
            alvos.AddRange(batalha.Aliados);

            return alvos;
        }

        public static int GerarRNG(int porcentagem)
        {
            Random rng = new Random();

            int chance = rng.Next(100);
            //faz o rng de inimigos aqui
            int resultado = (chance < porcentagem) ? 0 : 1;

            return resultado;

        }

        //COdigo vita! Ele é quem gera os inimigos da fase
        public static List<OInimigo> GerarOsInimigos(int dificuldadeJogo, BiomaJogo biomaAtual, int faseAtual)
        {
            List<OInimigo> inimigosDaFase = new List<OInimigo>();

            bool ehChefe = faseAtual % 10 == 0 ? true : false; // Verifica se é um boss (a cada 10 fases)

            var listaOrigem = ehChefe ? biomaAtual.DefinirChefes() : biomaAtual.DefinirInimigos();

            int qtdInimigos = ehChefe ? 1 : (BatalhaController.GerarRNG(60) == 0 ? 3 : 4);

            for (int i = 0; i < qtdInimigos; i++)
            {
                if (listaOrigem.Count == 0) break;

                int indice = rng.Next(listaOrigem.Count);
                Type tipo = listaOrigem[indice].GetType();

                InimigoRPG inimigoGerado = (InimigoRPG)Activator.CreateInstance(tipo);

                inimigosDaFase.Add(new OInimigo(inimigoGerado));
            }



            // Multiplicador de dificuldade
            double multiplicador = 0;

            switch (dificuldadeJogo)
            {
                case 1:
                    multiplicador = 1.25;
                    break;
                case 2:
                    multiplicador = 1.5;
                    break;
                default:
                    multiplicador = 1.0;
                    break;
            }

            foreach (var inimigo in inimigosDaFase)
            {
                inimigo.VidaMax = (int)(inimigo.VidaMax * multiplicador);
                inimigo.VidaAtual = inimigo.VidaMax;
                inimigo.DanoBase = (int)(inimigo.DanoBase * multiplicador);
            }

            return inimigosDaFase;
        }


        public static void AcaoUsarCarta(Batalha batalha)
        {
            // Primeiro, escolher a carta
            int escolhaCarta = PersonagemController.SelecionarCarta(batalha.Jogador, batalha.Jogador.Mao, 0);

            if (escolhaCarta < 0) return; // cancelou

            var carta = batalha.Jogador.Mao[escolhaCarta];

            batalha.Jogador.UsarCarta(batalha, escolhaCarta);
            VerificarMorte(batalha);

        }

        public static void AcaoExibir(Batalha batalha)
        {
            if (batalha.Aliados.Count > 0)
            {
                string[] opcoesConfirmacao = { "Aliado", "Inimigo" };
                int opcao = TextoController.MostrarMenuSelecao(false, "Deseja Analisar Um Aliado Ou Um Inimigo?", opcoesConfirmacao);

                if (opcao == 0)
                {
                    var alvo = batalha.Aliados[AlvoController.SelecionarAlvo(batalha.Aliados)];

                    ExibirAlvo(alvo);

                }
                else
                {
                    int alvo = AlvoController.SelecionarAlvo(batalha.Inimigos);

                    ExibirAlvo(batalha.Inimigos[alvo]);

                }


            }
            else
            {

                int alvo = AlvoController.SelecionarAlvo(batalha.Inimigos);

                ExibirAlvo(batalha.Inimigos[alvo]);


            }
        }

        public static void AcaoPassarTurno(Batalha batalha)
        {
            CondicaoController.Checape(batalha);

            Console.ReadKey();

            if (batalha.Aliados.Count > 0)
            {
                batalha.TurnoAliados();

                Console.ReadKey();
            }

            batalha.TurnoInimigos();

            Console.ReadKey();

            VerificarMorte(batalha);

            CondicaoController.Checape(batalha.Jogador);

        }

        public static void ExibirAlvo(ICriaturaCombatente critatura)
        {
            Console.Clear();

            Console.WriteLine("\n\n\n");
            TextoController.CentralizarTexto($"================================  < Analisando {critatura.Nome} > ================================\n\n");

            TextoController.CentralizarTexto($"Vida: {critatura.VidaAtual}/{critatura.VidaMax}\n");

            CondicaoController.ExibirCondicoes(critatura);

            for (int linha = 0; linha < critatura.Modelo.Count; linha++)
            {
                TextoController.CentralizarTexto(critatura.Modelo[linha]);
            }
        }


        public static void VerificarMorte(Batalha batalha)
        {
            List<OInimigo> inimigosMortos = new List<OInimigo>();
            List<ICriaturaCombatente> aliadosMortos = new List<ICriaturaCombatente>();

            foreach (var inimigo in batalha.Inimigos)
            {

                if (inimigo.VidaAtual <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    TextoController.CentralizarTexto($"{inimigo.Nome} foi derrotado!");
                    Console.ResetColor();

                    int ouroGanho = (batalha.Jogador.Especie.NomeEspecie == "Anão") ? (15 * (int)inimigo.BiomaDeOrigem) : (10 * (int)inimigo.BiomaDeOrigem);

                    ouroGanho += (inimigo.EBoss) ? (25 * (int)inimigo.BiomaDeOrigem) : 0;

                    batalha.Jogador.Ouros += ouroGanho;

                    TextoController.CentralizarTexto($"Você ganhou {ouroGanho} ouros. Total: {batalha.Jogador.Ouros}");

                    batalha.Jogador.ContabilizarXp(inimigo);

                    //passa o inimigo que morreu pra uma lista temporaria. A remoção direta causa erro
                    inimigosMortos.Add(inimigo);
                }
            }

            foreach (var morto in inimigosMortos)
            {
                batalha.InimigosDerrotados.Add(morto);
                batalha.Inimigos.Remove(morto);
            }

            if (batalha.Aliados.Count > 0)
            {
                foreach (var aliado in batalha.Aliados)
                {

                    if (aliado.VidaAtual <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        TextoController.CentralizarTexto($"{aliado.Nome} foi derrotado!");
                        Console.ResetColor();

                        //passa o aliado que morreu pra uma lista temporaria. A remoção direta causa erro
                        aliadosMortos.Add(aliado);
                    }
                }
                foreach (var aliado in aliadosMortos)
                {
                    batalha.Aliados.Remove(aliado);
                }

                Console.ReadKey();
            }
        }

        public static int VerificarResultadoTurno(Batalha batalha)
        {

            if (batalha.Jogador.VidaAtual == 0)
            {
                return 1;
            }
            if (batalha.Inimigos.Count == 0)
            {
                return 2;
            }
            else
            {
                return 3;

            }
        }

        public static void VerificarRessucitadores(Batalha batalha)
        {
            if (batalha.Aplicadores.Count > 0)
            {
                // Cria uma cópia da lista para evitar problemas de modificação durante a iteração
                var aplicadoresAtuais = new List<IAplicador>(batalha.Aplicadores);

                foreach (var aplicador in aplicadoresAtuais)
                {
                    if (aplicador is AplicadorRessuireicao ressucitador)
                    {
                        if(ressucitador.Aplicar(batalha)) batalha.Aplicadores.Remove(ressucitador); // Remove o aplicador imediatamente após a execução
                    }
                }
            }
        }

        public static void VerificarContaminadores(Batalha batalha)
        {
            if (batalha.Aplicadores.Count > 0)
            {
                // Cria uma cópia da lista para evitar problemas de modificação durante a iteração
                var aplicadoresAtuais = new List<IAplicador>(batalha.Aplicadores);

                foreach (var aplicador in aplicadoresAtuais)
                {
                    if (aplicador is AplicadorCondicao contaminador)
                    {
                        contaminador.Aplicar(batalha);
                        batalha.Aplicadores.Remove(contaminador); // Remove o aplicador imediatamente após a execução
                    }
                }
            }
        }

        public static void VerificarEvoluidores(Batalha batalha)
        {
            if (batalha.Aplicadores.Count > 0)
            {
                // Cria uma cópia da lista para evitar problemas de modificação durante a iteração
                var aplicadoresAtuais = new List<IAplicador>(batalha.Aplicadores);

                foreach (var aplicador in aplicadoresAtuais)
                {
                    if(aplicador is AplicadorEvolucao evoluidor)
                    {
                        evoluidor.Aplicar(batalha);
                        batalha.Aplicadores.Remove(evoluidor); // Remove o aplicador imediatamente após a execução
                    }
                }
            }
        }

        public static void NovaRodada(Batalha batalha)
        {
            BatalhaController.VerificarRessucitadores(batalha);

            if (batalha.Jogador.BaralhoCompra.Count == 0)
            {
                PersonagemController.RecarregarBaralho(batalha.Jogador);
            }
            PersonagemController.ComprarCartas(batalha.Jogador);

            PersonagemController.RestaurarJogador(batalha.Jogador);

        }

    }
}
