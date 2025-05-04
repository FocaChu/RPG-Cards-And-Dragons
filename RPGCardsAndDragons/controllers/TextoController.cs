using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragonsJogo;

namespace CardsAndDragons.Controllers
{
    public static class TextoController
    {
        public static void Titulo()
        {
            //mostra o titulo do jogo// degrade: magenta -> red -> dark red -> dark yellow -> yellow
            Console.WriteLine("\n\n");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            CentralizarTexto(@"                 (    (      (                  )  (       (      (                         )      )  (     ");
            CentralizarTexto(@"    (     (      )\ ) )\ )   )\ )     (      ( /(  )\ )    )\ )   )\ )    (      (       ( /(   ( /(  )\ )  ");
            CentralizarTexto(@"    )\    )\    (()/((()/(  (()/(     )\     )\())(()/(   (()/(  (()/(    )\     )\ )    )\())  )\())(()/(  ");

            Console.ForegroundColor = ConsoleColor.Red;
            CentralizarTexto(@"  (((_)((((_)(   /(_))/(_))  /(_)) ((((_)(  ((_)\  /(_))   /(_))  /(_))((((_)(  (()/(   ((_)\  ((_)\  /(_)) ");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            CentralizarTexto(@"  )\___ )\ _ )\ (_)) (_))_  (_))    )\ _ )\  _((_)(_))_   (_))_  (_))   )\ _ )\  /(_))_   ((_)  _((_)(_))   ");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            CentralizarTexto(@" ((/ __|(_)_\(_)| _ \ |   \ / __|   (_)_\(_)| \| | |   \   |   \ | _ \  (_)_\(_)(_)) __| / _ \ | \| |/ __|  ");
            CentralizarTexto(@"  | (__  / _ \  |   / | |) |\__ \    / _ \  | .` | | |) |  | |) ||   /   / _ \    | (_ || (_) || .` |\__ \  ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            CentralizarTexto(@"   \___|/_/ \_\ |_|_\ |___/ |___/   /_/ \_\ |_|\_| |___/   |___/ |_|_\  /_/ \_\    \___| \___/ |_|\_||___/  ");

            Console.ResetColor();
            Console.WriteLine("\n\n");

            /*
            
                (    (      (                  )  (       (      (                         )      )  (    
   (     (      )\ ) )\ )   )\ )     (      ( /(  )\ )    )\ )   )\ )    (      (       ( /(   ( /(  )\ )  
   )\    )\    (()/((()/(  (()/(     )\     )\())(()/(   (()/(  (()/(    )\     )\ )    )\())  )\())(()/(  
 (((_)((((_)(   /(_))/(_))  /(_)) ((((_)(  ((_)\  /(_))   /(_))  /(_))((((_)(  (()/(   ((_)\  ((_)\  /(_)) 
 )\___ )\ _ )\ (_)) (_))_  (_))    )\ _ )\  _((_)(_))_   (_))_  (_))   )\ _ )\  /(_))_   ((_)  _((_)(_))   
((/ __|(_)_\(_)| _ \ |   \ / __|   (_)_\(_)| \| | |   \   |   \ | _ \  (_)_\(_)(_)) __| / _ \ | \| |/ __|  
 | (__  / _ \  |   / | |) |\__ \    / _ \  | .` | | |) |  | |) ||   /   / _ \    | (_ || (_) || .` |\__ \  
  \___|/_/ \_\ |_|_\ |___/ |___/   /_/ \_\ |_|\_| |___/   |___/ |_|_\  /_/ \_\    \___| \___/ |_|\_||___/  
                                                                                                          
            
            */
        }

        public static void TituloBossFight()
        {
            //mostra o titulo do jogo// degrade: magenta -> red -> dark red -> dark yellow -> yellow
            Console.WriteLine("\n\n\n\n");
            Console.ForegroundColor = ConsoleColor.Red;
            CentralizarTexto(@"._. .____            __                                                  .__            _____        ._.");
            CentralizarTexto(@"| | |    |    __ ___/  |______      ____  ____   _____     ____     ____ |  |__   _____/ ____\____   | |");
            CentralizarTexto(@"| | |    |   |  |  \   __\__  \   _/ ___\/  _ \ /     \   /  _ \  _/ ___\|  |  \_/ __ \   __\/ __ \  | |");
            CentralizarTexto(@" \| |    |___|  |  /|  |  / __ \_ \  \__(  <_> )  Y Y  \ (  <_> ) \  \___|   Y  \  ___/|  | \  ___/   \|");
            CentralizarTexto(@" __ |_______ \____/ |__| (____  /  \___  >____/|__|_|  /  \____/   \___  >___|  /\___  >__|  \___  >  __");
            CentralizarTexto(@" \/         \/                \/       \/            \/                \/     \/     \/          \/   \/");

            Console.ResetColor();

        }

        public static void MostrarCreditos()
        {
            string espaco = "               ";

            Console.WriteLine("\n\n\n\n");
            CentralizarTexto("--- Créditos da Jornada ---\n\n\n\n");

            Console.WriteLine(espaco + "Abstração e Estrutura: João Augusto Cordeiro\n\n");

            Console.WriteLine(espaco + "Sistema de Cartas e Inimigos:  João Augusto Cordeiro e Yago Von Krüger Fukuoka\n\n");

            Console.WriteLine(espaco + "Sistema de Combate:  João Augusto Cordeiro, Kauã Gabriel dos Santos e Isaque Anacleto de Meira\n\n");

            Console.WriteLine(espaco + "Som e Áudio: Isaque Anacleto de Meira e Emylli Sousa Lima\n\n");

            Console.WriteLine(espaco + "Gráficos e História: Isaque Anacleto de Meira e Emylli Sousa Lima\n\n");

            Console.WriteLine(espaco + "Menção Honrosa: Aymê Strithorst Ender\n\n");

            Console.WriteLine(espaco + "Obrigado por jogar nossa aventura!\n\n\n");

            Console.WriteLine(espaco + "Pressione qualquer tecla para voltar");
        }

        //COdigo mais "generico" para mostrar menus simples de seleção. Atualmente sendo usado pra menu inicial, dificulade e confirmar personagem
        public static int MostrarMenuSelecao(bool mostrarTitulo, string titulo, string[] opcoes)
        {
            int option = 0;
            bool selecionado = false;

            Console.CursorVisible = false;

            //continua até vc selecionar uma opção
            while (!selecionado)
            {
                Console.Clear();
                Console.WriteLine("\n\n\n\n");

                //serve pra mostrar o titulo do jogo
                if (mostrarTitulo) Titulo();

                //Poem o titulo no menu em cacha alta
                CentralizarTexto($"================================================  -- {titulo.ToUpper()} -- ==============================================");

                Console.ForegroundColor = ConsoleColor.White;

                //aqui ele recebe todas as opções num vetor, para cada opção ele vai desenhar elas na tela. Durante o processo de desenho, ele vai verificar o valor atual da seleção
                //e ver qual é a opção do  vetor que tem esse valor, ai ele vai destacar ele com ">>" e uma cor diferente

                //Nesse vetor e sistema de escrita, ele escreve do primeiro ao ultimo, oque significa que o numero 0 sempre vai ser o primeiro a ser escrito, então quanto maior o 
                //valor da escolha, mais baixo ele vai ta no desenho(valor desce, ele sobe e valor cresce, ele desce)

                Console.WriteLine();

                for (int i = 0; i < opcoes.Length; i++)
                {
                    //verifica se essa é a opçao correspondente. Sim? Pinta de cinza escuro. Não? Pinta de branco
                    Console.ForegroundColor = (i == option) ? ConsoleColor.Green : ConsoleColor.White;

                    //verifica se essa é a opçao correspondente. Sim? Poem ">>" na frente. Não? deixa um espaco vazio qualquer
                    CentralizarTexto($"{(i == option ? ">>" : "  ")} {opcoes[i]}");
                }

                Console.WriteLine();
                Console.ResetColor();
                CentralizarTexto("==============================================================================================================");

                //Ele vai ler qual é a tecla que vc pressionou
                ConsoleKeyInfo key = Console.ReadKey(true);

                //aqui ele verifica se a tecla selecionada corresponde a algo
                switch (key.Key)
                {
                    //Se clicou em flecha pra cima, o valor da opção desce um na lista(ja que o vetor começa no zero e vai sendo escrito de cima pra baixo.
                    case ConsoleKey.UpArrow:
                        if (option > 0) option--;
                        else option = opcoes.Length - 1;
                        break;

                    //se clicou em flecha pra baixo, o valor da opção sobe(limite sendo o tamanho maximo de opções)
                    case ConsoleKey.DownArrow:
                        if (option < opcoes.Length - 1) option++;
                        else option = 0;
                        break;

                    case ConsoleKey.Enter:
                        selecionado = true;
                        break;
                }
            }

            return option;
        }

        public static void MostrarVenceuBatalha(Batalha batalha)
        {
            Console.Clear();

            Console.WriteLine("\n\n\n\n");
            TextoController.CentralizarTexto("=============================== ! Você Venceu ! ==============================\n\n");

            TextoController.CentralizarTexto($"{batalha.Jogador.Nome} venceu a batalha contra os inimigos!\n\n\n");

            TextoController.CentralizarTexto($"Aperte qualquer tecla para continuar...");

            Console.ReadKey();
        }

        //Serve pra centralizar qualquer texto na tela
        public static void CentralizarTexto(string texto)
        {
            //pega o tamanho da tela
            int larguraConsole = Console.WindowWidth;

            //define a posição que o texto deve começar com base na formula. Largura da Tela - O tamanho do texto que vai ser centralizado
            //Nisso ele ve o tanto que o texto vai ocupar na tela e dividindo esse valor por 2 que vc faz ele ficar centralizado
            int posicaoX = (larguraConsole - texto.Length) / 2;

            if (posicaoX < 0) posicaoX = 0; // Evita erro se o texto for maior que a largura

            //deixa a posição do cursor na posição, para o texto começar a ser escrito a partir dela, ficnado centralizado
            Console.SetCursorPosition(posicaoX, Console.CursorTop);
            Console.WriteLine(texto);
        }

        //Faz a mesma coisa que o cnetralizar texto, mas n quebra a linha
        public static void CentralizarLinha(string texto)
        {
            int larguraConsole = Console.WindowWidth;
            int posicaoX = (larguraConsole - texto.Length) / 2;

            if (posicaoX < 0) posicaoX = 0; // Evita erro se o texto for maior que a largura

            Console.SetCursorPosition(posicaoX, Console.CursorTop);
            Console.Write(texto);
        }

        //Codigo usada pra deixar os textos coloridos com base na condição especial dele!
        public static void DefinirCorDaCondicao(string nomeCondicao)
        {
            switch (nomeCondicao)
            {
                case "Veneno":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "Sangramento":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case "Maldição":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case "Atordoamento":
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                case "Silêncio":
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case "Queimadura":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
            }
        }

        //Codigo usado para deixar as cores das cartas coloridas com base na raridade
        public static void DefinirCorDaCarta(Raridade raridade)
        {
            int raridadeInt = ((int)raridade);

            switch (raridadeInt)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
            }
        }

    }
}
