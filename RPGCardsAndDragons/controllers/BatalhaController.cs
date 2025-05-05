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

namespace CardsAndDragons
{
    public static class BatalhaController
    {

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
        public static List<OInimigo> GerarOsInimigos(int dificuldadeJogo, int faseAtual, Bioma biomaAtual)
        {
            List<OInimigo> inimigosDaFase = new List<OInimigo>();

            Random rng = new Random();

            if (faseAtual % 10 == 0)
            {

                //Usa o ajudante para ganhar a lista com os inimigos do jogo
                var tiposDeInimigos = InimigoRPGAjudante.ObterTiposDeInimigosDisponiveis();

                // Filtrar inimigos com base na dificuldade
                var inimigosValidos = tiposDeInimigos
                    .Select(t => (InimigoRPG)Activator.CreateInstance(t))
                    .Where(inimigo => inimigo.BiomaDeOrigem == biomaAtual && inimigo.EBoss == true)
                    .ToList();

                //faz o rng de inimigos aqui
                int quantidadeInimigosNaFase = 1;

                for (int i = 0; i < quantidadeInimigosNaFase; i++)
                {
                    if (inimigosValidos.Count == 0) break;

                    int indice = rng.Next(inimigosValidos.Count);

                    // cria inimigos aqui
                    Type tipo = inimigosValidos[indice].GetType();
                    InimigoRPG novoInimigo = (InimigoRPG)Activator.CreateInstance(tipo);

                    inimigosDaFase.Add(new OInimigo(novoInimigo));
                }
            }
            else
            {

                //Usa o ajudante para ganhar a lista com os inimigos do jogo
                var tiposDeInimigos = InimigoRPGAjudante.ObterTiposDeInimigosDisponiveis();

                // Filtrar inimigos com base na dificuldade
                var inimigosValidos = tiposDeInimigos
                    .Select(t => (InimigoRPG)Activator.CreateInstance(t))
                    .Where(inimigo => inimigo.BiomaDeOrigem == Program.BiomaAtual && inimigo.EBoss == false)
                    .ToList();

                //faz o rng dos inimigos aqui
                int porcentagem = 60;
                int resultado = BatalhaController.GerarRNG(porcentagem);

                int qtdInimigosNaFase = resultado == 0 ? 3 : 4;

                for (int i = 0; i < qtdInimigosNaFase; i++)
                {
                    if (inimigosValidos.Count == 0) break;

                    int indice = rng.Next(inimigosValidos.Count);

                    // cria inimigos aqui
                    Type tipo = inimigosValidos[indice].GetType();
                    InimigoRPG novoInimigo = (InimigoRPG)Activator.CreateInstance(tipo);

                    inimigosDaFase.Add(new OInimigo(novoInimigo));
                }
            }
            // Define o multiplicador de dificuldade
            double multiplicador = 1.0;

            if (dificuldadeJogo == 1) multiplicador = 1.25;    // Médio
            else if (dificuldadeJogo == 2) multiplicador = 1.5; // Difícil

            for (int i = 0; i < inimigosDaFase.Count; i++)
            {
                var inimigo = inimigosDaFase[i];

                // Aplica o modificador de dificuldade
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
            if (batalha.Evoluidores.Count > 0)
            {
                foreach (var evoluidor in batalha.Evoluidores)
                {
                    evoluidor.Evoluir(batalha.Jogador.BaralhoCompleto);
                }

                batalha.Evoluidores.Clear();
            }


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

        public static void NovaRodada(Batalha batalha)
        {
            if (batalha.Jogador.BaralhoCompra.Count == 0)
            {
                PersonagemController.RecarregarBaralho(batalha.Jogador);
            }
            PersonagemController.ComprarCartas(batalha.Jogador);

            PersonagemController.RestaurarJogador(batalha.Jogador);

        }

    }
}
