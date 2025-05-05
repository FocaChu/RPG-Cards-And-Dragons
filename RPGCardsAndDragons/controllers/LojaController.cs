using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.ClassesDasCartas;
using CardsAndDragonsJogo;

namespace CardsAndDragons.Controllers
{
    public static class LojaController
    {
        public static void ComprarCartas(Loja loja)
        {
            while(true) 
            {
                
                int escolhaCompra = LojaController.SelecionarItemEstoque(loja, 0);

                if (escolhaCompra < 0) break; // cancelou

                var carta = loja.CartasAVenda[escolhaCompra];

                if (carta.Preco < loja.Jogador.Ouro) ComprarProduto(loja, carta);
                else
                {
                    Console.WriteLine("\n");
                    TextoController.CentralizarTexto("Você não tem ouro para comprar esta carta!");
                    Console.ReadKey();
                    Console.ResetColor();
                }
            }
        }
         
        public static void ComprarCura(Loja loja)
        {
            if(loja.CuraDisponivel)
            {
                while(true)
                {
                    string[] opcoesConfirmacao = { "Sim (Pagar 50 ouros)", "Não" };
                    int opcao = TextoController.MostrarMenuSelecao(false, "Comprar Cura", opcoesConfirmacao);

                    if (opcao == 0)
                    {
                        if (loja.Jogador.Ouro > 50)
                        {
                            loja.Jogador.Ouros -= 50;

                            loja.Jogador.VidaAtual = loja.Jogador.VidaMax;
                            Console.WriteLine();
                            TextoController.CentralizarTexto($"{loja.Jogador.Nome} foi curado completamente\n");
                            loja.CuraDisponivel = false;

                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Red;
                            TextoController.CentralizarTexto($"Você não possui ouro suficiente\n");
                            Console.ResetColor();

                            Console.ReadKey();
                        }
                    }
                    else break;
                }
            }
            else
            {
                Console.WriteLine();
                TextoController.CentralizarTexto("Você já comprou cura nessa loja\n");

                Console.ReadKey();
            }

        }

        public static bool FecharLoja()
        {
            string[] opcoesConfirmacao = { "Sim", "Não" };
            int opcao = TextoController.MostrarMenuSelecao(false, "Sair da Loja", opcoesConfirmacao);

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.White;

            if (opcao == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region Funções que cuidam do Estoque das Lojas
        public static List<ICartaUsavel> ObterTodasCartasDisponiveis(Loja loja)
        {
            List<ICartaUsavel> cartas = new List<ICartaUsavel>
            {
                
                #region Cartas Lendárias e Profanas

                FabricaDeCartas.CriarAbracoDaMariposa(),
                FabricaDeCartas.CriarCoremataRuinoso(),
                FabricaDeCartas.CriarFuriaDoDragao(),

                #endregion

                #region Cartas Raras
                
                FabricaDeCartas.CriarMimico(),
                FabricaDeCartas.CriarSilenciar(),
                FabricaDeCartas.CriarFeiticoDeGelo(),
                FabricaDeCartas.CriarExplosaoDeEnergia(),
                FabricaDeCartas.CriarGolpePesado(),
                FabricaDeCartas.CriarSangueExplosivo(),
                FabricaDeCartas.CriarGolpeEmpoderado(),
                FabricaDeCartas.CriarEscudoDeEspinhos(),
                FabricaDeCartas.CriarProtecao(),
                FabricaDeCartas.CriarCompraPoderosa(),
                FabricaDeCartas.CriarPurificacao(),

                #endregion

                #region Cartas Comuns

                FabricaDeCartas.CriarPocaoVenenosa(),
                FabricaDeCartas.CriarExplosaoImprudente(),
                FabricaDeCartas.CriarBombardeio(),
                FabricaDeCartas.CriarMaldicaoDaLua(),
                FabricaDeCartas.CriarAtacarFerida(),
                FabricaDeCartas.CriarEscudo(),
                FabricaDeCartas.CriarPocaoDeCura(),
                FabricaDeCartas.CriarPocaoDeStamina(),
                FabricaDeCartas.CriarPocaoDeMana(),
                FabricaDeCartas.CriarPocaoFuriosa(),

                #endregion
            };

            #region Cartas exclusivas

            switch (loja.Jogador.Classe.NomeClasse)
            {
                case "Arqueiro":
                    cartas.Add(FabricaDeCartas.CriarSaraivada());
                    cartas.Add(FabricaDeCartas.CriarTiroMultiplo());
                    cartas.Add(FabricaDeCartas.CriarDisparoPerfurante());

                    cartas.Add(FabricaDeCartas.CriarFlechaAfiada());
                    cartas.Add(FabricaDeCartas.CriarFlechaEnvenenada());
                    cartas.Add(FabricaDeCartas.CriarBemMunido());
                    break;

                case "Doutor":
                    cartas.Add(FabricaDeCartas.CriarInvocarDoencaFixa());
                    cartas.Add(FabricaDeCartas.CriarSacrificarServo());

                    cartas.Add(FabricaDeCartas.CriarSortearDestino());
                    break;

                case "Engenheiro":
                    cartas.Add(FabricaDeCartas.CriarInvocarRoboFixo());
                    cartas.Add(FabricaDeCartas.CriarRoboTemporario());
                    cartas.Add(FabricaDeCartas.CriarReparos());
                    break;

                case "Guerreiro":
                    cartas.Add(FabricaDeCartas.CriarEspadaEEscudo());
                    break;

                case "Mago":
                    cartas.Add(FabricaDeCartas.CriarLivroDeFeiços());
                    cartas.Add(FabricaDeCartas.CriarPraga());
                    cartas.Add(FabricaDeCartas.CriarExplosaoArcana());

                    cartas.Add(FabricaDeCartas.CriarMaldicaoAmarga());
                    cartas.Add(FabricaDeCartas.CriarFogoMagico());
                    break;

                case "Necromante":
                    cartas.Add(FabricaDeCartas.CriarRessureicao());
                    cartas.Add(FabricaDeCartas.CriarCuidadosPosMortem());
                    cartas.Add(FabricaDeCartas.CriarPraga());
                    cartas.Add(FabricaDeCartas.CriarSacrificarServo());

                    cartas.Add(FabricaDeCartas.CriarMaldicaoAmarga());
                    break;
            };

            #endregion

            // Aqui tem a lista com todas as cartas disponiveis
            return cartas;
        }

        #endregion

        #region Funções que cuidam da exibição das lojas

        public static int MenuLoja(Loja loja)
        {
            int acao = 0;
            bool selecionado = false;
            int totalOpcoes = loja.CartasAVenda.Count;

            Console.CursorVisible = false;

            string[] opcoesLoja = { "Comprar Cartas", "Se Curar", "Ver Inventário", "Sair" };

            while (!selecionado)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("\n");
                TextoController.CentralizarTexto(@"=======================================  $ LOJA $  ====================================" + "\n\n");

                for (int i = 0; i < opcoesLoja.Length; i++)
                {
                    Console.ForegroundColor = (i == acao) ? ConsoleColor.DarkGray : ConsoleColor.White;
                    TextoController.CentralizarTexto($"{(i == acao ? ">>" : "  ")} {opcoesLoja[i]}");
                }
                Console.ResetColor();
                Console.WriteLine("\n\n");
                TextoController.CentralizarTexto("============================================================================================================");

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (acao > 0) acao--;
                        else acao = opcoesLoja.Length - 1;
                        break;

                    case ConsoleKey.DownArrow:
                        if (acao < opcoesLoja.Length - 1) acao++;
                        else acao = 0;
                        break;

                    case ConsoleKey.Enter:
                        selecionado = true;
                        break;
                }
            }
            return acao;

        }

        public static void MostrarEstoque(Loja loja, int opcao)
        {
            int totalOpcoes = loja.CartasAVenda.Count;
            int larguraModelo = loja.CartasAVenda.First().Modelo[0].Length;
            int espacoEntre = 2;

            int larguraTotal = totalOpcoes * larguraModelo + (totalOpcoes - 1) * espacoEntre;
            int margemEsquerda = (Console.WindowWidth - larguraTotal) / 2;

            for (int linha = 0; linha < 10; linha++)
            {
                Console.SetCursorPosition(margemEsquerda, Console.CursorTop); // centraliza a linha atual

                for (int i = 0; i < totalOpcoes; i++)
                {
                    Console.ForegroundColor = (i == opcao) ? ConsoleColor.Red : ConsoleColor.Gray;

                    Console.Write(loja.CartasAVenda[i].Modelo[linha]);

                    // Espaço entre cartas, exceto após a última
                    if (i < totalOpcoes - 1)
                        Console.Write(new string(' ', espacoEntre));
                }
                Console.WriteLine();
            }
            Console.ResetColor();

            TextoController.DefinirCorDaCarta(loja.CartasAVenda[opcao].RaridadeCarta);

            TextoController.CentralizarTexto($"Carta - {loja.CartasAVenda[opcao].Nome}");
            TextoController.CentralizarTexto($"Efeito - {loja.CartasAVenda[opcao].Descricao}");

            Console.ForegroundColor = loja.CartasAVenda[opcao].Preco > loja.Jogador.Ouro ? ConsoleColor.Red : ConsoleColor.Green;

            TextoController.CentralizarTexto($"Preço - {loja.CartasAVenda[opcao].Preco}");

            TextoController.DefinirCorDaCarta(loja.CartasAVenda[opcao].RaridadeCarta);

            Console.ResetColor();

            Console.WriteLine();

            TextoController.CentralizarTexto("Custo:");
            if (loja.CartasAVenda[opcao].CustoVida > 0) TextoController.CentralizarTexto($" - {loja.CartasAVenda[opcao].CustoVida} de Vida");
            if (loja.CartasAVenda[opcao].CustoStamina > 0) TextoController.CentralizarTexto($" - {loja.CartasAVenda[opcao].CustoStamina} de Stamina");
            if (loja.CartasAVenda[opcao].CustoMana > 0) TextoController.CentralizarTexto($" - {loja.CartasAVenda[opcao].CustoMana} de Mana");
            if (loja.CartasAVenda[opcao].CustoOuro > 0) TextoController.CentralizarTexto($" - {loja.CartasAVenda[opcao].CustoOuro} de Ouro");
            Console.WriteLine("\n");
        }

        #endregion

        #region Funções que cuidam das compras feitas nas lojas

        public static int SelecionarItemEstoque(Loja loja, int option)
        {
            bool selecionado = false;
            int totalOpcoes = loja.CartasAVenda.Count;
            bool voltar = false;

            while (!selecionado)
            {
                Console.Clear();

                Console.WriteLine("\n\n\n");
                TextoController.CentralizarTexto("====================================================   $ LOJA $   ====================================================\n");
                TextoController.CentralizarTexto($"Ouro Disponivél: {loja.Jogador.Ouros}");

                MostrarEstoque(loja, option);

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

                        if (loja.CartasAVenda.Count > 0)
                        {
                            TextoController.CentralizarTexto($"Você selecionou o carta {loja.CartasAVenda[option].Nome}.");

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

        public static void ComprarProduto(Loja loja, ICartaUsavel carta)
        {
            Console.WriteLine();
            TextoController.CentralizarTexto($"Você comprou a carta {carta.Nome} com sucesso!");
            loja.Jogador.Ouros -= carta.Preco;
            
            PersonagemController.AdicionarCartaAoBaralho(loja.Jogador, carta);

            loja.CartasAVenda.Remove(carta);


            Console.ReadKey();
            Console.ResetColor();
        }

        #endregion
    }
}
