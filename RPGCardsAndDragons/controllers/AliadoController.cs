using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.Aliados;
using CardsAndDragons.Inimigos;
using CardsAndDragonsJogo;

namespace CardsAndDragons.Controllers
{
    public static class AliadoController
    {
        private static Random rng = new Random();


        public static void ReviverInimigo(Batalha batalha, OInimigo inimigoEscolhido)
        {
            Type tipo = inimigoEscolhido.InimigoBase.GetType();
            var novaInstancia = (InimigoRPG)Activator.CreateInstance(tipo);
            var revivido = new InimigoRevivido(novaInstancia);

            batalha.Aliados.Add(revivido);
            batalha.InimigosDerrotados.Remove(inimigoEscolhido);

            TextoController.CentralizarTexto($"{revivido.Nome} foi revivido como aliado!");
        }

        public static RoboAliado ConfigurarRobo()
        {
            // Escolha interativa do software e hardware
            TipoSoftware software = ProgramarRobo();    // ex: Cura, Ataque...
            TipoHardware hardware = MontarRobo();       // ex: Eficiência, Resistência...

            string nome = "";

            // Pede o nome do robô
            while (nome == null || nome == string.Empty)
            {
                Console.WriteLine();
                TextoController.CentralizarTexto("Nomeie o seu robô: ");
                TextoController.CentralizarLinha("");
                nome = Console.ReadLine();
            }

            // Garante que sempre cria uma nova instância separada
            return new RoboAliado(software, hardware, nome);
        }

        public static TipoSoftware ProgramarRobo()
        {
            //mostra todas os softwares
            string[] opcoes = new string[]
            {
                "Médico",
                "Segurança",
                "Anti-Vírus",
                "Malware"
            };

            string[] descricoes = new string[]
            {
                "Cura aliados durante o combate",
                "Ataque possíves ameaças durante um combate",
                "Concede aumentos nos status de aliados ao longo do combate",
                "Sabota possiveis ameaças durante um combate, reduzindo seus status"
            };

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
                TextoController.CentralizarTexto("====================================== PROGRAMANDO ROBÔ ==================================");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                TextoController.CentralizarTexto("   Escolha seu Software:\n\n\n");

                //é oque mostra o menu na tela com base no EspecieHelper e permite vc ver qual especie vc ta selecionando
                for (int i = 0; i < 4; i++)
                {
                    if (i == opcaoSelecionada)
                    {

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        TextoController.CentralizarTexto($">> {opcoes[i]}");
                        TextoController.CentralizarTexto($" -- {descricoes[i]}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        TextoController.CentralizarTexto($"   {opcoes[i]}");
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
                        else opcaoSelecionada = opcoes.Length - 1;
                        break;
                    //se clicar pra baixo desce pra opção de baixo e ajusta o valor pra combinar com ela na lista
                    case ConsoleKey.DownArrow:
                        if (opcaoSelecionada < opcoes.Length - 1) opcaoSelecionada++;
                        else opcaoSelecionada = 0;
                        break;

                    case ConsoleKey.Enter:
                        selecionado = true;
                        break;
                    //Ao clicar em enter ele para o codigo e pega a especie correspondente ao valor escolhido(que é definido por opcaoSelecionada)

                } 
            }

            TipoSoftware software = (TipoSoftware)opcaoSelecionada;

            return software;
        }

        public static TipoHardware MontarRobo()
        {
            //mostra todas as especies
            string[] opcoes = new string[]
            {
                "Eficiente",
                "Resistente",
                "Multitarefas",
                "Economico"
            };

            string[] descricoes = new string[]
            {
                "Mais eficiente, porém mais fragíl",
                "Mais resistente, porém menos eficiente",
                "Ativa duas vezes por turno, porém qualidade mediana",
                "Equilibrio entre eficiência e resistencia"
            };

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
                TextoController.CentralizarTexto("====================================== CONSTRUINDO ROBÔ ==================================");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                TextoController.CentralizarTexto("   Escolha seu Hardware:");
                Console.WriteLine();
                Console.WriteLine();

                //é oque mostra o menu na tela com base no EspecieHelper e permite vc ver qual especie vc ta selecionando
                for (int i = 0; i < 4; i++)
                {
                    if (i == opcaoSelecionada)
                    {

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        TextoController.CentralizarTexto($">> {opcoes[i]}");
                        TextoController.CentralizarTexto($" -- {descricoes[i]}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        TextoController.CentralizarTexto($"   {opcoes[i]}");
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
                        else opcaoSelecionada = opcoes.Length - 1;
                        break;
                    //se clicar pra baixo desce pra opção de baixo e ajusta o valor pra combinar com ela na lista
                    case ConsoleKey.DownArrow:
                        if (opcaoSelecionada < opcoes.Length - 1) opcaoSelecionada++;
                        else opcaoSelecionada = 0;
                        break;

                    case ConsoleKey.Enter:
                        selecionado = true;
                        break;
                        //Ao clicar em enter ele para o codigo e pega a especie correspondente ao valor escolhido(que é definido por opcaoSelecionada)

                }
            }

            TipoHardware hardware = (TipoHardware)opcaoSelecionada;

            return hardware;
        }

        public static void AcidionarRobosABatalha(Batalha batalha)
        {
            if(batalha.Jogador.Robos.Count > 0)
            {
                while (batalha.Jogador.Robos.Count != 0)
                {
                    var robo = batalha.Jogador.Robos.Dequeue();
                    batalha.Aliados.Add(robo);

                    TextoController.CentralizarTexto($"{robo.Nome} foi ativado para esta batalha!");
                }

                Console.ReadKey();
            }
        }

        public static void RetornarRobosSobreviventes(Batalha batalha)
        {
            var robos = new List<ICriaturaCombatente>();

            foreach(var aliado in batalha.Aliados)
            {
                if (aliado.Tipo == TipoCriatura.Robo) robos.Add(aliado); 
            }

            foreach (var robo in robos)
            {
                batalha.Jogador.Robos.Enqueue(robo);
                TextoController.CentralizarTexto($"{robo.Nome} foi armazenado novamente.");
            }
        }


    }
}
