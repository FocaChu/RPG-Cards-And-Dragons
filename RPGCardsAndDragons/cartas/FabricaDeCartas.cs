﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragonsJogo.ClassesCartas;
using CardsAndDragonsJogo;
using CardsAndDragons.ClassesCondicoes;
using CardsAndDragons.Controllers;
using CardsAndDragons.Aliados;
using CardsAndDragons.Inimigos;
using CardsAndDragons.Cartas;
using RPGCardsAndDragons.doencas;
using RPGCardsAndDragons.condicoes.doencas.tipoDoenca;
using RPGCardsAndDragons.condicoes.doencas.transmissaoDoenca;
using RPGCardsAndDragons.cartas;
using System.Runtime.CompilerServices;

namespace CardsAndDragons.ClassesDasCartas
{
    //Classe estatica com a função de criar as cartas do jogo
    public static class FabricaDeCartas
    {
        //Todas as "public static ICartaUsavel Cria(insiraNomeDaCartaAqui)" tem a mesma função, criar uma carta generica com:
        //um nome, uma descrição, uma raridade, um modelo, um custo, um alvo e um efeito
        public static ICartaUsavel CriarSilenciar()
        {
            return new CartaGenerica
            {
                //O nome da carta
                Nome = "Silenciar",

                //A descrição dela em jogo
                Descricao = "Silencia o alvo, impedindo ele de usar sua habilidade na proxima rodada.",

                //A raridade
                RaridadeCarta = Raridade.Rara,

                Preco = GerarPreco(Raridade.Rara),

                //Seus custos
                CustoMana = 35,

                //Seu modelo
                Modelo = GerarModeloCarta("¨", 1),

                //Seu efeito
                Efeito = batalha =>
                {
                    int option = AlvoController.SelecionarAlvo(batalha.Inimigos);

                    var alvo = batalha.Inimigos[option];

                    CondicaoController.AplicarOuAtualizarCondicao(new Silencio(1, 2), alvo.Condicoes);
                    Console.WriteLine();
                    TextoController.CentralizarTexto($"{alvo.Nome} foi silênciado...");
                }
            };
        }

        public static ICartaUsavel CriarInvocarDoencaFixa()
        {
            return new CartaInvocarDoencaFixa
            {
                Nome = "Criar Doença",
                Descricao = "Invoca uma doençã customizavel permanente.",
                RaridadeCarta = Raridade.Rara,
                Preco = GerarPreco(Raridade.Rara),
                CustoMana = 80,
                CustoOuro = 40,
                Modelo = GerarModeloCarta("D", 1) // opcional
            };
        }

        #region Cartas Lendárias e Profanas

        public static ICartaUsavel CriarAbracoDaMariposa()
        {
            return new CartaGenerica
            {
                Nome = "Abraço da Mariposa",
                Descricao = "Aplica 44 de maldição e silência todos os inimigos. O jogador ganha 4 de maldição por inimigo afetado.",
                RaridadeCarta = Raridade.Profana,
                Preco = GerarPreco(Raridade.Profana),
                CustoMana = 44,
                CustoVida = 44,
                Modelo = GerarModeloCarta("M", 3),
                Efeito = batalha =>
                {
                    foreach (var inimigo in batalha.Inimigos)
                    {
                        CondicaoController.AplicarOuAtualizarCondicao(new Maldicao(4), batalha.Jogador.Condicoes);

                        CondicaoController.AplicarOuAtualizarCondicao(new Maldicao(44), inimigo.Condicoes);
                        CondicaoController.AplicarOuAtualizarCondicao(new Silencio(1, 2), inimigo.Condicoes);
                    }
                }
            };
        }

        public static ICartaUsavel CriarCoremataRuinoso()
        {
            return new CartaGenerica
            {
                Nome = "Coremata Ruinoso",
                Descricao = "Causa dano aos inimigos igual a maldição atual deles. Causa 44 de maldição ao jogador.",
                RaridadeCarta = Raridade.Profana,
                Preco = GerarPreco(Raridade.Profana),
                CustoMana = 44,
                CustoVida = 44,
                Modelo = GerarModeloCarta("~", 3),
                Efeito = batalha =>
                {
                    CondicaoController.AplicarOuAtualizarCondicao(new Maldicao(44), batalha.Jogador.Condicoes);

                    foreach (var inimigo in batalha.Inimigos)
                    {
                        foreach (var condicao in inimigo.Condicoes)
                        {
                            if (condicao.Nome == "Maldição")
                            {
                                inimigo.SofrerDano(batalha.Jogador, condicao.Nivel, false, true);
                            }
                        }
                    }
                }
            };
        }

        public static ICartaUsavel CriarFuriaDoDragao()
        {
            return new CartaGenerica
            {
                Nome = "Fúria do Dragão",
                Descricao = "Causa 50 de dano base a todos os inimigos. Para cada inimigo derrotado dessa forma ganha 10 de Ouro e 5 de XP.",
                RaridadeCarta = Raridade.Lendaria,
                Preco = GerarPreco(Raridade.Lendaria),
                CustoMana = 75,
                CustoStamina = 75,
                Modelo = GerarModeloCarta("D", 3),
                Efeito = batalha =>
                {
                    int danoFinal = 50 + batalha.Jogador.ModificadorDano;

                    int recompensa = 0;

                    foreach (var inimigo in batalha.Inimigos)
                    {
                        inimigo.SofrerDano(batalha.Jogador, danoFinal, false, true);
                        if (inimigo.VidaAtual <= 0) recompensa++;
                    }

                    batalha.Jogador.Ouros += 10 * recompensa;
                    batalha.Jogador.XpAtual += 5 * recompensa;
                }
            };
        }

        #endregion

        #region Cartas Épicas

        public static ICartaUsavel CriarFogoDaConflagracao()
        {
            return new CartaGenerica
            {
                Nome = "Fogo da Conflagração",
                Descricao = "Causa 35 de dano base a todos os inimigos. Aplica queimadura em todos os inimigos.",
                RaridadeCarta = Raridade.Epica,
                Preco = GerarPreco(Raridade.Epica),
                CustoMana = 50,
                Modelo = GerarModeloCarta("F", 3),
                Efeito = batalha =>
                {
                    int danoFinal = 35 + batalha.Jogador.ModificadorDano;
                    foreach (var inimigo in batalha.Inimigos)
                    {
                        inimigo.SofrerDano(batalha.Jogador, danoFinal, false, true);
                        CondicaoController.AplicarOuAtualizarCondicao(new Queimadura(5, 5), inimigo.Condicoes);
                    }
                }
            };
        }

        public static ICartaUsavel CriarColapsoDeMana()
        {
            return new CartaGenerica
            {
                Nome = "Colapso de Mana",
                Descricao = "Gasta toda a mana para causa dano equivalente a um inimigo.",
                RaridadeCarta = Raridade.Epica,
                Preco = GerarPreco(Raridade.Epica),
                CustoMana = 50,
                Modelo = GerarModeloCarta("M", 1),
                Efeito = batalha =>
                {
                    int danoFinal = batalha.Jogador.ManaAtual + batalha.Jogador.ModificadorDano;
                    
                    var alvo = batalha.Inimigos[AlvoController.SelecionarAlvo(batalha.Inimigos)];

                    alvo.SofrerDano(batalha.Jogador, danoFinal, false, true);

                    batalha.Jogador.ManaAtual = 0;
                }
            };
        }

        public static ICartaUsavel CriarAuraVerdejante()
        {
            return new CartaGenerica
            {
                Nome = "Aura Verdejante",
                Descricao = "Cura vida 5 de vida para cada ponto de regenaração. Aumenta a regeneração em 3 pela batalha.",
                RaridadeCarta = Raridade.Epica,
                Preco = GerarPreco(Raridade.Epica),
                CustoMana = 50,
                CustoStamina = 25,
                Modelo = GerarModeloCarta("V", 1),
                Efeito = batalha =>
                {
                    int qtdCUra = 5 * batalha.Jogador.Regeneracao;

                    batalha.Jogador.Curar(qtdCUra);

                    batalha.Jogador.Regeneracao += 3;
                }
            };
        }


        #endregion


        #region Cartas Raras



        public static ICartaUsavel CriarSubornoEstrategico()
        {
            return new CartaGenerica
            {
                Nome = "Suborno Estratégico",
                Descricao = "Paga 20 de ouro. Um inimigo aleatório pula sua próxima ação.",
                RaridadeCarta = Raridade.Rara,
                Preco = GerarPreco(Raridade.Rara),
                CustoOuro = 20,
                Modelo = GerarModeloCarta("§", 2),
                Efeito = batalha =>
                {
                    var alvo = AlvoController.EscolherInimigoAleatorio(batalha.Inimigos);

                    if (alvo.EBoss)
                    {
                        TextoController.CentralizarTexto("Você não pode subornar um chefe!");
                        batalha.Jogador.Ouro += 20;
                        return;
                    }

                    CondicaoController.AplicarOuAtualizarCondicao(new Atordoamento(), alvo.Condicoes);

                    TextoController.CentralizarTexto($"{alvo.Nome} foi subornado e perderá sua próxima ação!");
                }
            };
        }

        public static ICartaUsavel CriarInvocarRoboFixo()
        {
            return new CartaInvocarRoboFixa
            {
                Nome = "Criar Robô",
                Descricao = "Invoca um robô customizavel permanente.",
                RaridadeCarta = Raridade.Rara,
                Preco = GerarPreco(Raridade.Rara),
                CustoMana = 20,
                CustoStamina = 15,
                CustoOuro = 20,
                Modelo = GerarModeloCarta("R", 1) // opcional
            };
        }

        public static ICartaUsavel CriarRoboTemporario()
        {
            return new CartaGenerica
            {
                Nome = "Robô Provisório",
                Descricao = "Invoca um robô auxiliar temporário para esta batalha.",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Rara),
                CustoMana = 20,
                CustoStamina = 25,
                CustoOuro = 35,
                Modelo = GerarModeloCarta("r", 1),
                Efeito = batalha =>
                {
                    var robo = AliadoController.ConfigurarRobo(); // cria novo robô aleatório
                    batalha.Aliados.Add(robo);
                    TextoController.CentralizarTexto($"{robo.Nome} foi invocado para a batalha!");
                }
            };
        }

        public static ICartaUsavel CriarRessureicao()
        {
            return new CartaGenerica
            {
                Nome = "Ressureição",
                Descricao = "Reviva um inimigo morto aleatório como seu aliado.",
                RaridadeCarta = Raridade.Rara,
                Preco = GerarPreco(Raridade.Rara),
                CustoMana = 80,
                Modelo = GerarModeloCarta("9", 1),
                Efeito = batalha =>
                {
                    if (batalha.InimigosDerrotados.Count == 0)
                    {
                        TextoController.CentralizarTexto("Não há inimigos para reviver.");
                        return;
                    }

                    var inimigoEscolhido = AlvoController.EscolherInimigoAleatorio(batalha.InimigosDerrotados);
                    AliadoController.ReviverInimigo(batalha, inimigoEscolhido);
                }
            };
        }

        public static ICartaUsavel CriarPraga()
        {
            return new CartaGenerica
            {
                Nome = "Praga",
                Descricao = "Lança uma praga que Amaldiçoa e Envenena o inimigo o inimigo.",
                RaridadeCarta = Raridade.Rara,
                Preco = GerarPreco(Raridade.Rara),
                CustoMana = 40,
                Modelo = GerarModeloCarta("9", 1),
                Efeito = batalha =>
                {
                    int option = AlvoController.SelecionarAlvo(batalha.Inimigos);

                    var alvo = batalha.Inimigos[option];

                    CondicaoController.AplicarOuAtualizarCondicao(new Veneno(2, 4), alvo.Condicoes);
                    CondicaoController.AplicarOuAtualizarCondicao(new Maldicao(5), alvo.Condicoes);
                    Console.WriteLine();
                    TextoController.CentralizarTexto($"{alvo.Nome} foi amaldiçoado e envenenado!");
                }
            };
        }

        public static ICartaUsavel CriarFeiticoDeGelo()
        {
            return new CartaGenerica
            {
                Nome = "Feitiço de Gelo",
                Descricao = "Um feitiço gélido que pode congelar um inimigo o impedindo de atacar.",
                RaridadeCarta = Raridade.Rara,
                Preco = GerarPreco(Raridade.Rara),
                CustoMana = 50,
                Modelo = GerarModeloCarta("P", 1),
                Efeito = batalha =>
                {
                    int option = AlvoController.SelecionarAlvo(batalha.Inimigos);

                    var alvo = batalha.Inimigos[option];
                    CondicaoController.AplicarOuAtualizarCondicao(new Atordoamento(), alvo.Condicoes);
                    Console.WriteLine();
                    TextoController.CentralizarTexto($"{alvo.Nome} foi congelado!\n");
                }
            };
        }

        public static ICartaUsavel CriarSacrificarServo()
        {
            return new CartaGenerica
            {
                Nome = "Sacrificar Servo",
                Descricao = "Causa 30 de dano base a todos os inimigos. Consome um aliado para ser ativado",
                RaridadeCarta = Raridade.Rara,
                Preco = GerarPreco(Raridade.Rara),
                CustoVida = 10,
                CustoMana = 30,
                Modelo = GerarModeloCarta("F", 3),
                Efeito = batalha =>
                {
                    int danoFinal = 30 + batalha.Jogador.ModificadorDano;

                    int option = AlvoController.SelecionarAlvo(batalha.Aliados);

                    Console.ForegroundColor = ConsoleColor.Red;
                    TextoController.CentralizarTexto($"{batalha.Aliados[option].Nome} foi sacrificado na explosão\n");
                    Console.ResetColor();

                    batalha.Aliados.RemoveAt(option);

                    Console.Clear();
                    BatalhaController.MostrarInimigos(batalha.Inimigos, -1);
                    Console.WriteLine();

                    foreach (var inimigo in batalha.Inimigos)
                    {
                        inimigo.SofrerDano(batalha.Jogador, danoFinal, false, true);
                    }
                }
            };
        }

        public static ICartaUsavel CriarExplosaoDeEnergia()
        {
            return new CartaRecarregavel
            {
                Nome = "Explosão de Energia",
                Descricao = "Causa 53 de dano em todos os inimigos. Após 3 usos, precisa ser recarregada.",
                RaridadeCarta = Raridade.Rara,
                Preco = GerarPreco(Raridade.Rara),
                CustoMana = 30,
                Modelo = GerarModeloCarta("O", 1),

                CargasMaximas = 3,
                CargasAtuais = 3,

                EfeitoComCarga = batalha =>
                {
                    int danoFinal = 30 + batalha.Jogador.ModificadorDano;

                    foreach (var inimigo in batalha.Inimigos)
                    {
                        inimigo.SofrerDano(batalha.Jogador, danoFinal, false, true);
                    }
                    TextoController.CentralizarTexto("Todos os inimigos foram atingidos!");
                },
                EfeitoSemCarga = batalha =>
                {
                    TextoController.CentralizarTexto("A carta foi recarregada, mas não causou efeito.");
                }
            };
        }

        public static ICartaUsavel CriarExplosaoArcana()
        {
            return new CartaGenerica
            {
                Nome = "Explosão Arcana",
                Descricao = "Causa 25 de dano base a todos os inimigos.",
                RaridadeCarta = Raridade.Rara,
                Preco = GerarPreco(Raridade.Rara),
                CustoMana = 40,
                Modelo = GerarModeloCarta("*", 3),
                Efeito = batalha =>
                {
                    int danoFinal = 25 + batalha.Jogador.ModificadorDano;

                    Console.Clear();
                    BatalhaController.MostrarInimigos(batalha.Inimigos, -1);
                    Console.WriteLine();

                    foreach (var inimigo in batalha.Inimigos)
                    {
                        inimigo.SofrerDano(batalha.Jogador, danoFinal, false, true);
                    }
                }
            };
        }

        public static ICartaUsavel CriarLivroDeFeiços()
        {
            return new CartaGenerica
            {
                Nome = "Livro de Feitiços",
                Descricao = "Escolha entre 3 feitiços do livro: Gelo, Natureza e Fogo",
                RaridadeCarta = Raridade.Rara,
                Preco = GerarPreco(Raridade.Rara),
                CustoMana = 50,
                Modelo = GerarModeloCarta("3", 3),
                Efeito = batalha =>
                {
                    List<List<string>> modelos = new List<List<string>>();
                    List<string> descricoes = new List<string>();

                    List<string> modeloUm = GerarModeloCarta("<", 1);
                    modelos.Add(modeloUm);

                    List<string> modeloDois = GerarModeloCarta("^", 2);
                    modelos.Add(modeloDois);

                    List<string> modeloTres = GerarModeloCarta(">", 3);
                    modelos.Add(modeloTres);


                    string descricaoUm = "Projétil de Gelo: Causa 15 de dano a um inimigo e atordoa ele.";
                    descricoes.Add(descricaoUm);

                    string descricaoDois = "Parede de Vinha: Causa 10 de dano a um inimigo. Ele e inimigos adjacentes sofreram de veneno.";
                    descricoes.Add(descricaoDois);


                    string descricaoTres = "Rastro de Fogo: Causa 10 de dano base e aplica queimadura em todos os inimigos.";
                    descricoes.Add(descricaoTres);

                    int opcao = CartaController.MostrarOpcoes(modelos, descricoes);

                    if (opcao < 0) return;
                    else if (opcao == 0)
                    {
                        int danoFinal = 15 + batalha.Jogador.ModificadorDano;

                        int option = AlvoController.SelecionarAlvo(batalha.Inimigos);

                        var alvo = batalha.Inimigos[option];

                        alvo.SofrerDano(batalha.Jogador, danoFinal, false, true);
                        CondicaoController.AplicarOuAtualizarCondicao(new Atordoamento(), alvo.Condicoes);
                        TextoController.CentralizarTexto($"{alvo.Nome} foi atingido por Projétil de Gelo!\n");
                    }
                    else if (opcao == 1)
                    {
                        int danoFinal = 10 + batalha.Jogador.ModificadorDano;

                        int option = AlvoController.SelecionarAlvo(batalha.Inimigos);

                        var alvo = batalha.Inimigos[option];
                        alvo.SofrerDano(batalha.Jogador, danoFinal, false, true);
                        CondicaoController.AplicarOuAtualizarCondicao(new Veneno(5, 3), alvo.Condicoes);

                        if (option > 0)
                        {
                            var alvoEsquerda = batalha.Inimigos[option - 1];
                            CondicaoController.AplicarOuAtualizarCondicao(new Veneno(3, 3), alvoEsquerda.Condicoes);
                        }
                        if (option < batalha.Inimigos.Count - 1)
                        {
                            var alvoDireita = batalha.Inimigos[option + 1];
                            CondicaoController.AplicarOuAtualizarCondicao(new Veneno(3, 3), alvoDireita.Condicoes);
                        }
                    }
                    else if (opcao == 2)
                    {
                        int danoFinal = 10 + batalha.Jogador.ModificadorDano;

                        foreach (var inimigo in batalha.Inimigos)
                        {
                            inimigo.SofrerDano(batalha.Jogador, danoFinal, false, true);
                            CondicaoController.AplicarOuAtualizarCondicao(new Queimadura(3, 3), inimigo.Condicoes);
                        }
                    }
                }
            };
        }

        public static ICartaUsavel CriarSaraivada()
        {
            return new CartaGenerica
            {
                Nome = "Saraivada",
                Descricao = "Chove flechas causando 20 de dano base em um inimigo e 10 de dano base em inimigos adjacentes",
                RaridadeCarta = Raridade.Rara,
                Preco = GerarPreco(Raridade.Rara),
                CustoStamina = 30,
                Modelo = GerarModeloCarta("^", 2),
                Efeito = batalha =>
                {
                    int danoFinal = 20 + batalha.Jogador.ModificadorDano;
                    int danoFInalAdjacente = 10 + batalha.Jogador.ModificadorDano;

                    int option = AlvoController.SelecionarAlvo(batalha.Inimigos);

                    var alvo = batalha.Inimigos[option];
                    alvo.SofrerDano(batalha.Jogador, danoFinal, false, true);

                    if (option > 0)
                    {
                        var alvoEsquerda = batalha.Inimigos[option - 1];
                        alvoEsquerda.SofrerDano(batalha.Jogador, danoFinal, false, true);
                    }
                    if (option < batalha.Inimigos.Count - 1)
                    {
                        var alvoDireita = batalha.Inimigos[option + 1];
                        alvoDireita.SofrerDano(batalha.Jogador, danoFinal, false, true);
                    }
                }
            };
        }

        public static ICartaUsavel CriarTiroMultiplo()
        {
            return new CartaGenerica
            {
                Nome = "Tiro Multiplo",
                Descricao = "Causa 15 de dano ao inimigo mais a esquerda e ao mais a direita ou causa 10 de dano em até 2 inimigos",
                RaridadeCarta = Raridade.Rara,
                Preco = GerarPreco(Raridade.Rara),
                CustoStamina = 30,
                Modelo = GerarModeloCarta("%", "v", 2),
                Efeito = batalha =>
                {
                    int danoFinal = 15 + batalha.Jogador.ModificadorDano;

                    List<List<string>> modelos = new List<List<string>>();
                    List<string> descricoes = new List<string>();

                    List<string> modeloUm = GerarModeloCarta("%", 2);
                    modelos.Add(modeloUm);

                    List<string> modeloDois = GerarModeloCarta("v", 2);
                    modelos.Add(modeloDois);

                    string descricaoUm = "Disparo em Cone: Causa 15 de dano ao inimigo mais a esquerda e ao mais a direita";
                    descricoes.Add(descricaoUm);

                    string descricaoDois = "Tiro Duplicado: Causa 10 de dano em até 2 inimigos";
                    descricoes.Add(descricaoDois);

                    int opcao = CartaController.MostrarOpcoes(modelos, descricoes);

                    if (opcao < 0) return;
                    else if (opcao == 0)
                    {
                        danoFinal = 20 + batalha.Jogador.ModificadorDano;

                        var alvoUm = batalha.Inimigos.First();

                        var alvoDois = batalha.Inimigos.Last();

                        alvoUm.SofrerDano(batalha.Jogador, danoFinal, false, true);

                        alvoDois.SofrerDano(batalha.Jogador, danoFinal, false, true);

                    }
                    else if (opcao == 1)
                    {
                        danoFinal = 10 + batalha.Jogador.ModificadorDano;

                        var alvoUm = batalha.Inimigos[AlvoController.SelecionarAlvo(batalha.Inimigos)];

                        var alvoDois = batalha.Inimigos[AlvoController.SelecionarAlvo(batalha.Inimigos)];

                        alvoUm.SofrerDano(batalha.Jogador, danoFinal, false, true);

                        alvoDois.SofrerDano(batalha.Jogador, danoFinal, false, true);

                    }
                }
            };
        }

        public static ICartaUsavel CriarFrenesiImpetuoso()
        {
            return new CartaGenerica
            {
                Nome = "Frenesi Impetuoso",
                Descricao = "Gasta toda a stamina do jogador. Causa 5 de dano a um inimigo para cada 15 de stamina gasta dessa forma",
                RaridadeCarta = Raridade.Rara,
                Preco = GerarPreco(Raridade.Rara),
                Modelo = GerarModeloCarta("Z", 2),
                Efeito = batalha =>
                {
                    int qtdAtaques = batalha.Jogador.StaminaAtual / 15;

                    int danoFinal = 5 + batalha.Jogador.ModificadorDano;

                    for (int i = 0; i < qtdAtaques; i++)
                    {
                        var alvo = AlvoController.EscolherInimigoAleatorio(batalha.Inimigos);
                        alvo.SofrerDano(batalha.Jogador, danoFinal, false, true);
                    }

                    batalha.Jogador.StaminaAtual = 0;
                }
            };
        }

        public static ICartaUsavel CriarDisparoPerfurante()
        {
            return new CartaGenerica
            {
                Nome = "Disparo Perfurante",
                Descricao = "Causa 20 de dano base ao inimigo mais à esquerda. Se ele morrer, o próximo inimigo também é atacado.",
                RaridadeCarta = Raridade.Rara,
                Preco = GerarPreco(Raridade.Rara),
                CustoStamina = 30,
                Modelo = GerarModeloCarta(">", 2),
                Efeito = batalha =>
                {
                    int danoFinal = 20 + batalha.Jogador.ModificadorDano;

                    int alvoAtual = 0;

                    Console.Clear();
                    BatalhaController.MostrarInimigos(batalha.Inimigos, -1);
                    Console.WriteLine();

                    while (alvoAtual < batalha.Inimigos.Count)
                    {
                        var inimigo = batalha.Inimigos[alvoAtual];
                        inimigo.SofrerDano(batalha.Jogador, danoFinal, false, true);

                        Console.WriteLine();
                        TextoController.CentralizarTexto($"{inimigo.Nome} foi atingido por Disparo Perfurante!\n");

                        if (inimigo.VidaAtual <= 0)
                        {
                            TextoController.CentralizarTexto($"{inimigo.Nome} foi morto!\n");
                            alvoAtual++;
                        }
                        else break;
                    }
                }
            };
        }

        public static ICartaUsavel CriarGolpePesado()
        {
            return new CartaGenerica
            {
                Nome = "Golpe Pesado",
                Descricao = "Esmaga um inimigo causando 30 de dano.",
                RaridadeCarta = Raridade.Rara,
                Preco = GerarPreco(Raridade.Rara),
                CustoStamina = 40,
                Modelo = GerarModeloCarta("T", 1),
                Efeito = batalha =>
                {
                    int danoFinal = 30 + batalha.Jogador.ModificadorDano;

                    int option = AlvoController.SelecionarAlvo(batalha.Inimigos);

                    var alvo = batalha.Inimigos[option];

                    alvo.SofrerDano(batalha.Jogador, danoFinal, false, true);
                }

            };

        }

        public static ICartaUsavel CriarSangueExplosivo()
        {
            return new CartaGenerica
            {
                Nome = "Sangue Explosivo",
                Descricao = "Sacrifique 10 de vida do jogador para causar 30 de dano a um inimigo.",
                RaridadeCarta = Raridade.Rara,
                Preco = GerarPreco(Raridade.Rara),
                CustoVida = 10,
                CustoMana = 20,
                Modelo = GerarModeloCarta("3", 1),
                Efeito = batalha =>
                {
                    int danoFinal = 25 + batalha.Jogador.ModificadorDano;

                    int option = AlvoController.SelecionarAlvo(batalha.Inimigos);

                    var alvo = batalha.Inimigos[option];
                    alvo.SofrerDano(batalha.Jogador, danoFinal, false, true);
                }
            };
        }

        public static ICartaUsavel CriarMordidaVampirica()
        {
            return new CartaGenerica
            {
                Nome = "Mordida Vampirica",
                Descricao = "Causa 15 de dano base a um inimigo. Drena a vida de um inimigo igual ao dano causado.",
                RaridadeCarta = Raridade.Rara,
                Preco = GerarPreco(Raridade.Rara),
                CustoStamina = 25,
                CustoMana = 20,
                Modelo = GerarModeloCarta("¨", 1),
                Efeito = batalha =>
                {
                    int danoFinal = 15 + batalha.Jogador.ModificadorDano;

                    int option = AlvoController.SelecionarAlvo(batalha.Inimigos);

                    var alvo = batalha.Inimigos[option];

                    int vidaInicial = alvo.VidaAtual;

                    alvo.SofrerDano(batalha.Jogador, danoFinal, false, true);

                    int vidaFinal = vidaInicial - alvo.VidaAtual;

                    batalha.Jogador.Curar(vidaFinal);
                }
            };
        }

        public static ICartaUsavel CriarGolpeEmpoderado()
        {
            return new CartaGenerica
            {
                Nome = "Golpe Empoderado",
                Descricao = "Causa 10 de dano base a um inimgio. Durante essa rodada suas cartas tem +5 de dano.",
                RaridadeCarta = Raridade.Rara,
                Preco = GerarPreco(Raridade.Rara),
                CustoStamina = 25,
                Modelo = GerarModeloCarta("F", 1),
                Efeito = batalha =>
                {
                    int danoFinal = 10 + batalha.Jogador.ModificadorDano;

                    int option = AlvoController.SelecionarAlvo(batalha.Inimigos);

                    var alvo = batalha.Inimigos[option];
                    alvo.SofrerDano(batalha.Jogador, danoFinal, false, true);

                    batalha.Jogador.ModificadorDano += 5;
                }
            };
        }

        public static ICartaUsavel CriarEspadaEEscudo()
        {
            return new CartaGenerica
            {
                Nome = "Espada e Escudo",
                Descricao = "Causa 15 de dano a um inimigo ou concede ao jogador 10 de escudo pela rodada",
                RaridadeCarta = Raridade.Rara,
                Preco = GerarPreco(Raridade.Rara),
                CustoStamina = 20,
                Modelo = GerarModeloCarta("/", "}", 1),
                Efeito = batalha =>
                {
                    List<List<string>> modelos = new List<List<string>>();
                    List<string> descricoes = new List<string>();

                    List<string> modeloUm = GerarModeloCarta("/", 1);
                    modelos.Add(modeloUm);

                    List<string> modeloDois = GerarModeloCarta("}", 1);
                    modelos.Add(modeloDois);


                    string descricaoUm = "Espada: Causa 15 de dano a um inimigo";
                    descricoes.Add(descricaoUm);

                    string descricaoDois = "Escudo: Concede ao jogador 10 de escudo pela rodada";
                    descricoes.Add(descricaoDois);

                    int opcao = CartaController.MostrarOpcoes(modelos, descricoes);

                    if (opcao < 0) return;
                    else if (opcao == 0)
                    {
                        int danoFinal = 15 + batalha.Jogador.ModificadorDano;

                        int option = AlvoController.SelecionarAlvo(batalha.Inimigos);

                        var alvo = batalha.Inimigos[option];

                        alvo.SofrerDano(batalha.Jogador, danoFinal, false, true);
                    }
                    else if (opcao == 1)
                    {
                        batalha.Jogador.Escudo = +10;
                        TextoController.CentralizarTexto($"{batalha.Jogador.Nome} ganhou 10 de escudo!");
                    }
                }
            };
        }

        public static ICartaUsavel CriarMixDeErvas()
        {
            return new CartaGenerica
            {
                Nome = "Mix de Ervas",
                Descricao = "Aplica venene em um inimigo ou cura o jogador em 15.",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoStamina = 30,
                CustoMana = 15,
                Modelo = GerarModeloCarta("V", "C", 1),
                Efeito = batalha =>
                {
                    List<List<string>> modelos = new List<List<string>>();
                    List<string> descricoes = new List<string>();

                    List<string> modeloUm = GerarModeloCarta("V", 1);
                    modelos.Add(modeloUm);

                    List<string> modeloDois = GerarModeloCarta("C", 1);
                    modelos.Add(modeloDois);


                    string descricaoUm = "Ervas Venenosas: Aplica veneno forte em um inimigo";
                    descricoes.Add(descricaoUm);

                    string descricaoDois = "Ervas Medicinais: Cura 15 de vida";
                    descricoes.Add(descricaoDois);

                    int opcao = CartaController.MostrarOpcoes(modelos, descricoes);

                    if (opcao < 0) return;
                    else if (opcao == 0)
                    {
                        var alvo = batalha.Inimigos[AlvoController.SelecionarAlvo(batalha.Inimigos)];

                        CondicaoController.AplicarOuAtualizarCondicao(new Veneno(5, 5), alvo.Condicoes);
                    }
                    else if (opcao == 1)
                    {
                        batalha.Jogador.Curar(15);
                    }
                }
            }; 
        }

        public static ICartaUsavel CriarEscudoDeEspinhos()
        {
            return new CartaGenerica
            {
                Nome = "Escudo de Espinhos",
                Descricao = "Concede ao jogador 5 de escudo. Causa dano igual ao escudo atual",
                RaridadeCarta = Raridade.Rara,
                Preco = GerarPreco(Raridade.Rara),
                CustoStamina = 30,
                Modelo = GerarModeloCarta("K", 1),
                Efeito = batalha =>
                {
                    int danoFinal = batalha.Jogador.Escudo + batalha.Jogador.ModificadorDano;

                    int option = AlvoController.SelecionarAlvo(batalha.Inimigos);

                    var alvo = batalha.Inimigos[option];
                    alvo.SofrerDano(batalha.Jogador, danoFinal, false, true);
                }
            };
        }

        public static ICartaUsavel CriarProtecao()
        {
            return new CartaGenerica
            {
                Nome = "Protecao",
                Descricao = "Concede ao jogador 5 de reducao de dano sofrido durante está rodada.",
                RaridadeCarta = Raridade.Rara,
                Preco = GerarPreco(Raridade.Rara),
                CustoStamina = 30,
                CustoMana = 10,
                Modelo = GerarModeloCarta("T", 1),
                Efeito = batalha =>
                {
                    batalha.Jogador.ModificadorDefesa += 5;
                }
            };
        }

        public static ICartaUsavel CriarCompraPoderosa()
        {
            return new CartaGenerica
            {
                Nome = "Compra Poderosa",
                Descricao = "Compra 3 cartas.",
                RaridadeCarta = Raridade.Rara,
                Preco = GerarPreco(Raridade.Rara),
                CustoStamina = 20,
                CustoMana = 20,
                Modelo = GerarModeloCarta("C", 1),
                Efeito = batalha =>
                {
                    PersonagemController.ComprarCartasExtras(batalha.Jogador, 3);
                }
            };
        }

        public static ICartaUsavel CriarMimico()
        {
            return new CartaGenerica
            {
                Nome = "Mimico",
                Descricao = "Copia o efeito de outra carta.",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Rara),
                CustoMana = 10,
                Modelo = GerarModeloCarta("M", 1),
                Efeito = batalha =>
                {
                    var jogador = batalha.Jogador;
                    List<ICartaUsavel> cartasViaveis = new List<ICartaUsavel>();

                    foreach (var cartaDisponivel in batalha.Jogador.Mao)
                    {
                        if (cartaDisponivel.CustoVida < jogador.VidaAtual
                        && cartaDisponivel.CustoMana < jogador.ManaAtual
                        && cartaDisponivel.CustoStamina < jogador.StaminaAtual
                        && cartaDisponivel.CustoOuro < jogador.Ouro
                        && cartaDisponivel.Nome != "Mimico")
                        {
                            cartasViaveis.Add(cartaDisponivel);
                        }
                    }

                    var carta = batalha.Jogador.Mao[PersonagemController.SelecionarCarta(batalha.Jogador, cartasViaveis, 0)];

                    carta.Usar(batalha);
                }
            };
        }

        public static ICartaUsavel CriarPurificacao()
        {
            return new CartaGenerica
            {
                Nome = "Purificação",
                Descricao = "Reduz a duração de todas as condições que aflingem o jogador em 1",
                RaridadeCarta = Raridade.Rara,
                Preco = GerarPreco(Raridade.Rara),
                CustoMana = 40,
                Modelo = GerarModeloCarta("†", 3),
                Efeito = batalha =>
                {
                    List<ICondicaoTemporaria> condicoesRemovidas = new List<ICondicaoTemporaria>();

                    foreach (var condicao in batalha.Jogador.Condicoes)
                    {
                        condicao.Atualizar();
                        if (condicao.Duracao == 0) condicoesRemovidas.Add(condicao);
                    }
                    foreach (var condicao in condicoesRemovidas)
                    {
                        batalha.Jogador.Condicoes.Remove(condicao);
                    }

                    TextoController.CentralizarTexto($"{batalha.Jogador.Nome} foi purificado!");
                }
            };
        }

        public static ICartaUsavel CriarReparos()
        {
            return new CartaGenerica
            {
                Nome = "Reparos",
                Descricao = "Cura todos os robôs aliados em 5",
                RaridadeCarta = Raridade.Rara,
                Preco = GerarPreco(Raridade.Rara),
                CustoStamina = 15,
                CustoOuro = 5,
                Modelo = GerarModeloCarta("P", 3),
                Efeito = batalha =>
                {
                    foreach (var aliado in batalha.Aliados)
                    {
                        if (aliado.Tipo == TipoCriatura.Robo)
                        {
                            aliado.Curar(10);
                        }
                    }
                }
            };
        }

        public static ICartaUsavel CriarCuidadosPosMortem()
        {
            return new CartaGenerica
            {
                Nome = "Cuidados Pós-Mortem",
                Descricao = "Cura todos os inimigos revidos em 10",
                RaridadeCarta = Raridade.Rara,
                Preco = GerarPreco(Raridade.Rara),
                CustoMana = 35,
                Modelo = GerarModeloCarta("†", 3),
                Efeito = batalha =>
                {
                    foreach (var aliado in batalha.Aliados)
                    {
                        if (aliado.Tipo == TipoCriatura.InimigoRevivido)
                        {
                            aliado.Curar(10);
                        }
                    }
                }
            };
        }

        public static ICartaUsavel CriarComprarSuprimentos()
        {
            return new CartaGenerica
            {
                Nome = "Comprar Suprimentos",
                Descricao = "Paga 20 de ouro para ganha 10 de Escudo e Dano extra nessa rodada",
                RaridadeCarta = Raridade.Rara,
                Preco = GerarPreco(Raridade.Rara),
                CustoOuro = 20,
                Modelo = GerarModeloCarta("%", 1),
                Efeito = batalha =>
                {
                    batalha.Jogador.Escudo += 10;
                    batalha.Jogador.ModificadorDano += 10;

                    TextoController.CentralizarTexto($"{batalha.Jogador.Nome} comprou suprimentos e ganhou 10 de escudo e dano extra!\n");
                }
            };
        }

        public static ICartaUsavel CriarCeia()
        {
            return new CartaGenerica
            {
                Nome = "Ceia",
                Descricao = "Cura 20 de vida ou aumenta a defesa em 5.",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoStamina = 30,
                CustoOuro = 5,
                Modelo = GerarModeloCarta("6", "9", 1),
                Efeito = batalha =>
                {
                    List<List<string>> modelos = new List<List<string>>();
                    List<string> descricoes = new List<string>();

                    List<string> modeloUm = GerarModeloCarta("6", 1);
                    modelos.Add(modeloUm);

                    List<string> modeloDois = GerarModeloCarta("9", 1);
                    modelos.Add(modeloDois);


                    string descricaoUm = "Sopa: Cura 20 de vida";
                    descricoes.Add(descricaoUm);

                    string descricaoDois = "Carne: Ganha 5 de defesa";
                    descricoes.Add(descricaoDois);

                    int opcao = CartaController.MostrarOpcoes(modelos, descricoes);

                    if (opcao < 0) return;
                    else if (opcao == 0)
                    {
                        batalha.Jogador.Curar(20);
                    }
                    else if (opcao == 1)
                    {
                        batalha.Jogador.ModificadorDefesa = +5;
                        TextoController.CentralizarTexto($"{batalha.Jogador.Nome} ganhou 5 de defesa!");
                    }
                }
            };
        }
   
        public static ICartaUsavel CriarCompraHabilidosa()
        {
            return new CartaGenerica
            {
                Nome = "Compra Habilidosa",
                Descricao = "Compra uma carta garantidamente",
                RaridadeCarta = Raridade.Rara,
                Preco = GerarPreco(Raridade.Rara),
                CustoMana = 20,
                CustoStamina = 20,
                Modelo = GerarModeloCarta("+", 1),
                Efeito = batalha =>
                {
                    PersonagemController.EmbaralharCartas(batalha.Jogador.BaralhoDescarte);
                    PersonagemController.ComprarCartasExtras(batalha.Jogador, 1);
                }
            };
        }


        #endregion


        #region Cartas Comuns

        public static ICartaUsavel CriarPocaoVenenosa()
        {
            return new CartaGenerica
            {
                Nome = "Poção Venenosa",
                Descricao = "Aplica veneno nível 5 em um inimigo por 5 turnos.",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoMana = 25,
                Modelo = GerarModeloCarta("p", 1),
                Efeito = batalha =>
                {
                    var alvo = batalha.Inimigos[AlvoController.SelecionarAlvo(batalha.Inimigos)];

                    CondicaoController.AplicarOuAtualizarCondicao(new Veneno(5, 5), alvo.Condicoes);
                    TextoController.CentralizarTexto($"{alvo.Nome} foi envenenado!\n");
                }
            };
        }

        public static ICartaUsavel CriarInseguranca()
        {
            return new CartaGenerica
            {
                Nome = "Insegurança",
                Descricao = "Observa fixamente um alvo pelas sombras. O deixando paranoico.",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoMana = 25,
                Modelo = GerarModeloCarta(".", 1),
                Efeito = batalha =>
                {
                    var alvo = batalha.Inimigos[AlvoController.SelecionarAlvo(batalha.Inimigos)];

                    CondicaoController.AplicarOuAtualizarCondicao(new Paranoia(3, 2), alvo.Condicoes);
                    TextoController.CentralizarTexto($"{alvo.Nome} se sente observado e entra em estado de panico!\n");
                }
            };
        }

        public static ICartaUsavel CriarExplosaoImprudente()
        {
            return new CartaGenerica
            {
                Nome = "Explosão Imprudente",
                Descricao = "Causa 10 de dano a todos os inimigos, mas você também sofre 5 de dano.",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoMana = 20,
                Modelo = GerarModeloCarta("#", 3),
                Efeito = batalha =>
                {
                    int danoFinal = 15 + batalha.Jogador.ModificadorDano;

                    foreach (var inimigo in batalha.Inimigos)
                    {
                        inimigo.SofrerDano(batalha.Jogador, danoFinal, false, true);
                    }

                    batalha.Jogador.SofrerDano(batalha.Jogador, 10, false, false);
                }
            };
        }

        public static ICartaUsavel CriarBombardeio()
        {
            return new CartaGenerica
            {
                Nome = "Bombardeio",
                Descricao = "Causa 10 de dano a todos os inimigos ou 3 vezes 5 de dano a um inimigo.",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoMana = 35,
                Modelo = GerarModeloCarta("v", "#", 6),
                Efeito = batalha =>
                {
                    int danoFinal = batalha.Jogador.ModificadorDano;

                    List<List<string>> modelos = new List<List<string>>();

                    List<string> descricoes = new List<string>();


                    List<string> modeloUm = GerarModeloCarta("v", 1);
                    modelos.Add(modeloUm);

                    List<string> modeloDois = GerarModeloCarta("#", 3);
                    modelos.Add(modeloDois);

                    string descricaoUm = "Rajada: Causa 5 de dano base a um inimigo 3 vezes";
                    descricoes.Add(descricaoUm);

                    string descricaoDois = "Chuva de Projetéis: Causa 10 de dano base em todos os inimigos";
                    descricoes.Add(descricaoDois);

                    int opcao = CartaController.MostrarOpcoes(modelos, descricoes);

                    if (opcao < 0) return;
                    else if (opcao == 0)
                    {
                        danoFinal = 5 + batalha.Jogador.ModificadorDano;

                        var alvo = batalha.Inimigos[AlvoController.SelecionarAlvo(batalha.Inimigos)];

                        for (int i = 0; i < 3; i++)
                        {
                            if (alvo.VidaAtual > 0)
                            {
                                alvo.SofrerDano(batalha.Jogador, danoFinal, false, true);
                                Console.WriteLine();
                            }
                        }

                    }
                    else if (opcao == 1)
                    {
                        danoFinal = 10 + batalha.Jogador.ModificadorDano;

                        Console.Clear();
                        BatalhaController.MostrarInimigos(batalha.Inimigos, -1);
                        Console.WriteLine();

                        foreach (var inimigo in batalha.Inimigos)
                        {
                            inimigo.SofrerDano(batalha.Jogador, danoFinal, false, true);
                        }

                    }
                }
            };
        }

        public static ICartaUsavel CriarChicotada()
        {
            return new CartaGenerica
            {
                Nome = "Chicotada",
                Descricao = "Causa 10 de dano a um inimigo aleatório. Chance de critoco com base no nivel atual",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoStamina = 25,
                Modelo = GerarModeloCarta("S", 1),
                Efeito = batalha =>
                {
                    int danoFinal = 10 + batalha.Jogador.ModificadorDano;

                    var alvo = AlvoController.EscolherInimigoAleatorio(batalha.Inimigos);

                    int chance = 10 + (batalha.Jogador.Nivel * 5);
                    int critico = BatalhaController.GerarRNG(chance);

                    danoFinal += critico == 0 ? danoFinal : 0;

                    if (critico == 0) TextoController.CentralizarTexto("O golpe foi crítico!");

                    alvo.SofrerDano(batalha.Jogador, danoFinal, false, true);

                }
            };
        }

        public static ICartaUsavel CriarMaldicaoDaLua()
        {
            return new CartaEvolutiva
            {
                Nome = "Maldição da Lua",
                NomeEvolucao = "Maldição da Lua Vermelha",
                Descricao = "Aplica 10 de maldição em um inimigo.",
                DescricaoEvolucao = "Causa 10 de dano base e aplica 10 de maldição em todos os inimigos.",
                RaridadeCarta = Raridade.Comum,
                RaridadeEvolucao = Raridade.Profana,
                Preco = GerarPreco(Raridade.Comum),
                CustoMana = 30,
                CustoVidaEvolucao = 10,
                Modelo = GerarModeloCarta("L", 1),
                ModeloEvolucao = GerarModeloCarta("L", 3),
                Evoluiu = false,
                usosAteEvoluir = 100,
                EfeitoEvolucao = batalha =>
                {
                    int danoFinal = 10 + batalha.Jogador.ModificadorDano;

                    Console.WriteLine();
                    foreach (var inimigo in batalha.Inimigos)
                    {
                        inimigo.SofrerDano(batalha.Jogador, danoFinal, false, true);
                        CondicaoController.AplicarOuAtualizarCondicao(new Maldicao(10), inimigo.Condicoes);

                        TextoController.CentralizarTexto($"{inimigo.Nome} foi amaldiçoado!\n");
                    }

                },
                EfeitoPadrao = batalha =>
                {
                    var alvo = batalha.Inimigos[AlvoController.SelecionarAlvo(batalha.Inimigos)];

                    CondicaoController.AplicarOuAtualizarCondicao(new Maldicao(50), alvo.Condicoes);

                    TextoController.CentralizarTexto($"{alvo.Nome} foi amaldiçoado!\n");

                    foreach (var condicao in alvo.Condicoes)
                    {
                        if (condicao is Maldicao && condicao.Nivel >= 50)
                        {
                            TextoController.CentralizarTexto($"A carta Maldição da Lua Evoluiu para Maldição da Lua Vermelha!\n");
                            batalha.Aplicadores.Add(new AplicadorEvolucao("Maldição da Lua"));
                            break;
                        }
                    }
                }
            };
        }

        public static ICartaUsavel CriarFogoMagico()
        {
            return new CartaGenerica
            {
                Nome = "Fogo Mágico",
                Descricao = "Causa 10 de dano base a um inimigo mais o nível de queimadura atual dele. Aplica queimadura.",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoStamina = 40,
                Modelo = GerarModeloCarta("f", 1),
                Efeito = batalha =>
                {
                    int danoFinal = 10 + batalha.Jogador.ModificadorDano;

                    var alvo = batalha.Inimigos[AlvoController.SelecionarAlvo(batalha.Inimigos)];

                    foreach (var condicao in alvo.Condicoes)
                    {
                        if (condicao is Queimadura queimadura)
                        {
                            danoFinal += queimadura.Nivel;
                        }
                    }

                    alvo.SofrerDano(batalha.Jogador, danoFinal, false, true);
                    CondicaoController.AplicarOuAtualizarCondicao(new Queimadura(2, 2), alvo.Condicoes);
                }

            };

        }

        public static ICartaUsavel CriarAtacarFerida()
        {
            return new CartaGenerica
            {
                Nome = "Atacar Ferida",
                Descricao = "Causa 10 de dano base a um inimigo. Se ele estiver com metade da vida ou menos, o dano é aumentado em 10",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoMana = 35,
                Modelo = GerarModeloCarta("_", 1),
                Efeito = batalha =>
                {
                    int danoFinal = 10 + batalha.Jogador.ModificadorDano;

                    var alvo = batalha.Inimigos[AlvoController.SelecionarAlvo(batalha.Inimigos)];

                    danoFinal += alvo.VidaAtual <= alvo.VidaMax / 2 ? 10 : 0;

                    alvo.SofrerDano(batalha.Jogador, danoFinal, false, true);
                }
            };
        }

        public static ICartaUsavel CriarSortearDestino()
        {
            return new CartaGenerica
            {
                Nome = "Sortear Destino",
                Descricao = "Dispara um feitiço que atinge um inimigo aleatório causando 20 de dano base.",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoMana = 15,
                Modelo = GerarModeloCarta("~", 2),
                Efeito = batalha =>
                {
                    int danoFinal = 20 + batalha.Jogador.ModificadorDano;

                    var alvo = AlvoController.EscolherInimigoAleatorio(batalha.Inimigos);
                    alvo.SofrerDano(batalha.Jogador, danoFinal, false, true);
                }
            };
        }

        public static ICartaUsavel CriarMaldicaoAmarga()
        {
            return new CartaGenerica
            {
                Nome = "Maldição Amarga",
                Descricao = "Causa 5 de dano base e aplica 5 de Maldição ao inimigo.",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoMana = 20,
                Modelo = GerarModeloCarta("6", 1),
                Efeito = batalha =>
                {
                    int danoFinal = 5 + batalha.Jogador.ModificadorDano;

                    int option = AlvoController.SelecionarAlvo(batalha.Inimigos);

                    var alvo = batalha.Inimigos[option];

                    alvo.SofrerDano(batalha.Jogador, danoFinal, false, true);
                    CondicaoController.AplicarOuAtualizarCondicao(new Maldicao(5), alvo.Condicoes);
                    TextoController.CentralizarTexto($"{alvo.Nome} foi amaldiçoado!\n");
                }
            };
        }

        public static ICartaUsavel CriarFlechaAfiada()
        {
            return new CartaRecarregavel
            {
                Nome = "Flecha Afiada",
                Descricao = "Uma flecha de ponta afiada que causa 10 de dano base e corta o inimigo.",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoStamina = 30,
                Modelo = GerarModeloCarta("!", 1),
                CargasMaximas = 1,
                CargasAtuais = 1,
                EfeitoComCarga = batalha =>
                {
                    int danoFinal = 10 + batalha.Jogador.ModificadorDano;

                    int option = AlvoController.SelecionarAlvo(batalha.Inimigos);

                    var alvo = batalha.Inimigos[option];
                    alvo.SofrerDano(batalha.Jogador, danoFinal, false, true);

                    CondicaoController.AplicarOuAtualizarCondicao(new Sangramento(2, 2), alvo.Condicoes);
                    TextoController.CentralizarTexto($"{alvo.Nome} foi cortado e está sangrando!");
                },
                EfeitoSemCarga = batalha =>
                {
                    TextoController.CentralizarTexto("A carta foi recarregada, mas não causou efeito.");
                }
            };
        }

        public static ICartaUsavel CriarFlechaEnvenenada()
        {
            return new CartaRecarregavel
            {
                Nome = "Flecha Envenenada",
                Descricao = "Causa 10 de dano base e aplica 3 de Veneno por 3 turnos",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoStamina = 30,
                CustoMana = 10,
                Modelo = GerarModeloCarta("S", 1),
                CargasMaximas = 1,
                CargasAtuais = 1,
                EfeitoComCarga = batalha =>
                {
                    int danoFinal = 10 + batalha.Jogador.ModificadorDano;

                    var alvo = batalha.Inimigos[AlvoController.SelecionarAlvo(batalha.Inimigos)];

                    alvo.SofrerDano(batalha.Jogador, danoFinal, false, true);
                    CondicaoController.AplicarOuAtualizarCondicao(new Veneno(3, 3), alvo.Condicoes);
                    TextoController.CentralizarTexto($"{alvo.Nome} foi envenenado!");
                },
                EfeitoSemCarga = batalha =>
                {
                    TextoController.CentralizarTexto("A carta foi recarregada, mas não causou efeito.");
                }
            };
        }

        public static ICartaUsavel CriarFlechada()
        {
            return new CartaRecarregavel
            {
                Nome = "Flechada",
                Descricao = "Lança uma flecha no alvo causando 15 de dano base.",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoStamina = 15,
                Modelo = GerarModeloCarta(@"\", 1),
                CargasMaximas = 1,
                CargasAtuais = 1,

                EfeitoComCarga = batalha =>
                {
                    int danoFinal = 20 + batalha.Jogador.ModificadorDano;

                    var alvo = batalha.Inimigos[AlvoController.SelecionarAlvo(batalha.Inimigos)];

                    alvo.SofrerDano(batalha.Jogador, danoFinal, false, true);

                },
                EfeitoSemCarga = batalha =>
                {
                    TextoController.CentralizarTexto("A carta foi recarregada, mas não causou efeito.");
                }
            };
        }

        public static ICartaUsavel CriarEspadada()
        {
            return new CartaGenerica
            {
                Nome = "Espadada",
                Descricao = "Golpe físico que causa 15 de dano base a um inimigo.",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoStamina = 15,
                Modelo = GerarModeloCarta("/", 1),
                Efeito = batalha =>
                {
                    int danoFinal = 15 + batalha.Jogador.ModificadorDano;

                    int option = AlvoController.SelecionarAlvo(batalha.Inimigos);

                    var alvo = batalha.Inimigos[option];

                    alvo.SofrerDano(batalha.Jogador, danoFinal, false, true);
                }
            };
        }

        public static ICartaUsavel CriarAtaqueMagico()
        {
            return new CartaGenerica
            {
                Nome = "Ataque Magico",
                Descricao = "Lança um ataque magico no alvo causando 15 de dano base.",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoMana = 15,
                Modelo = GerarModeloCarta("A", 1),
                Efeito = batalha =>
                {
                    int danoFinal = 15 + batalha.Jogador.ModificadorDano;

                    int option = AlvoController.SelecionarAlvo(batalha.Inimigos);

                    var alvo = batalha.Inimigos[option];
                    alvo.SofrerDano(batalha.Jogador, danoFinal, false, true);
                }
            };
        }

        public static ICartaUsavel CriarBisturi()
        {
            return new CartaGenerica
            {
                Nome = "Bisturi",
                Descricao = "Corta um inimigo cusando 15 de dano. Chance de aplicar sangramento leve",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoMana = 15,
                Modelo = GerarModeloCarta("A", 1),
                Efeito = batalha =>
                {
                    int danoFinal = 15 + batalha.Jogador.ModificadorDano;

                    var alvo = batalha.Inimigos[AlvoController.SelecionarAlvo(batalha.Inimigos)];

                    int chance = BatalhaController.GerarRNG(20);

                    if (chance == 0)
                    {
                        CondicaoController.AplicarOuAtualizarCondicao(new Sangramento(2, 2), alvo.Condicoes);
                    }
                    alvo.SofrerDano(batalha.Jogador, danoFinal, false, true);
                }
            };
        }

        public static ICartaUsavel CriarChoque()
        {
            return new CartaGenerica
            {
                Nome = "Choque",
                Descricao = "Causa dano base de 10 a um alvo. 15 se tiver um aliado robô ativo",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoMana = 10,
                CustoStamina = 10,
                Modelo = GerarModeloCarta("A", 1),
                Efeito = batalha =>
                {
                    int danoFinal = 10 + batalha.Jogador.ModificadorDano;

                    foreach (var aliado in batalha.Aliados)
                    {
                        if (aliado.Tipo == TipoCriatura.Robo)
                        {
                            danoFinal += 5;
                            break;
                        }
                    }

                    int option = AlvoController.SelecionarAlvo(batalha.Inimigos);

                    var alvo = batalha.Inimigos[option];
                    alvo.SofrerDano(batalha.Jogador, danoFinal, false, true);
                }
            };
        }

        public static ICartaUsavel CriarInvestida()
        {
            return new CartaGenerica
            {
                Nome = "Investida",
                Descricao = "Causa 10 de dano base a um inimigo. Sofre 5 de dano ao jogador",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoStamina = 15,
                Modelo = GerarModeloCarta("P", 1),
                Efeito = batalha =>
                {
                    int danoFinal = 10 + batalha.Jogador.ModificadorDano;

                    var alvo = batalha.Inimigos[AlvoController.SelecionarAlvo(batalha.Inimigos)];

                    alvo.SofrerDano(batalha.Jogador, danoFinal, false, true);
                    batalha.Jogador.SofrerDano(batalha.Jogador, 5, false, true);

                }
            };
        }

        public static ICartaUsavel CriarDrenarSangue()
        {
            return new CartaGenerica
            {
                Nome = "Drenar Sangue",
                Descricao = "Causa 10 de dano base a um inimigo. Cura o jogador em 5 caso o inimigo seja finalizado",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoStamina = 15,
                CustoMana = 10,
                Modelo = GerarModeloCarta("P", 1),
                Efeito = batalha =>
                {
                    int danoFinal = 10 + batalha.Jogador.ModificadorDano;

                    var alvo = batalha.Inimigos[AlvoController.SelecionarAlvo(batalha.Inimigos)];

                    alvo.SofrerDano(batalha.Jogador, danoFinal, false, true);

                    if(alvo.VidaAtual <= 0)
                    {
                        batalha.Jogador.Curar(5);
                        TextoController.CentralizarTexto($"{batalha.Jogador.Nome} drenou o sangue de {alvo.Nome} e curou 5 de vida!");
                    }

                }
            };
        }

        public static ICartaUsavel CriarEscudo()
        {
            return new CartaGenerica
            {
                Nome = "Escudo",
                Descricao = "Concede 10 de escudo ao jogador.",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoStamina = 15,
                Modelo = GerarModeloCarta("}", 1),
                Efeito = batalha =>
                {
                    batalha.Jogador.Escudo = +10;
                    TextoController.CentralizarTexto($"{batalha.Jogador.Nome} ganhou 10 de escudo!");
                }
            };
        }

        public static ICartaUsavel CriarBemMunido()
        {
            return new CartaGenerica
            {
                Nome = "Bem Munido",
                Descricao = "Suas cartas causam mais 5 de dano durante essa rodada. Compre 1 carta.",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoMana = 15,
                Modelo = GerarModeloCarta("ª", 1),
                Efeito = batalha =>
                {
                    batalha.Jogador.ModificadorDano += 5;
                    PersonagemController.ComprarCartasExtras(batalha.Jogador, 1);

                    foreach (var carta in batalha.Jogador.Mao)
                    {
                        if (carta is CartaRecarregavel cartaRecarregavel)
                        {
                            cartaRecarregavel.CargasAtuais++;
                        }
                    }
                }
            };
        }

        public static ICartaUsavel CriarPocaoDeCura()
        {
            return new CartaGenerica
            {
                Nome = "Poção de Cura",
                Descricao = "Restaura 15 de vida.",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoMana = 15,
                Modelo = GerarModeloCarta("+", 1),
                Efeito = batalha =>
                {
                    batalha.Jogador.Curar(15);
                }
            };
        }

        public static ICartaUsavel CriarPocaoDeStamina()
        {
            return new CartaGenerica
            {
                Nome = "Poção de Stamina",
                Descricao = "Restaura 15 de stamina.",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoMana = 15,
                Modelo = GerarModeloCarta("E", 1),
                Efeito = batalha =>
                {
                    batalha.Jogador.StaminaAtual += 15;

                    TextoController.CentralizarTexto($"{batalha.Jogador.Nome} recuperou parte de sua stamina!\n");
                }
            };
        }

        public static ICartaUsavel CriarPocaoDeMana()
        {
            return new CartaGenerica
            {
                Nome = "Poção de Mana",
                Descricao = "Restaura 15 de mana.",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoStamina = 20,
                Modelo = GerarModeloCarta("Q", 1),
                Efeito = batalha =>
                {
                    batalha.Jogador.ManaAtual += 15;

                    TextoController.CentralizarTexto($"{batalha.Jogador.Nome} recuperou parte de sua mana!\n");
                }
            };
        }

        public static ICartaUsavel CriarPocaoFuriosa()
        {
            return new CartaGenerica
            {
                Nome = "Poção Furiosa",
                Descricao = "Aumenta o dano das cartas do jogador em 10.",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoVida = 5,
                CustoMana = 20,
                Modelo = GerarModeloCarta("f", 1),
                Efeito = batalha =>
                {
                    batalha.Jogador.ModificadorDano += 5;

                    TextoController.CentralizarTexto($"{batalha.Jogador.Nome} ganhou mais 5 de dano bônus!\n");

                }
            };
        }

        public static ICartaUsavel CriarEconomiaLocal()
        {
            return new CartaGenerica
            {
                Nome = "Economia Local",
                Descricao = "Ganha 10 de ouro. Ganha +1 de juros para cada 25 de ouro.",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoStamina = 10,
                CustoOuro = 5,
                Modelo = GerarModeloCarta("$", 1),
                Efeito = batalha =>
                {
                    batalha.Jogador.Ouro += 10;

                    int juros = (int)batalha.Jogador.Ouro / 25;


                    batalha.Jogador.Ouro += juros;
                    TextoController.CentralizarTexto($"{batalha.Jogador.Nome} ganhou 10 de ouros e {juros} de juros!");

                }
            };
        }


        #endregion


        #region Funções para automatizar criação das cartas
        private static int GerarPreco(Raridade raridade)
        {
            switch (raridade)
            {
                case Raridade.Comum:
                    return 30;

                case Raridade.Rara:
                    return 65;

                case Raridade.Lendaria:
                    return 100;

                case Raridade.Profana:
                    return 90;

                default:
                    return 25;
            }
        }


        //função pra criar o modelo de forma mais automatica : Esse é pra alvo unico, mais de um alvo, todos os alvos
        private static List<string> GerarModeloCarta(string simbolo, int opcao)
        {
            if (opcao == 1)
            {
                return new List<string>
                {
                "           ",
                " .-------. ",
               $" |{simbolo}.-^-. | ",
               @" | :/ \: | ",
               $" | |({simbolo})| | ",
               @" | :\ /: | ",
               $" | '-v-'{simbolo}| ",
                " `-------' ",
                "           ",
                "           ",
                };
            }
            else if (opcao == 2)
            {
                return new List<string>
                {
                "           ",
                " .-------. ",
               $" | .---.{simbolo}| ",
               @" | < \\: | ",
               $" |   {simbolo}   | ",
               @" | :// > | ",
               $" |{simbolo}'---' | ",
                " `-------' ",
                "           ",
                "           ",
                };

            }
            else if (opcao == 3)
            {
                return new List<string>
                {
                "           ",
                " .-------. ",
               $" |{simbolo}.---. | ",
               @" | :():  | ",
               $" |   {simbolo}:()| ",
               @" | ()    | ",
               $" | '---'{simbolo}| ",
                " `-------' ",
                "           ",
                "           ",
                };

            }
            else return null;

        }
        /*
        
            .-------.
            |1.-^-. |
            | :/ \: |
            | |(1)| |
            | :\ /: |
            | '-v-'1|
            `-------'
        

            .-------.
            | .---.2|
            | < \\: |
            |   2   |
            | :// > |
            |2'---' |
            `-------'
        
            
            .-------.
            |3.---. |
            | :():  |
            |   3:()|
            | :():  |
            | '---'3|
            `-------'

        
        */

        //função pra criar o modelo de forma mais automatica : Esse é pra cartas com escolha dupla
        private static List<string> GerarModeloCarta(string simboloUm, string simboloDois, int option)
        {
            switch (option)
            {
                case 1:
                    return new List<string>
                    {
                        "           ",
                        " .-------. ",
                       $" |{simboloUm}.-|-. | ",
                       @" | :/|\: | ",
                       $" | |(|)| | ",
                       @" | :\|/: | ",
                       $" | '-|-'{simboloDois}| ",
                        " `-------' ",
                        "           ",
                        "           ",
                    };

                case 2:
                    return new List<string>
                    {
                        "           ",
                        " .-------. ",
                       $" | .-|-.{simboloDois}| ",
                       @" | < |\: | ",
                       $" |   |   | ",
                       @" | :/| > | ",
                       $" |{simboloUm}'-|-' | ",
                        " `-------' ",
                        "           ",
                        "           ",
                    };

                case 3:
                    return new List<string>
                    {
                        "           ",
                        " .-------. ",
                       $" |{simboloUm}.-|-. | ",
                       @" | :(|:  | ",
                       $" |   |:()| ",
                       @" | :(|:  | ",
                       $" | '-|-'{simboloDois}| ",
                        " `-------' ",
                        "           ",
                        "           ",
                    };

                case 6:
                    return new List<string>
                    {
                        "           ",
                        " .-------. ",
                       $" |{simboloUm}.-|-. | ",
                       @" | :/|:  | ",
                       $" | |(|:()| ",
                       @" | :\|:  | ",
                       $" | '-|-'{simboloDois}| ",
                        " `-------' ",
                        "           ",
                        "           ",
                    };

                default:
                    return null;
            }
        }
        /*
        
        duas opções de alvo unico(ou o jogador)
            .-------.
            |A.-|-. |
            | :/|\: |
            | |(|)| |
            | :\|/: |
            | '-|-'B|
            `-------'

        duas opções de alvos multiplos
            .-------.
            | .-|-.2|
            | < |\: |
            |   |   |
            | :/| > |
            |2'-|-' |
            `-------'
        
        duas opções de todos os alvos
            .---=---.
            |3.-|-. |
            | :(|:  |
            |   |:()|
            | :(|:  |
            | '-|-'3|
            `-------'
        */

        #endregion
    }
}
