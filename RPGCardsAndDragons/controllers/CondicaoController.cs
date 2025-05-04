using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.ClassesCondicoes;
using CardsAndDragonsJogo;
using RPGCardsAndDragons.condicoes.doencas;
using RPGCardsAndDragons.doencas;

namespace CardsAndDragons.Controllers
{
    //é a classe estatica responsavel por lidar com tudo referente as condições especiais do jogo(aplicar, atualizar e verificar)


    public static class CondicaoController
    {

        //CODIGO PRINCIPAL PARA APLICAR E ATUALIZAR AS CONDIÇÕES ESPECIAIS DO JOGO 
        public static void AplicarOuAtualizarCondicao(ICondicaoTemporaria nova, List<ICondicaoTemporaria> condicoes)
        {
            //cria uma condição
            var existente = condicoes.FirstOrDefault(c => c.GetType() == nova.GetType());

            //verifica se essa condição já existe. Não? Aplica ela. Sim? E é empilhavel? Funde as duas.
            //Mas e se ela já existir e não foi empilhavel? Nada acontece, a condição não é duplicada
            if (existente == null)
            {
                condicoes.Add(nova);
            }
            else if (existente is ICondicaoEmpilhavel empilhavel)
            {
                empilhavel.Fundir(nova);
            }
        }

        #region Criar Doença

        public static Doenca IncubarDoenca()
        {
            TipoDoenca tipoDoenca = EscolherTipoDoenca();

            int nivel = 3;


            //verifica se o jogador quer uma doença agressiva ou não
            bool Eagrassiva = EscolherAgressividade();

            int duracao = 10;

            ITipoTransmissao transmissao = EscolherTipoTransmissao(tipoDoenca);

            List<IEfeitoDoenca> efeitos = EscolherEfeitosDoenca(tipoDoenca);

            string nome = EscolherNome();

            return new Doenca(tipoDoenca, nivel, Eagrassiva, duracao, transmissao, efeitos, nome);
        }

        //Escolhe o tipo de doença
        public static TipoDoenca EscolherTipoDoenca()
        {
            //variaveis que fazem a seleção funcionar
            List<TipoDoenca> tiposDoencaDisponivel = TipoDoencaAjudante.ObterTodasOsTipoDoencaDisponiveis();

            int opcaoSelecionada = 0;
            bool selecionado = false;

            //tira o cursor da tela
            Console.CursorVisible = false;

            //manter o menu funcionando em quanto vc n selecionar algo
            while (!selecionado)
            {
                Console.Clear();

                Console.WriteLine("\n\n\n");
                TextoController.CentralizarTexto("====================================== INCUBANDO DOENÇA ==================================");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                TextoController.CentralizarTexto("   Escolha o tipo da sua doenca:\n");
                TextoController.CentralizarTexto("   O tipo dela vai definir sua gama de efeitos e forma de transmissão\n\n\n");

                //é oque mostra o menu na tela com base no EspecieHelper e permite vc ver qual especie vc ta selecionando
                for (int i = 0; i < tiposDoencaDisponivel.Count; i++)
                {
                    if (i == opcaoSelecionada)
                    {

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        TextoController.CentralizarTexto($">> {tiposDoencaDisponivel[i].Nome}");
                        TextoController.CentralizarTexto($" -- {tiposDoencaDisponivel[i].Descricao}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        TextoController.CentralizarTexto($"   {tiposDoencaDisponivel[i].Nome}");
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
                        else opcaoSelecionada = tiposDoencaDisponivel.Count - 1;
                        break;
                    //se clicar pra baixo desce pra opção de baixo e ajusta o valor pra combinar com ela na lista
                    case ConsoleKey.DownArrow:
                        if (opcaoSelecionada < tiposDoencaDisponivel.Count - 1) opcaoSelecionada++;
                        else opcaoSelecionada = 0;
                        break;

                    case ConsoleKey.Enter:
                        selecionado = true;
                        break;

                }

            }

            TextoController.CentralizarTexto($"Você escolheu {tiposDoencaDisponivel[opcaoSelecionada].Nome} como tipo da doença\n");
            Console.ReadKey();
            return tiposDoencaDisponivel[opcaoSelecionada];
        }

        //Escolhe os efeitos da doença
        public static List<IEfeitoDoenca> EscolherEfeitosDoenca(TipoDoenca tipoDoenca)
        {
            List<IEfeitoDoenca> efeitos = new List<IEfeitoDoenca>();
            List<IEfeitoDoenca> efeitosDisponiveis = tipoDoenca.CriarEfeitos();

            int opcaoSelecionada = 0;
            bool selecionado = false;
            bool continuar = true;

            Console.CursorVisible = false;

            while (continuar)
            {
                while (!selecionado && efeitosDisponiveis.Count > 0)
                {
                    Console.Clear();

                    Console.WriteLine("\n\n\n");
                    TextoController.CentralizarTexto("====================================== INCUBANDO DOENÇA ==================================");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    TextoController.CentralizarTexto("   Escolha seus sintomas:\n");
                    TextoController.CentralizarTexto("   Quanto maior o preço final, mais caro será de produzir a doença\n\n\n");

                    for (int i = 0; i < efeitosDisponiveis.Count; i++)
                    {
                        if (i == opcaoSelecionada)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            TextoController.CentralizarTexto($">> {efeitosDisponiveis[i].Nome}");
                            TextoController.CentralizarTexto($" -- {efeitosDisponiveis[i].Descricao}");
                            TextoController.CentralizarTexto($" -- {tipoDoenca.ObterCustoEfeito(efeitosDisponiveis[i])}");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            TextoController.CentralizarTexto($"   {efeitosDisponiveis[i].Nome}");
                        }
                    }

                    Console.WriteLine();
                    Console.ResetColor();
                    TextoController.CentralizarTexto("============================================================================================================");

                    ConsoleKeyInfo tecla = Console.ReadKey(true);

                    switch (tecla.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (opcaoSelecionada > 0) opcaoSelecionada--;
                            else opcaoSelecionada = efeitosDisponiveis.Count - 1;
                            break;

                        case ConsoleKey.DownArrow:
                            if (opcaoSelecionada < efeitosDisponiveis.Count - 1) opcaoSelecionada++;
                            else opcaoSelecionada = 0;
                            break;

                        case ConsoleKey.Enter:
                            selecionado = true;
                            efeitos.Add(efeitosDisponiveis[opcaoSelecionada]);
                            efeitosDisponiveis.RemoveAt(opcaoSelecionada);

                            // Ajusta o índice para evitar acesso inválido
                            if (opcaoSelecionada >= efeitosDisponiveis.Count)
                            {
                                opcaoSelecionada = efeitosDisponiveis.Count - 1;
                            }
                            break;
                    }
                }

                if (efeitosDisponiveis.Count > 0)
                {
                    TextoController.CentralizarTexto($"Você escolheu {efeitos[efeitos.Count - 1].Nome} como sintoma da doença\n");
                    Console.ReadKey();

                    while (true)
                    {
                        TextoController.CentralizarTexto($"Deseja continuar? (s/n)");
                        TextoController.CentralizarLinha("- ");
                        string continuarInput = Console.ReadLine().ToLower();

                        if (continuarInput == "s")
                        {
                            continuar = true;
                            selecionado = false;
                            break;
                        }
                        else if (continuarInput == "n")
                        {
                            continuar = false;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Opção inválida. Tente novamente.");
                        }
                    }
                }
                else
                {
                    TextoController.CentralizarTexto($"Você não pode adicionar mais efeitos à doença");
                    Console.ReadKey();
                    break;
                }
            }

            return efeitos;
        }


        //Escolhe o tipo de transmissão da doença
        public static ITipoTransmissao EscolherTipoTransmissao(TipoDoenca tipoDoenca)
        {
            //variaveis que fazem a seleção funcionar
            List<ITipoTransmissao> tiposTransmisssaoDisponivel = tipoDoenca.CriarTransmissoes();

            int opcaoSelecionada = 0;
            bool selecionado = false;

            //tira o cursor da tela
            Console.CursorVisible = false;

            //manter o menu funcionando em quanto vc n selecionar algo
            while (!selecionado)
            {
                Console.Clear();

                Console.WriteLine("\n\n\n");
                TextoController.CentralizarTexto("====================================== INCUBANDO DOENÇA ==================================");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                TextoController.CentralizarTexto("   Escolha o tipo da sua doenca:\n");
                TextoController.CentralizarTexto("   O tipo dela vai definir sua gama de efeitos e forma de transmissão\n\n\n");

                //é oque mostra o menu na tela com base no EspecieHelper e permite vc ver qual especie vc ta selecionando
                for (int i = 0; i < tiposTransmisssaoDisponivel.Count; i++)
                {
                    if (i == opcaoSelecionada)
                    {

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        TextoController.CentralizarTexto($">> {tiposTransmisssaoDisponivel[i].Nome}");
                        TextoController.CentralizarTexto($" -- {tiposTransmisssaoDisponivel[i].Descricao}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        TextoController.CentralizarTexto($"   {tiposTransmisssaoDisponivel[i].Nome}");
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
                        else opcaoSelecionada = tiposTransmisssaoDisponivel.Count - 1;
                        break;
                    //se clicar pra baixo desce pra opção de baixo e ajusta o valor pra combinar com ela na lista
                    case ConsoleKey.DownArrow:
                        if (opcaoSelecionada < tiposTransmisssaoDisponivel.Count - 1) opcaoSelecionada++;
                        else opcaoSelecionada = 0;
                        break;

                    case ConsoleKey.Enter:
                        selecionado = true;
                        break;

                }

            }

            TextoController.CentralizarTexto($"Você escolheu {tiposTransmisssaoDisponivel[opcaoSelecionada].Nome} como forma de transmissão da doença\n");
            Console.ReadKey();
            return tiposTransmisssaoDisponivel[opcaoSelecionada];
        }

        public static bool EscolherAgressividade()
        {
            bool Eagrassiva = false;


            Console.Clear();

            Console.WriteLine("\n\n\n");
            TextoController.CentralizarTexto("====================================== INCUBANDO DOENÇA ==================================");

            while (true)
            {
                TextoController.CentralizarTexto($"Deseja que a doença seja agressiva? (s/n)");
                TextoController.CentralizarLinha("- ");
                string continuarInput = Console.ReadLine().ToLower();
                if (continuarInput == "s")
                {
                    Eagrassiva = true;
                    break;
                }
                else if (continuarInput == "n")
                {
                    Eagrassiva = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Opção inválida. Tente novamente.");
                }
            }

            return Eagrassiva;
        }

        public static string EscolherNome()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n");
            TextoController.CentralizarTexto("====================================== INCUBANDO DOENÇA ==================================");
            TextoController.CentralizarTexto($"Escolha o nome da doença: ");
            TextoController.CentralizarLinha("- ");
            string nome = Console.ReadLine();
            return nome;
        }

        #endregion

        //Verifica as condições especiais dos inimigos e aliados
        public static void Checape(Batalha batalha)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n\n\n");
            TextoController.CentralizarTexto("============================================   +CHECAPE INIMIGOS+   ============================================\n\n");
            Console.ResetColor();

            foreach (var inimigo in batalha.Inimigos)
            {
                //Remove os buffs temporarios
                inimigo.ModificadorDano = 0;
                inimigo.ModificadorDefesa = 0;
                inimigo.Escudo = 0;

                if (inimigo.Condicoes.Count <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    TextoController.CentralizarTexto($"{inimigo.Nome} não está sendo afetado por condições...\n");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    TextoController.CentralizarTexto($"{inimigo.Nome} está sendo afetado por condições!\n");

                    for (int i = inimigo.Condicoes.Count - 1; i >= 0; i--)
                    {
                        var condicao = inimigo.Condicoes[i];

                        TextoController.DefinirCorDaCondicao(condicao.Nome);

                        TextoController.CentralizarTexto($"{inimigo.Nome} sofre os efeitos de {condicao.Nome}!");

                        if (condicao is Doenca doenca)
                        {
                            doenca.AplicarEfeito(inimigo, batalha);
                            doenca.Atualizar();
                        }
                        else
                        {
                            condicao.AplicarEfeito(inimigo);
                            condicao.Atualizar();
                        }

                        if (condicao.Expirou())
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            TextoController.CentralizarTexto($"{inimigo.Nome} não está mais afetado por {condicao.Nome}.");
                            inimigo.Condicoes.RemoveAt(i);
                        }

                        Console.ResetColor();
                        Console.WriteLine();
                    }

                }

                Console.ResetColor();
            }

            foreach (var aplicador in batalha.Aplicadores)
            {
                aplicador.AplicarCondicao();
            }

            batalha.Aplicadores.Clear();

            if (batalha.Aliados.Count > 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\n\n");
                TextoController.CentralizarTexto("============================================   +CHECAPE ALIADOS+   ============================================\n\n");
                Console.ResetColor();


                foreach (var criatura in batalha.Aliados)
                {
                    //Remove os buffs temporarios
                    criatura.ModificadorDano = 0;
                    criatura.ModificadorDefesa = 0;
                    criatura.Escudo = 0;

                    if (criatura.Condicoes.Count <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        TextoController.CentralizarTexto($"{criatura.Nome} não está sendo afetado por condições...\n");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        TextoController.CentralizarTexto($"{criatura.Nome} está sendo afetado por condições!\n");

                        for (int i = criatura.Condicoes.Count - 1; i >= 0; i--)
                        {
                            var condicao = criatura.Condicoes[i];

                            TextoController.DefinirCorDaCondicao(condicao.Nome);

                            TextoController.CentralizarTexto($"{criatura.Nome} sofre os efeitos de {condicao.Nome}!");

                            condicao.AplicarEfeito(criatura);
                            condicao.Atualizar();

                            if (condicao.Expirou())
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                TextoController.CentralizarTexto($"{criatura.Nome} não está mais afetado por {condicao.Nome}.");
                                criatura.Condicoes.RemoveAt(i);
                            }

                            Console.ResetColor();
                            Console.WriteLine();
                        }

                    }

                    Console.ResetColor();
                }
            }
            BatalhaController.VerificarMorte(batalha);
        }

        //Verifica as condições especiais do jogador
        public static void Checape(Personagem jogador)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n\n\n");
            TextoController.CentralizarTexto($"============================================   +CHECAPE  {jogador.Nome}+   ============================================\n\n");
            Console.ResetColor();

            //Remove os buffs temporarios
            jogador.ModificadorDano = 0;
            jogador.ModificadorDefesa = 0;
            jogador.Escudo = 0;

            if (jogador.Condicoes.Count <= 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine();
                TextoController.CentralizarTexto($"{jogador.Nome} não está sendo afetado por condições...");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                TextoController.CentralizarTexto($"{jogador.Nome} está sendo afetado por condições!\n");

                for (int i = jogador.Condicoes.Count - 1; i >= 0; i--)
                {
                    var condicao = jogador.Condicoes[i];

                    TextoController.DefinirCorDaCondicao(condicao.Nome);

                    TextoController.CentralizarTexto($"{jogador.Nome} sofre os efeitos de {condicao.Nome}!");

                    condicao.AplicarEfeito(jogador);
                    condicao.Atualizar();

                    if (condicao.Expirou())
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        TextoController.CentralizarTexto($"{jogador.Nome} não está mais afetado por {condicao.Nome}.");
                        jogador.Condicoes.RemoveAt(i);
                    }

                    Console.ResetColor();
                    Console.WriteLine();
                }



                Console.ResetColor();
            }
        }

        //mostra as condições especiais que o jogador tem no momento(elas já vem formatas pelo ToString delas)
        public static void ExibirCondicoes(Personagem jogador)
        {
            if (jogador.Condicoes.Count > 0)
                for (int i = jogador.Condicoes.Count - 1; i >= 0; i--)
                {
                    var condicao = jogador.Condicoes[i];

                    TextoController.DefinirCorDaCondicao(condicao.Nome);

                    TextoController.CentralizarTexto($"{condicao}\n");

                    Console.ResetColor();
                }
        }

        //mostra as condições especiais que o inimigo/aliado tem no momento(elas já vem formatas pelo ToString delas)
        public static void ExibirCondicoes(OInimigo inimigo)
        {
            if (inimigo.Condicoes.Count > 0)
                for (int i = inimigo.Condicoes.Count - 1; i >= 0; i--)
                {
                    var condicao = inimigo.Condicoes[i];

                    TextoController.DefinirCorDaCondicao(condicao.Nome);

                    TextoController.CentralizarTexto($"{condicao}");

                    Console.ResetColor();
                }
        }

        //usado para ver se uma condição específica está ativa
        public static bool VerificarCondicao<T>(List<ICondicaoTemporaria> condicoes) where T : ICondicaoTemporaria
        {
            return condicoes.Any(condicao => condicao is T tCondicao && tCondicao.Nivel > 0 && tCondicao.Duracao > 0);
        }

        //Verifica se o inimigo/aliado esta sangrando no turno dele. Sim? Aplica efeito apos atacar
        public static void SangrarFerida(ICriaturaCombatente criatura)
        {
            foreach (var condicao in criatura.Condicoes)
            {
                if (condicao is Sangramento sangramento)
                {
                    if (sangramento.Nivel > 0 && sangramento.Duracao > 0)
                        sangramento.AplicarEfeito(criatura);
                }
            }
        }

        //Verifica se o jogador esta sangrando no turno dele. Sim? Aplica efeito apos ser atacado
        public static void SangrarFerida(Personagem jogador)
        {
            foreach (var condicao in jogador.Condicoes)
            {
                if (condicao is Sangramento sangramento)
                {
                    if (sangramento.Nivel > 0 && sangramento.Duracao > 0)
                        sangramento.AplicarEfeito(jogador);
                }
            }
        }
    }
}
