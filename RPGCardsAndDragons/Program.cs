using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.ClassesDasCartas;
using CardsAndDragons.Controllers;
using CardsAndDragons.Inimigos;
using CardsAndDragonsJogo;
using CardsAndDragonsJogo.ClassesCartas;

namespace CardsAndDragons
{
    //Enum com todos os possiveis estados de tela do jogo
    enum EstadoDoJogo
    {
        MenuInicial,
        Creditos,
        SelecionarDificuldade,
        SelecaoPersonagem,
        CriarPersonagem,
        Jogo,
        Derrota,
        Vitoria,
        Encerrar
    }

    class Program
    {
        #region Variaveis Static
        //Abre o menu assim que começa o jogo
        static EstadoDoJogo estadoAtual = EstadoDoJogo.MenuInicial;

        //Guarda nosso jogador
        static List<Personagem> oJogador = new List<Personagem>();

        //Guarda os inimigos de cada batalha
        static List<OInimigo> inimigosDaFase = new List<OInimigo> { };

        //forma mais intuitva de chamar o jogador que estamos usando
        static int jogadorAtual = 0;

        //define a dificuldade das lutas
        public static Bioma BiomaAtual = Bioma.Floresta;

        public static int faseAtual = 1;

        static int dificuladeDoJogo = 1;

        static bool acabarJogo = false;
        static bool acabarBatalha = false;
        static bool acabarRodada = false;
        static bool primeiroRodada = true;
        static bool acabarLoja = false;
        #endregion

        static void Main(string[] args)
        {
            Console.WindowWidth = Console.LargestWindowWidth;
            Console.WindowHeight = Console.LargestWindowHeight;

             //Mantem o jogo rodando, chamando as funções com base no estadoa atual do jogo
            while (estadoAtual != EstadoDoJogo.Encerrar)
            {
                switch (estadoAtual)
                {
                    case EstadoDoJogo.MenuInicial:
                        MostrarMenuInicial();
                        break;

                    case EstadoDoJogo.Creditos:
                        Creditos();
                        break;

                    case EstadoDoJogo.SelecionarDificuldade:
                        MudarDificuldade();
                        break;

                    case EstadoDoJogo.CriarPersonagem:
                        CriarPersonagem();
                        break;

                    case EstadoDoJogo.Jogo:
                        JogarPartida();
                        break;

                    case EstadoDoJogo.Derrota:
                        MostrarDerrota();
                        break;

                    case EstadoDoJogo.Vitoria:
                        MostrarVitoria();
                        break;
                }

                Console.Clear();
            }
        }

        #region Menus

        //Menu inicial do jogo
        static void MostrarMenuInicial()
        {
            oJogador.Clear();

            //passa as opções pro menu
            string[] opcoes = new string[]
            {
                "Jogar",
                "Credítos",
                "Dificuldade",
                "Sair"
            };

            //usa a função para receber o valor da opção escolhida
            int escolha = TextoController.MostrarMenuSelecao(true, "Menu Inicial", opcoes);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            //verifca o valor da escolha e muda o estado atual do jogo com base nessa opção
            switch (escolha)
            {
                case 0:
                    estadoAtual = EstadoDoJogo.CriarPersonagem;
                    break;
                case 1:
                    estadoAtual = EstadoDoJogo.Creditos;
                    break;
                case 2:
                    estadoAtual = EstadoDoJogo.SelecionarDificuldade;
                    break;
                case 3:
                    estadoAtual = EstadoDoJogo.Encerrar;
                    break;
            }
        }

        //Opção de mudar dificulade do jogo
        static void MudarDificuldade()
        {
            string[] dificuldadesEscolhidas = new string[]
            {
                "Facil",
                "Médio",
                "Dificil",
                "Sair"
            };

            int escolha = TextoController.MostrarMenuSelecao(false, "DIFICULDADE", dificuldadesEscolhidas);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            switch (escolha)
            {
                case 0:
                    dificuladeDoJogo = 0;
                    Console.WriteLine("\n\n");
                    TextoController.CentralizarTexto("A dificuldade do jogo agora é fácil");
                    Console.ReadKey();
                    break;
                case 1:
                    dificuladeDoJogo = 1;
                    Console.WriteLine("\n\n");
                    TextoController.CentralizarTexto("A dificuldade do jogo agora é média");
                    Console.ReadKey();
                    break;
                case 2:
                    dificuladeDoJogo = 2;
                    Console.WriteLine("\n\n");
                    TextoController.CentralizarTexto("A dificuldade do jogo agora é díficil");
                    Console.ReadKey();
                    break;
                case 3:
                    Console.WriteLine("\n\n");
                    TextoController.CentralizarTexto("Voltando...");
                    Console.ReadKey();
                    break;
            }
            estadoAtual = EstadoDoJogo.MenuInicial;
        }

        //mostra os creditos
        public static void Creditos()
        {
            TextoController.MostrarCreditos();

            Console.ReadKey();
            estadoAtual = EstadoDoJogo.MenuInicial;
        }

        #endregion

        //codigo que faz nosso personagem
        static void CriarPersonagem()
        {
            bool acabar = false;
            bool resetar = false;
            while (!acabar)
            {
                //Chama o controler do personagem pra definir a especie
                EspecieRPG especieEscolhida = PersonagemController.SelecionarEspecie();

                //Caso o jogador cancele a ação de selecionar a especie, ela retorna null e o codigo volta pro menu
                if (especieEscolhida == null)
                {
                    estadoAtual = EstadoDoJogo.MenuInicial;
                    break;

                }

                //Chama o controler do personagem pra definir a classe
                ClasseRPG classeEscolhida = PersonagemController.EscolherClasse();

                //Caso o jogador cancele a ação de selecionar a especie, ela retorna null e o codigo volta pra seleção de especie
                if (classeEscolhida == null)
                {
                    resetar = true;
                    break;

                }

                //Aqui o jogador confirma o personagem dele
                string[] opcoesConfirmacao = { "Sim", "Não" };
                int option = TextoController.MostrarMenuSelecao(false, "Confirmação de Personagem", opcoesConfirmacao);

                Console.Clear();

                Console.ForegroundColor = ConsoleColor.White;
                if (option == 0)
                {
                    //Cria o persongaem do jogador
                    var jogador = PersonagemController.CriarPersonagem(especieEscolhida, classeEscolhida);
                    oJogador.Add(jogador);

                    Console.Clear();

                    //exibe o personagem pro jogador
                    PersonagemController.ExibirJogador(jogador, false);

                    estadoAtual = EstadoDoJogo.Jogo;
                    acabar = true;
                }
                else
                {
                    estadoAtual = EstadoDoJogo.MenuInicial;
                    acabar = true;
                }
            }
            //aqui ele chama a função de novo caso o jogador tenha retornado null em classe
            if (resetar) CriarPersonagem();

        }

        #region Combate

        static void JogarPartida()
        {
            faseAtual = 1;

            acabarJogo = false;
            acabarBatalha = false;
            acabarRodada = false;
            primeiroRodada = true;

            //começa a run

            Console.WriteLine("\n\n\n\n");
            TextoController.CentralizarTexto("================================================== * CPAITÚLO 1 * ==================================================\n\n");

            TextoController.CentralizarTexto($"{oJogador[jogadorAtual].Nome} adentra a floresta antiga. Suas árvores e mata escondem perigos sinuoso pela a jornada até o dragão...");

            Console.ReadKey();

            while (!acabarJogo)
            {
                inimigosDaFase.Clear();

                //define os inimigos da batalha com base na dificuldade atual
                inimigosDaFase = BatalhaController.GerarOsInimigos(dificuladeDoJogo, faseAtual, BiomaAtual);

                //faz a classe batalha recebendo os inimigos e o jogador
                Batalha batalha = new Batalha(oJogador[jogadorAtual], inimigosDaFase);

                //permite que outra batalha começe
                acabarBatalha = false;
                primeiroRodada = true;

                if (faseAtual % 10 == 0)
                {
                    Console.Clear();

                    Console.WriteLine("\n\n\n\n");
                    TextoController.CentralizarTexto($"Após horas de jornada {batalha.Jogador.Nome} finalmente chega ao ninho do dragão...\n\n");

                    TextoController.CentralizarTexto($"Céus e terra tremem enquanto a forma da besta sai da escuridão de seu esconderijo");

                    Console.ReadKey();

                    Console.Clear();

                    TextoController.TituloBossFight();


                    Console.ReadKey();

                    Console.Clear();
                }

                if (faseAtual % 5 == 0)
                {

                }
                //começa uma batalha
                while (!acabarBatalha)
                {
                    
                    IniciarBatalha(batalha);

                }

                if (faseAtual % 3 == 0)
                {
                    acabarLoja = false;

                    Loja loja = new Loja(oJogador[jogadorAtual]);
                    while (!acabarLoja)
                    {
                        IniciarLoja(loja);

                    }
                }
            }
        }

        static void IniciarBatalha(Batalha batalha)
        {
            Console.CursorVisible = false;

            if (primeiroRodada)
            {
                batalha.Jogador.Condicoes.Clear();
                BatalhaController.NovaRodada(batalha);
                primeiroRodada = false;
            }

            //Mostra os inimigos ao mesmo tempo que deixa escolher uma ação
            int acao = BatalhaController.MenuBatalha(batalha);

            //verifica e efetua a ação
            switch (acao)
            {
                case 0:
                    BatalhaController.AcaoUsarCarta(batalha);
                    break;

                case 1:
                    BatalhaController.AcaoExibir(batalha);
                    break;

                case 2:
                    PersonagemController.ExibirJogador(batalha.Jogador, true);
                    break;

                case 3:
                    BatalhaController.AcaoPassarTurno(batalha);
                    acabarRodada = true;
                    break;
            }
            Console.ReadKey();

            //Aqui ele verifica oque aconteceu nesse turno, se jogador morreu, se os inimigos morreram ou se nada demais aconteceu
            int resultadoTurno = BatalhaController.VerificarResultadoTurno(batalha);

            switch (resultadoTurno)
            {
                //caso o jogador tenha morrido, da game over
                case 1:
                    estadoAtual = EstadoDoJogo.Derrota;
                    acabarBatalha = true;
                    acabarJogo = true;
                    break;

                //caso todos os inimigos tenham morrido, batalha acaba
                case 2:
                    AliadoController.RetornarRobosSobreviventes(batalha);
                    acabarBatalha = true;
                    if(faseAtual == 10) 
                    {
                        estadoAtual = EstadoDoJogo.Vitoria;
                        acabarJogo = true;
                    }
                    else 
                    { 
                        faseAtual++; 
                        TextoController.MostrarVenceuBatalha(batalha);
                    }
                    break;

                case 3:
                    //caso nada demais tenha acontecido E o jogador tenha passado o turno(e saido vivo) ele começa a proxima rodada
                    if (acabarRodada) BatalhaController.NovaRodada(batalha);
                    acabarRodada = false;
                    break;
            }
        }

        static void IniciarLoja(Loja loja)
        {
            Console.CursorVisible = false;

            //Mostra os inimigos ao mesmo tempo que deixa escolher uma ação
            int acao = LojaController.MenuLoja(loja);

            //verifica e efetua a ação
            switch (acao)
            {
                case 0:
                    LojaController.ComprarCartas(loja);
                    break;

                case 1:
                    LojaController.ComprarCura(loja);
                    break;

                case 2:
                    PersonagemController.ExibirJogador(loja.Jogador, true);
                    break;

                case 3:
                    acabarLoja = LojaController.FecharLoja();
                    break;
            }
        }

        static void IniciarEventoAleatorio()
        {

        }

        //tela de derrota do jogo
        static void MostrarDerrota()
        {
            string[] opcoes = new string[]
            {
                "Voltar ao Menu",
                "Sair do Jogo"
            };

            int opcao = TextoController.MostrarMenuSelecao(false, "Você perdeu", opcoes);

            switch (opcao)
            {
                case 1:
                    estadoAtual = EstadoDoJogo.MenuInicial;
                    break;
                case 2:
                    estadoAtual = EstadoDoJogo.Encerrar;
                    break;
            }
        }

        static void MostrarVitoria()
        {
            Console.WriteLine("\n\n\n");
            TextoController.CentralizarTexto("============================== ! Você venceu ! ==============================\n\n\n");

            TextoController.CentralizarTexto("Muito obrigado por jogar! Esse projeto foi feito com muito carinho e espero que tenha se divertido!\n\n");

            TextoController.CentralizarTexto("Aperte qualquer tecla para voltar...");

            Console.ReadKey();

            estadoAtual = EstadoDoJogo.Creditos;
        }

        #endregion
    }
}