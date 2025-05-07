using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.Aliados;
using CardsAndDragons.ClassesCondicoes;
using CardsAndDragons.Controllers;
using CardsAndDragonsJogo;
using RPGCardsAndDragons.controllers;

namespace CardsAndDragons
{
    public static class PersonagemController
    {

        #region Cria o Personagem

        public static Personagem CriarUmPersonagem()
        {
            string[] opcoesConfirmacao = { "Sim", "Não" };
            bool cancelouClasse = false;

            while (true)
            {
                cancelouClasse = false;

                // Escolha a especie e classe
                EspecieRPG especie = SelecionarEspecie();

                if (especie == null)
                {
                    return null;
                }

                ClasseRPG classe = EscolherClasse();

                if (classe == null)
                {
                    cancelouClasse = true;
                }

                if (!cancelouClasse)
                {
                    //Aqui o jogador confirma o personagem dele
                    int option = TextoController.MostrarMenuSelecao(false, "Confirmação de Personagem", opcoesConfirmacao);

                    if (option == 0)
                    {

                        // Pede o nome do personagem
                        string nome = "";
                        Console.Clear();
                        Console.WriteLine("\n\n");
                        TextoController.CentralizarTexto("============================== CRIAÇÃO DE PERSONAGEM ==============================");
                        while (nome == null || nome == string.Empty)
                        {
                            Console.WriteLine();
                            TextoController.CentralizarTexto("Nomeie o seu personagem: ");
                            TextoController.CentralizarLinha("");
                            nome = Console.ReadLine();
                        }

                        // Garante que sempre cria uma nova instância separada
                        return new Personagem(nome, especie, classe);

                    }
                    else if (option == 1)
                    {
                        int opcao = TextoController.MostrarMenuSelecao(false, "Deseja voltar ao Menu?", opcoesConfirmacao);
                        if (opcao == 0)
                        {
                            return null;
                        }
                    }
                }
            }

        }

        public static EspecieRPG SelecionarEspecie()
        {
            //mostra todas as especies
            var especiesDisponiveis = BuscaController.ObterTodasAsEspeciesDisponiveis();

            //variaveis que fazem a seleção funcionar
            int opcaoSelecionada = 0;
            bool selecionado = false;

            //tira o cursor da tela
            Console.CursorVisible = false;

            //manter o menu funcionando em quanto vc n selecionar algo
            while (!selecionado)
            {
                Console.Clear();

                Console.WriteLine("\n\n\n");
                TextoController.CentralizarTexto("====================================== CRIAÇÃO DE PERSONAGEM ==================================");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                TextoController.CentralizarTexto("   Escolha sua Espécie:");
                Console.WriteLine();
                Console.WriteLine();

                //é oque mostra o menu na tela com base no EspecieHelper e permite vc ver qual especie vc ta selecionando
                for (int i = 0; i < especiesDisponiveis.Count; i++)
                {
                    if (i == opcaoSelecionada)
                    {

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        TextoController.CentralizarTexto($">> {especiesDisponiveis[i].NomeEspecie}");
                        TextoController.CentralizarTexto($" -- {especiesDisponiveis[i].DescricaoEspecie}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        TextoController.CentralizarTexto($"   {especiesDisponiveis[i].NomeEspecie}");
                    }

                }
                Console.WriteLine();
                Console.ResetColor();
                TextoController.CentralizarTexto("============================================================================================================");
                //pega a tecla que vc apertou pro switch
                ConsoleKeyInfo tecla = Console.ReadKey(true);

                switch (tecla.Key)
                {
                    //se cliclar pra cima sobe pra opção de cima e ajusta o valor pra acompanhar ela na lista
                    case ConsoleKey.UpArrow:
                        if (opcaoSelecionada > 0) opcaoSelecionada--;
                        else opcaoSelecionada = especiesDisponiveis.Count - 1;
                        break;
                    //se clicar pra baixo desce pra opção de baixo e ajusta o valor pra combinar com ela na lista
                    case ConsoleKey.DownArrow:
                        if (opcaoSelecionada < especiesDisponiveis.Count - 1) opcaoSelecionada++;
                        else opcaoSelecionada = 0;
                        break;

                    case ConsoleKey.Enter:
                        selecionado = true;
                        break;
                    //Ao clicar em enter ele para o codigo e pega a especie correspondente ao valor escolhido(que é definido por opcaoSelecionada)

                    case ConsoleKey.Escape:
                        selecionado = true;
                        return null;
                        //Serve para encerrar o codigo, mas n deixar ele continuar e forçar ele a voltar.
                }
            }

            //se vc cliclou enter vem pra ca normal e devolve a especie escolhida
            Console.Clear();
            Console.WriteLine("\n\n\n\n\n");
            TextoController.CentralizarTexto("============================================================================================================");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine();
            TextoController.CentralizarTexto($"Você escolheu a Espécie: {especiesDisponiveis[opcaoSelecionada].NomeEspecie}");
            Console.ResetColor();
            Console.WriteLine();
            TextoController.CentralizarTexto("============================================================================================================");
            Console.ReadKey();
            Console.Clear();

            return especiesDisponiveis[opcaoSelecionada];

        }

        public static ClasseRPG EscolherClasse()
        {
            //Mostra todas as classes
            var classesDisponiveis = BuscaController.ObterTodasAsClassesDisponiveis();

            //variaveis que fazem a seleção funcionar
            int opcaoSelecionada = 0;
            bool selecionado = false;

            //tira o cursor da tela
            Console.CursorVisible = false;

            //manter o menu funcionando em quanto vc n selecionar algo
            while (!selecionado)
            {
                Console.Clear();
                Console.WriteLine("\n\n\n");
                TextoController.CentralizarTexto("====================================== CRIAÇÃO DE PERSONAGEM ==================================");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                TextoController.CentralizarTexto("   Escolha sua Classe:");
                Console.WriteLine();
                Console.WriteLine();

                //é oque mostra o menu na tela com base no ClasseHelper e permite vc ver qual especie vc ta selecionando
                for (int i = 0; i < classesDisponiveis.Count; i++)
                {
                    if (i == opcaoSelecionada)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        TextoController.CentralizarTexto($">> {classesDisponiveis[i].NomeClasse}");
                        TextoController.CentralizarTexto($" -- {classesDisponiveis[i].DescricaoClasse}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        TextoController.CentralizarTexto($"   {classesDisponiveis[i].NomeClasse}");
                    }
                }
                Console.WriteLine();
                Console.ResetColor();
                TextoController.CentralizarTexto("============================================================================================================");

                //pega a tecla que vc apertou pro switch
                ConsoleKeyInfo tecla = Console.ReadKey(true);

                switch (tecla.Key)
                {
                    //se cliclar pra cima sobe pra opção de cima e ajusta o valor pra acompanhar ela na lista
                    case ConsoleKey.UpArrow:
                        if (opcaoSelecionada > 0) opcaoSelecionada--;
                        else opcaoSelecionada = classesDisponiveis.Count - 1;
                        break;

                    //se clicar pra baixo desce pra opção de baixo e ajusta o valor pra combinar com ela na lista
                    case ConsoleKey.DownArrow:
                        if (opcaoSelecionada < classesDisponiveis.Count - 1) opcaoSelecionada++;
                        else opcaoSelecionada = 0;
                        break;

                    case ConsoleKey.Enter:
                        selecionado = true;
                        break;
                    //Ao clicar em enter ele para o codigo e pega a especie correspondente ao valor escolhido(que é definido por opcaoSelecionada)

                    case ConsoleKey.Escape:
                        selecionado = true;
                        return null;
                        //Serve para encerrar o codigo, mas n deixar ele continuar e forçar ele a voltar.
                }
            }

            //se vc cliclou enter vem pra ca normal e devolve a especie escolhida
            Console.Clear();
            Console.WriteLine("\n\n\n\n\n");
            TextoController.CentralizarTexto("============================================================================================================");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine();
            TextoController.CentralizarTexto($"Você escolheu a Classe: {classesDisponiveis[opcaoSelecionada].NomeClasse}");
            Console.ResetColor();
            Console.WriteLine();
            TextoController.CentralizarTexto("============================================================================================================");
            Console.ReadKey();
            Console.Clear();

            return classesDisponiveis[opcaoSelecionada];
        }

        #endregion

        #region Exibi o Personagem, suas Cartas e seus Itens

        //Mostrra o jogador e seus status, opcionalmente seu baralho

        public static void ExibirJogador(Personagem jogador, bool mostrarBaralhosAd)
        {
            bool vendo = false;

            int opcaoSelecionada = 0;
            const int cartasPorPagina = 5;
            int paginaAtual = 0;

            while (!vendo)
            {
                Console.Clear();

                int totalCartas = jogador.BaralhoCompleto.Count;
                int totalPaginas = (int)Math.Ceiling(totalCartas / (float)cartasPorPagina);

                int inicio = paginaAtual * cartasPorPagina;
                int fim = Math.Min(inicio + cartasPorPagina, totalCartas);

                Console.WriteLine("\n\n\n");
                TextoController.CentralizarTexto($"================================  < {jogador.Nome} > ================================\n\n");

                TextoController.CentralizarTexto($"Nível: {jogador.Nivel} - {jogador.XpAtual}/{jogador.XpTotal}");
                TextoController.CentralizarTexto($"Classe: {jogador.Classe.NomeClasse} || Espécie: {jogador.Especie.NomeEspecie}");
                TextoController.CentralizarTexto($"Vida: {jogador.VidaAtual}/{jogador.VidaMax}");
                TextoController.CentralizarTexto($"Mana: {jogador.ManaAtual}/{jogador.ManaMax}");
                TextoController.CentralizarTexto($"Stamina: {jogador.StaminaAtual}/{jogador.StaminaMax}");
                TextoController.CentralizarTexto($"Ouro: {jogador.Ouro}");

                CondicaoController.ExibirCondicoes(jogador);

                Console.WriteLine("\n\n\n");
                TextoController.CentralizarTexto($"=============== Baralho de {jogador.Nome} ===============\n");

                if (mostrarBaralhosAd)
                    TextoController.CentralizarTexto($"Cartas Para Compra: {jogador.BaralhoCompra.Count} | Pilha de Descarte: {jogador.BaralhoDescarte.Count}\n");

                TextoController.CentralizarTexto($"Página {paginaAtual + 1} de {totalPaginas}");

                int larguraModelo = jogador.BaralhoCompleto.First().Modelo[0].Length;
                int espacoEntre = 2;

                int cartasVisiveis = fim - inicio;
                int larguraTotal = cartasVisiveis * larguraModelo + (cartasVisiveis - 1) * espacoEntre;
                int margemEsquerda = (Console.WindowWidth - larguraTotal) / 2;

                TextoController.CentralizarTexto($"Recursos Disponíveis");
                TextoController.CentralizarTexto($"Vida: {jogador.VidaAtual} | Ouro: {jogador.Ouro} | Mana: {jogador.ManaAtual} | Stamina: {jogador.StaminaAtual}\n\n");

                for (int linha = 0; linha < 10; linha++)
                {
                    Console.SetCursorPosition(margemEsquerda, Console.CursorTop);

                    for (int i = inicio; i < fim; i++)
                    {
                        Console.ForegroundColor = (i == opcaoSelecionada) ? ConsoleColor.Red : ConsoleColor.Gray;
                        Console.Write(jogador.BaralhoCompleto[i].Modelo[linha]);

                        if (i < fim - 1)
                            Console.Write(new string(' ', espacoEntre));
                    }

                    Console.WriteLine();
                }

                Console.ResetColor();

                if (opcaoSelecionada >= 0 && opcaoSelecionada < jogador.BaralhoCompleto.Count)
                {
                    var carta = jogador.BaralhoCompleto[opcaoSelecionada];

                    TextoController.DefinirCorDaCarta(carta.RaridadeCarta);
                    TextoController.CentralizarTexto($"Carta - {carta.Nome}");
                    TextoController.CentralizarTexto($"Efeito - {carta.Descricao}");
                    Console.ResetColor();

                    Console.WriteLine();
                    TextoController.CentralizarTexto("Custo:");
                    if (carta.CustoVida > 0) TextoController.CentralizarTexto($" - {carta.CustoVida} de Vida");
                    if (carta.CustoStamina > 0) TextoController.CentralizarTexto($" - {carta.CustoStamina} de Stamina");
                    if (carta.CustoMana > 0) TextoController.CentralizarTexto($" - {carta.CustoMana} de Mana");
                    if (carta.CustoOuro > 0) TextoController.CentralizarTexto($" - {carta.CustoOuro} de Ouro");
                }

                Console.WriteLine("\n");

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (opcaoSelecionada > 0)
                            opcaoSelecionada--;
                        else
                            opcaoSelecionada = totalCartas - 1;
                        paginaAtual = opcaoSelecionada / cartasPorPagina;
                        break;

                    case ConsoleKey.RightArrow:
                        if (opcaoSelecionada < totalCartas - 1)
                            opcaoSelecionada++;
                        else
                            opcaoSelecionada = 0;
                        paginaAtual = opcaoSelecionada / cartasPorPagina;
                        break;

                    case ConsoleKey.PageUp:
                        if (paginaAtual > 0)
                        {
                            paginaAtual--;
                            opcaoSelecionada = paginaAtual * cartasPorPagina;
                        }
                        break;

                    case ConsoleKey.PageDown:
                        if (paginaAtual < totalPaginas - 1)
                        {
                            paginaAtual++;
                            opcaoSelecionada = paginaAtual * cartasPorPagina;
                        }
                        break;

                    case ConsoleKey.Escape:
                    case ConsoleKey.Enter:
                        TextoController.CentralizarTexto("Aperte qualquer tecla para prosseguir");
                        Console.ReadKey();
                        vendo = true;
                        break;
                }
            }
        }


        //Mostra todas aws cartas que o jogador tem na mão
        public static void MostrarCartasNaMao(Personagem jogador, List<ICartaUsavel> cartas, int opcao)
        {
            Console.Clear();
            Console.WriteLine("\n\n\n");
            TextoController.CentralizarTexto($"=============== Cartas de {jogador.Nome} ===============\n");

            if (cartas.Count == 0)
            {
                TextoController.CentralizarTexto("Sua mão está vazia.");
                return;
            }

            int larguraModelo = cartas.First().Modelo[0].Length;
            int espacoEntre = 2;

            int larguraTotal = cartas.Count * larguraModelo + (cartas.Count - 1) * espacoEntre;
            int margemEsquerda = (Console.WindowWidth - larguraTotal) / 2;

            TextoController.CentralizarTexto($"Recursos Disponíveis\n");
            TextoController.CentralizarTexto($"Vida: {jogador.VidaAtual} | Ouro: {jogador.Ouro} | Mana: {jogador.ManaAtual} | Stamina: {jogador.StaminaAtual}\n\n");

            CartaController.MostrarCartas(cartas, opcao);
        }

        #endregion

        #region Gerencia as Cartas do Jogador

        public static void EscolherCartaParaUsar(Batalha batalha)
        {
            Console.Clear();
            TextoController.CentralizarTexto($"============================== Cartas na Mão ==============================");

            int option = 0;

            //seleciona uma carta para usar(ou voltac om um valor negativo)
            int escolha = SelecionarCarta(batalha.Jogador, batalha.Jogador.Mao, option);

            //se o valor for posiivo ele usa a carta
            if (escolha >= 0)
            {
                batalha.Jogador.UsarCarta(batalha, escolha);
            }
            //se for negativo ele cancela a ação
            else
            {
                Console.WriteLine();
                TextoController.CentralizarTexto("Ação cancelada.");
                Console.ReadKey();
            }
        }

        public static int SelecionarCarta(Personagem jogador, List<ICartaUsavel> cartas, int option)
        {
            bool selecionado = false;
            int totalOpcoes = cartas.Count;
            bool voltar = false;

            while (!selecionado)
            {
                Console.Clear();

                TextoController.CentralizarTexto("==========================   !COMBATE!   ==========================");

                MostrarCartasNaMao(jogador, cartas, option);

                Console.ResetColor();
                Console.WriteLine("\n");
                TextoController.CentralizarTexto(@"Use <- -> para navegar | ENTER para selecionar e ESC para sair.");

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (option > 0) option--;
                        else option = totalOpcoes - 1;
                        break;

                    case ConsoleKey.RightArrow:
                        if (option < totalOpcoes - 1) option++;
                        else option = 0;
                        break;

                    case ConsoleKey.Enter:
                        Console.ForegroundColor = ConsoleColor.Yellow;

                        if (cartas.Count > 0)
                        {
                            TextoController.CentralizarTexto($"Você selecionou o carta {cartas[option].Nome}.");

                            Console.WriteLine("\n");
                            TextoController.CentralizarTexto("Aperte ENTER para confirmar, qualquer outra tecla para voltar.\n");

                            if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                            {
                                selecionado = true;
                            }
                        }
                        else
                        {
                            selecionado = true;
                            voltar = true;
                        }
                        break;

                    case ConsoleKey.Escape:
                        selecionado = true;
                        voltar = true;
                        TextoController.CentralizarTexto("Voltando....");
                        break;
                }
            }
            if (voltar) return -1;
            else return option;
        }

        //embaralha as cartas do jogador
        public static Queue<ICartaUsavel> EmbaralharCartas(List<ICartaUsavel> cartas)
        {
            // Embaralha a lista de cartas e coloca na fila
            var cartasEmbaralhadas = cartas.OrderBy(c => Guid.NewGuid()).ToList();
            return new Queue<ICartaUsavel>(cartasEmbaralhadas);
        }

        //Compra as cartas pra mão do jogador
        public static void ComprarCartas(Personagem jogador)
        {
            Console.Clear();

            List<ICartaUsavel> cartasCompradas = new List<ICartaUsavel>();

            while (jogador.Mao.Count < 10 && jogador.BaralhoCompra.Count > 0)
            {
                var cartaComprada = jogador.BaralhoCompra.Dequeue();
                jogador.Mao.Add(cartaComprada);
                cartasCompradas.Add(cartaComprada);
            }

            if (jogador.BaralhoCompra.Count == 0)
            {
                TextoController.CentralizarTexto("Baralho vazio. Reciclando descarte...");
                jogador.BaralhoCompra = PersonagemController.EmbaralharCartas(jogador.BaralhoDescarte);
                jogador.BaralhoDescarte.Clear();
            }

            if (cartasCompradas.Count == 0)
            {
                Console.WriteLine("\n\n\n");
                TextoController.CentralizarTexto("========================= Nenhuma Carta Foi Comprada =========================");
                Console.ReadKey(true);
                return;
            }

            int index = 0;
            ConsoleKey tecla;

            do
            {
                Console.Clear();
                Console.WriteLine("\n\n\n");
                TextoController.CentralizarTexto("========================= Cartas Compradas Neste Turno =========================");
                Console.WriteLine();
                CartaController.MostrarCartas(cartasCompradas, index);

                Console.WriteLine();
                TextoController.CentralizarTexto($"Carta {index + 1} de {cartasCompradas.Count}");
                TextoController.CentralizarTexto("     Use <- -> para navegar | ESC ou ENTER para continuar");
                tecla = Console.ReadKey(true).Key;

                if (tecla == ConsoleKey.RightArrow)
                    index = (index + 1) % cartasCompradas.Count;
                else if (tecla == ConsoleKey.LeftArrow)
                    index = (index - 1 + cartasCompradas.Count) % cartasCompradas.Count;

            } while (tecla != ConsoleKey.Enter && tecla != ConsoleKey.Escape);

            Console.Clear();
        }

        public static void ComprarCartasExtras(Personagem jogador, int qtdCartas)
        {
            List<ICartaUsavel> cartasCompradas = new List<ICartaUsavel>();

            for (int i = 0; i < qtdCartas; i++)
            {
                if (jogador.BaralhoCompra.Count > 0)
                {
                    var cartaComprada = jogador.BaralhoCompra.Dequeue();
                    cartasCompradas.Add(cartaComprada);

                }
                else
                {
                    Console.WriteLine();
                    TextoController.CentralizarTexto($"{jogador.Nome} não possuia cartas para comprar");
                }
            }

            foreach (var carta in cartasCompradas)
            {
                jogador.Mao.Add(carta);
            }

            int index = 0;
            ConsoleKey tecla;

            if (cartasCompradas.Count > 0)
            {
                do
                {
                    Console.ResetColor();
                    Console.Clear();
                    Console.WriteLine("\n\n\n");
                    TextoController.CentralizarTexto("========================= Cartas Extras Compradas =========================");
                    Console.WriteLine();
                    CartaController.MostrarCartas(cartasCompradas, index);

                    Console.WriteLine();
                    TextoController.CentralizarTexto($"Carta {index + 1} de {cartasCompradas.Count}");
                    TextoController.CentralizarTexto("     Use <- -> para navegar | ESC ou ENTER para continuar");
                    tecla = Console.ReadKey(true).Key;

                    if (tecla == ConsoleKey.RightArrow)
                        index = (index + 1) % cartasCompradas.Count;
                    else if (tecla == ConsoleKey.LeftArrow)
                        index = (index - 1 + cartasCompradas.Count) % cartasCompradas.Count;

                } while (tecla != ConsoleKey.Enter && tecla != ConsoleKey.Escape);
            }
        }

        public static void RecarregarBaralho(Personagem jogador)
        {
            if (jogador.BaralhoDescarte.Count > 0)
            {
                TextoController.CentralizarTexto("Reciclando cartas do baralho...");
                jogador.BaralhoCompra = PersonagemController.EmbaralharCartas(jogador.BaralhoDescarte);
                jogador.BaralhoDescarte.Clear();
            }
            else
            {
                TextoController.CentralizarTexto("Não há mais cartas para reciclar.");
            }
        }

        //Working in progress
        public static void AdicionarCartaAoBaralho(Personagem jogador, ICartaUsavel carta)
        {
            jogador.BaralhoCompleto.Add(carta);
            jogador.BaralhoDescarte.Add(carta); // adiciona direto no descarte
        }


        #endregion

        #region Cuida da condições e da vida do Personagem
        //Restaura os status do jogador a cada rodada
        public static void RestaurarJogador(Personagem jogador)
        {
            jogador.VidaAtual = jogador.VidaAtual + jogador.Regeneracao;
            jogador.ManaAtual = jogador.ManaMax;
            jogador.StaminaAtual = jogador.StaminaMax;
        }

        #endregion
    }
}

