using System;
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

                    CondicaoController.AplicarOuAtualizarCondicao(new Silencio(), alvo.Condicoes);
                    Console.WriteLine();
                    TextoController.CentralizarTexto($"{alvo.Nome} foi silênciado...");
                }
            };
        }

        public static ICartaUsavel CriarFungo()
        {
            return new CartaGenerica
            {
                Nome = "Praga: Fungo",
                Descricao = "Infesta um inimigo com um fungo agressivo que drena vida e escudo.",
                RaridadeCarta = Raridade.Rara,
                Preco = GerarPreco(Raridade.Rara),
                CustoMana = 30,
                Modelo = GerarModeloCarta("☣", 1),

                Efeito = batalha =>
                {
                    var alvo = batalha.Inimigos[AlvoController.SelecionarAlvo(batalha.Inimigos)];

                    var tipoDoenca = new TipoFungo();

                    var efeitos = tipoDoenca.CriarEfeitos();

                    // Aplica a modificação de custo em cada efeito
                    foreach (var efeito in efeitos)
                    {
                        efeito.Custo += tipoDoenca.ModificarCusto(efeito);
                    }

                    var fungo = new Doenca(tipoDoenca, 3, true, 5, new TransmissaoAr(), efeitos);

                    CondicaoController.AplicarOuAtualizarCondicao(fungo, alvo.Condicoes);
                    TextoController.CentralizarTexto($"{alvo.Nome} foi infectado por esporos fúngicos!");
                }
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
                        CondicaoController.AplicarOuAtualizarCondicao(new Silencio(), inimigo.Condicoes);
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
                                inimigo.SofrerDano(condicao.Nivel, false);
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
                        inimigo.SofrerDano(danoFinal, false);
                        if (inimigo.VidaAtual <= 0) recompensa++;
                    }

                    batalha.Jogador.Ouros += 10 * recompensa;
                    batalha.Jogador.XpAtual += 5 * recompensa;
                }
            };
        }

        #endregion


        #region Cartas Raras

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

        public static ICartaUsavel CriarMimico()
        {
            return new CartaGenerica
            {
                Nome = "Mimico",
                Descricao = "Invoca um robô auxiliar temporário para esta batalha.",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Rara),
                CustoMana = 20,
                Modelo = GerarModeloCarta("M", 1),
                Efeito = batalha =>
                {
                    var carta = batalha.Jogador.Mao[PersonagemController.SelecionarCarta(batalha.Jogador, 0)];

                    carta.Usar(batalha);
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
                CustoMana = 40,
                Modelo = GerarModeloCarta("9", 1),
                Efeito = batalha =>
                {
                    if (batalha.InimigosDerrotados.Count == 0)
                    {
                        TextoController.CentralizarTexto("Não há inimigos para reviver.");
                        return;
                    }

                    var inimigoEscolhido = AliadoController.EscolherUmAlvo(batalha.InimigosDerrotados);
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
                        inimigo.SofrerDano(danoFinal, false);
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
                        inimigo.SofrerDano(danoFinal, false);
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
                        inimigo.SofrerDano(danoFinal, false);
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
                    alvo.SofrerDano(danoFinal, false);

                    if (option > 0)
                    {
                        var alvoEsquerda = batalha.Inimigos[option - 1];
                        alvoEsquerda.SofrerDano(danoFInalAdjacente, false);
                    }
                    if (option < batalha.Inimigos.Count - 1)
                    {
                        var alvoDireita = batalha.Inimigos[option + 1];
                        alvoDireita.SofrerDano(danoFInalAdjacente, false);
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

                    List<string> modeloUm = GerarModeloCarta("%", 2);
                    string descricaoUm = "Disparo em Cone: Causa 15 de dano ao inimigo mais a esquerda e ao mais a direita";

                    List<string> modeloDois = GerarModeloCarta("v", 2);
                    string descricaoDois = "Tiro Duplicado: Causa 10 de dano em até 2 inimigos";

                    int opcao = CartaController.MostrarOpcoes(modeloUm, modeloDois, descricaoUm, descricaoDois);

                    if (opcao < 0) return;
                    else if (opcao == 0)
                    {
                        danoFinal = 20 + batalha.Jogador.ModificadorDano;

                        var alvoUm = batalha.Inimigos.First();

                        var alvoDois = batalha.Inimigos.Last();

                        alvoUm.SofrerDano(danoFinal, false);

                        alvoDois.SofrerDano(danoFinal, false);

                    }
                    else if (opcao == 1)
                    {
                        danoFinal = 10 + batalha.Jogador.ModificadorDano;

                        var alvoUm = batalha.Inimigos[AlvoController.SelecionarAlvo(batalha.Inimigos)];

                        var alvoDois = batalha.Inimigos[AlvoController.SelecionarAlvo(batalha.Inimigos)];

                        alvoUm.SofrerDano(danoFinal, false);

                        alvoDois.SofrerDano(danoFinal, false);

                    }
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
                        inimigo.SofrerDano(danoFinal, false);

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

                    alvo.SofrerDano(danoFinal, false);
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
                    alvo.SofrerDano(danoFinal, false);
                }
            };
        }

        public static ICartaUsavel CriarMordidaVampirica()
        {
            return new CartaGenerica
            {
                Nome = "Mordida Vampirica",
                Descricao = "Causa 10 de dano base a um inimigo. Drena a vida de um inimigo igual ao dano causado.",
                RaridadeCarta = Raridade.Rara,
                Preco = GerarPreco(Raridade.Rara),
                CustoStamina = 20,
                CustoMana = 20,
                Modelo = GerarModeloCarta("¨", 1),
                Efeito = batalha =>
                {
                    int danoFinal = 10 + batalha.Jogador.ModificadorDano;

                    int option = AlvoController.SelecionarAlvo(batalha.Inimigos);

                    var alvo = batalha.Inimigos[option];

                    int vidaInicial = alvo.VidaAtual;

                    alvo.SofrerDano(danoFinal, false);

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
                    alvo.SofrerDano(danoFinal, false);

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
                    List<string> modeloUm = GerarModeloCarta("/", 1);
                    string descricaoUm = "Espada: Causa 15 de dano a um inimigo";

                    List<string> modeloDois = GerarModeloCarta("}", 1);
                    string descricaoDois = "Escudo: Concede ao jogador 10 de escudo pela rodada";

                    int opcao = CartaController.MostrarOpcoes(modeloUm, modeloDois, descricaoUm, descricaoDois);

                    if (opcao < 0) return;
                    else if (opcao == 0)
                    {
                        int danoFinal = 15 + batalha.Jogador.ModificadorDano;

                        int option = AlvoController.SelecionarAlvo(batalha.Inimigos);

                        var alvo = batalha.Inimigos[option];

                        alvo.SofrerDano(danoFinal, false);
                    }
                    else if (opcao == 1)
                    {
                        batalha.Jogador.Escudo = +10;
                        TextoController.CentralizarTexto($"{batalha.Jogador.Nome} ganhou 10 de escudo!");
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
                    alvo.SofrerDano(danoFinal, false);
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
                        if(aliado.Tipo == TipoCriatura.Robo)
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

        #endregion


        #region Cartas Comuns

        public static ICartaUsavel CriarPocaoVenenosa()
        {
            return new CartaGenerica
            {
                Nome = "Põção Venenosa",
                Descricao = "Aplica veneno nível 5 em um inimigo por 5 turnos.",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoMana = 25,
                Modelo = GerarModeloCarta("p", 1),
                Efeito = batalha =>
                {
                    int option = AlvoController.SelecionarAlvo(batalha.Inimigos);

                    var alvo = batalha.Inimigos[option];

                    CondicaoController.AplicarOuAtualizarCondicao(new Veneno(5, 5), alvo.Condicoes);
                    TextoController.CentralizarTexto($"{alvo.Nome} foi envenenado!\n");
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
                        inimigo.SofrerDano(danoFinal, false);
                    }

                    batalha.Jogador.SofrerDano(10, false);
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

                    List<string> modeloUm = GerarModeloCarta("v", 1);
                    string descricaoUm = "Rajada: Causa 5 de dano base a um inimigo 3 vezes";

                    List<string> modeloDois = GerarModeloCarta("#", 3);
                    string descricaoDois = "Chuva de Projetéis: Causa 10 de dano base em todos os inimigos";

                    int opcao = CartaController.MostrarOpcoes(modeloUm, modeloDois, descricaoUm, descricaoDois);

                    if (opcao < 0) return;
                    else if (opcao == 0)
                    {
                        danoFinal = 5 + batalha.Jogador.ModificadorDano;

                        var alvo = batalha.Inimigos[AlvoController.SelecionarAlvo(batalha.Inimigos)];

                        for (int i = 0; i < 3; i++)
                        {
                            if (alvo.VidaAtual > 0)
                            {
                                alvo.SofrerDano(danoFinal, false);
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
                            inimigo.SofrerDano(danoFinal, false);
                        }

                    }
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

                    alvo.SofrerDano(danoFinal, false);
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

                    Random random = new Random();
                    int indiceAleatorio = random.Next(batalha.Inimigos.Count);

                    var alvo = batalha.Inimigos[indiceAleatorio];
                    alvo.SofrerDano(danoFinal, false);
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

                    alvo.SofrerDano(danoFinal, false);
                    CondicaoController.AplicarOuAtualizarCondicao(new Maldicao(5), alvo.Condicoes);
                    TextoController.CentralizarTexto($"{alvo.Nome} foi amaldiçoado!\n");
                }
            };
        }

        public static ICartaUsavel CriarFlechaAfiada()
        {
            return new CartaGenerica
            {
                Nome = "Flecha Afiada",
                Descricao = "Uma flecha de ponta afiada que causa 10 de dano base e corta o inimigo.",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoStamina = 30,
                Modelo = GerarModeloCarta("!", 1),
                Efeito = batalha =>
                {
                    int danoFinal = 10 + batalha.Jogador.ModificadorDano;

                    int option = AlvoController.SelecionarAlvo(batalha.Inimigos);

                    var alvo = batalha.Inimigos[option];
                    alvo.SofrerDano(danoFinal, false);

                    CondicaoController.AplicarOuAtualizarCondicao(new Sangramento(2, 2), alvo.Condicoes);
                    TextoController.CentralizarTexto($"{alvo.Nome} foi cortado e está sangrando!");
                }
            };
        }

        public static ICartaUsavel CriarFlechaEnvenenada()
        {
            return new CartaGenerica
            {
                Nome = "Flecha Envenenada",
                Descricao = "Causa 10 de dano base e aplica 3 de Veneno por 3 turnos",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoStamina = 30,
                CustoMana = 10,
                Modelo = GerarModeloCarta("S", 1),
                Efeito = batalha =>
                {
                    int danoFinal = 10 + batalha.Jogador.ModificadorDano;

                    var alvo = batalha.Inimigos[AlvoController.SelecionarAlvo(batalha.Inimigos)];

                    alvo.SofrerDano(danoFinal, false);
                    CondicaoController.AplicarOuAtualizarCondicao(new Veneno(3, 3), alvo.Condicoes);
                    TextoController.CentralizarTexto($"{alvo.Nome} foi envenenado!");
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

                    alvo.SofrerDano(danoFinal, false);

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

                    alvo.SofrerDano(danoFinal, false);
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
                    alvo.SofrerDano(danoFinal, false);
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

                    foreach(var carta in batalha.Jogador.Mao)
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
                Descricao = "Restaura 10 de vida.",
                RaridadeCarta = Raridade.Comum,
                Preco = GerarPreco(Raridade.Comum),
                CustoMana = 15,
                Modelo = GerarModeloCarta("+", 1),
                Efeito = batalha =>
                {
                    batalha.Jogador.Curar(10);
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
