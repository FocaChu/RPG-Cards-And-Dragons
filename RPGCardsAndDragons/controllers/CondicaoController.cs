using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.ClassesCondicoes;
using CardsAndDragonsJogo;
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

        public static void AplicarDoenca(ICondicaoContagiosa doenca, List<ICondicaoContagiosa> doencas)
        {

        }


        //Verifica as condições especiais dos inimigos
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

            if(batalha.Aliados.Count > 0)
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

        //mostra as condições especiais que o inimigo tem no momento(elas já vem formatas pelo ToString delas)
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


        //Usado no turno dos inimigos pra ver se eles estão congelados. Sim? Não atacam
        public static bool VerificarCongelamento(ICriaturaCombatente criatura)
        {
            foreach (var condicao in criatura.Condicoes)
            {
                if (condicao is Atordoamento)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        //Usado no ataque dos inimigos pra ver se eles estão silenciados. Sim? Não podem usar o especial.
        public static bool VerificarSilencio(List<ICondicaoTemporaria> condicoes)
        {
            foreach (var condicao in condicoes)
            {
                if (condicao is Silencio)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public static bool VerificarSangramento(List<ICondicaoTemporaria> condicoes)
        {
            foreach (var condicao in condicoes)
            {
                if (condicao is Sangramento && condicao.Duracao > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public static bool VerificarEnvenenamento(List<ICondicaoTemporaria> condicoes)
        {
            foreach (var condicao in condicoes)
            {
                if (condicao is Veneno && condicao.Duracao > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        //Verifica se o inimigo esta sangrando no turno dele. Sim? Aplica efeito apos atacar
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
