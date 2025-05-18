using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.ClassesDasCartas;
using CardsAndDragons.ClassesCondicoes;
using CardsAndDragons.Inimigos;
using CardsAndDragonsJogo;
using CardsAndDragonsJogo.ClassesCartas;
using CardsAndDragons.Controllers;
using CardsAndDragons.Aliados;

namespace CardsAndDragons
{
    public class Personagem : ICriaturaCombatente
    {
        public string Nome { get; set; }
        public TipoCriatura Tipo { get; set; } = TipoCriatura.Jogador;

        public EspecieRPG Especie { get; set; }

        public ClasseRPG Classe { get; set; }

        public Queue<ICriaturaCombatente> Robos { get; set; }

        public List<ICartaUsavel> BaralhoCompleto { get; set; } //Baralho completo do jogador

        public Queue<ICartaUsavel> BaralhoCompra { get; set; } = new Queue<ICartaUsavel>(); // Cartas para comprar

        // BaralhoCompra é uma Fila (Queue), uma estrutura de dados do tipo FIFO (First In, First Out),
        // ou seja, a primeira carta que entra na fila é a primeira a sair.
        //
        // Essa fila representa o "baralho de compra" do jogador.
        // Quando ele compra uma carta, ela vem do topo da fila (com o método Dequeue).
        // Isso simula o ato de "comprar do topo do baralho", tipo pokémon TCG
        //
        // As cartas são embaralhadas ANTES de serem inseridas na Queue, o que garante a aleatoriedade.
        // Ex:
        // O baralho completo é embaralhado e convertido para uma Queue.
        // A cada rodada, o jogador compra cartas até ter 7 na mão, usando BaralhoCompra.Dequeue().
        // Quando a fila fica vazia, o BaralhoDescarte é embaralhado e usado para preencher o BaralhoCompra novamente.
        //
        // Por isso usamos uma Queue em vez de List: ela representa melhor a lógica de "tirar do topo" e mantém tudo simples.


        public List<ICartaUsavel> Mao { get; set; } = new List<ICartaUsavel>(); // Cartas na mão

        public List<ICartaUsavel> BaralhoDescarte { get; set; } = new List<ICartaUsavel>(); // Cartas já usadas


        //guarda as condições que o jogador ta sofrendo

        public List<ICondicaoTemporaria> Condicoes { get; set; }

        #region Status do Jogador

        public int Regeneracao { get; set; }

        public int RegeneracaoPermanente { get; set; }

        public int Ouro { get; set; }

        public int VidaMax { get; set; }

        public int ManaMax { get; set; }

        public int StaminaMax { get; set; }

        public double XpAtual { get; set; }

        public double XpTotal { get; set; }

        public int Nivel { get; set; }

        // Bônus temporários de combate
        public int Escudo { get; set; }
        public int ModificadorDefesa { get; set; }
        public int ModificadorDano { get; set; }

        //Status "atuais" usados no codigo
        private int vidaAtual;
        private int manaAtual;
        private int staminaAtual;

        #endregion

        #region Reguladores de Status

        //Codigo generico que impede os status de subir alem dos limites impostos por deus ou descer abaixo de 0
        private int ReguladorDeStatus(int valor, int valorMax)
        {
            if (valor < 0) return 0;
            //Se o valor (o status atual) for menor do que zero, o codigo breca e poem ele como zero, evitando coisas como -15 de vida.

            if (valor > valorMax) return valorMax;
            //Se o valor (o status atual) for maior que o limite maximo estipulado, o codigo breca e limita ele, impedido vc ter vida infinita por exemplo

            // Se estiver no intervalo permitido manda o valor normal
            return valor;
        }

        public int Ouros
        {
            get => Ouro;
            set => Ouro = ReguladorDeStatus(value, 1000);
            //Pega o ouro atual, seta ela pelos parametros passados e limita ela pelo minimo e maximo
        }

        public int VidaAtual
        {
            get => vidaAtual;
            set => vidaAtual = ReguladorDeStatus(value, VidaMax);
            //Pega a vida atual, seta ela pelos parametros passados e limita ela pelo minimo e maximo
        }

        public int ManaAtual
        {
            get => manaAtual;
            set => manaAtual = ReguladorDeStatus(value, ManaMax);
        }

        public int StaminaAtual
        {
            get => staminaAtual;
            set => staminaAtual = ReguladorDeStatus(value, StaminaMax);
        }

        public List<string> Modelo => throw new NotImplementedException();


        #endregion

        public Personagem(string nome, EspecieRPG especie, ClasseRPG classe)
        {
            this.Nome = nome;
            this.Especie = especie;
            this.Classe = classe;

            this.Ouros = (this.Especie.NomeEspecie == "Anão") ? 150 : 100;
            this.RegeneracaoPermanente = (this.Especie.NomeEspecie == "Vampiro") ? 5 : 0;

            this.Ouros += (this.Classe.NomeClasse == "Engenheiro") ? 25 : 0;

            this.Nivel = 1;
            this.XpTotal = 100;

            this.VidaMax = Classe.VidaMax;
            this.ManaMax = Classe.ManaMax;
            this.StaminaMax = Classe.StaminaMax;

            this.VidaAtual = this.VidaMax;
            this.ManaAtual = this.ManaMax;
            this.StaminaAtual = this.StaminaMax;


            this.Escudo = 0;
            this.ModificadorDano = 0;
            this.ModificadorDefesa = 0;

            // Adiciona o deck da classe ao baralho completo
            this.BaralhoCompleto = PegarCartasIniciais();

            // Embaralha o baralho completo e define como baralho de compra
            this.BaralhoCompra = PersonagemController.EmbaralharCartas(this.BaralhoCompleto);

            this.Robos = new Queue<ICriaturaCombatente>();
            this.Condicoes = new List<ICondicaoTemporaria>();
        }

        public List<ICartaUsavel> PegarCartasIniciais()
        {
            List<ICartaUsavel> cartas = new List<ICartaUsavel>();

            cartas.AddRange(Classe.DefinirCartasIniciais());
            cartas.AddRange(Especie.DefinirCartasIniciais());

            return cartas;
        }

        //Usa uma carta
        public void UsarCarta(Batalha batalha, int indice)
        {
            //verifica se usou ou não a carta
            if (indice < 0 || indice >= Mao.Count) return;

            //cria a carta para verificação
            var carta = Mao[indice];
            bool sucesso = false;

            //ve se é uma carta generica
            if (carta is ICartaUsavel cartaUsavel)
            {
                //usa a dita carta
                sucesso = cartaUsavel.Usar(batalha);
            }
            //Se usou manda pra pilha de descarte e remove da mão
            if (sucesso)
            {
                BaralhoDescarte.Add(carta);
                Mao.RemoveAt(indice);
            }
        }

        public void Curar(int quantidade)
        {
            if (CondicaoController.VerificarCondicao<Queimadura>(Condicoes))
            {
                foreach (var condicao in Condicoes)
                {
                    if (condicao is Queimadura queimadura)
                    {
                        queimadura.Duracao--; // Diminui a duração da queimadura
                        quantidade -= queimadura.Nivel; // Reduz a cura com base no nível da queimadura
                        quantidade = Math.Max(quantidade, 0); // Garante que a cura nunca seja negativa
                    }
                }

                TextoController.CentralizarTexto($"{Nome} teve dificuldade em se curar devido a suas queimaduras...");
            }

            if (quantidade > 0)
            {
                this.VidaAtual += quantidade;
                Console.WriteLine();
                TextoController.CentralizarTexto($"{this.Nome} recebeu {quantidade} de cura!");
            }
        }


        public void SofrerDano(ICriaturaCombatente agressor, int dano, bool foiCondicao, bool aoSofrerDano)
        {
            if (foiCondicao)
            {
                vidaAtual -= dano;
                return;
            }
            if (dano > 0)
            {
                int danoFinal = dano - this.ModificadorDefesa;

                if (Escudo > 0)
                {
                    int danoAbsorvido = Math.Min(danoFinal, Escudo);
                    danoFinal -= danoAbsorvido;
                    Escudo -= danoAbsorvido;
                    Console.WriteLine();
                    TextoController.CentralizarTexto($"{Nome} absorveu {danoAbsorvido} de dano com o escudo!");
                }

                danoFinal = Math.Max(danoFinal, 0);

                this.VidaAtual -= danoFinal;

                Console.WriteLine();
                TextoController.CentralizarTexto($"{this.Nome} recebeu {danoFinal} de dano!");

                CondicaoController.SangrarFerida(this);

                if (aoSofrerDano)
                {
                    AoSofrerDano(agressor, danoFinal);
                }
            }
            else
            {
                Console.WriteLine();
                TextoController.CentralizarTexto($"O golpe foi muito fraco para ferir {this.Nome}...");
            }
        }

        public void AoSofrerDano(ICriaturaCombatente agressor, int quantidade)
        {
            return;
        }

        public void AoMorrer(Batalha batalha, ICriaturaCombatente agressor)
        {
            return;
        }

        //lógica do XP

        public void ContabilizarXp(OInimigo inimigo)
        {
            //double ganhoXp = (50 + (inimigo.Dificuldade * 2));

            double ganhoXp = (this.Especie.NomeEspecie == "Humano") ? ganhoXp = ((60 + (Nivel * 5)) + (inimigo.Dificuldade * 2.5)) : ganhoXp = ((50 + (Nivel * 5)) + (inimigo.Dificuldade * 2));

            ganhoXp += (inimigo.EBoss) ? (inimigo.Dificuldade * 50) : 0;

            XpAtual += ganhoXp;

            Console.WriteLine();
            TextoController.CentralizarTexto($"Você ganhou {ganhoXp} de XP!");

            if (XpAtual >= XpTotal)
            {
                Nivel++;
                XpAtual -= XpTotal;
                XpTotal = XpTotal * (Nivel + 1);
                int pontosXp = 10;
                Console.WriteLine();
                TextoController.CentralizarTexto($"Você subiu para o nivél {this.Nivel}");
                this.VidaMax += 5;
                Console.ReadKey();
                AtribuirPontosXp(pontosXp);
                this.vidaAtual = this.VidaMax;
            }
        }

        protected void AtribuirPontosXp(int pontosXp)
        {
            int option = 0;
            string[] opcoes = { "Vida", "Mana", "Stamina", "Confirmar e sair" };

            while (pontosXp > 0)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("\n\n\n");
                TextoController.CentralizarTexto($"============================== Melhorar Atributos ==============================\n\n\n");

                Console.WriteLine();
                TextoController.CentralizarTexto($"Pontos de atributo disponíveis: {pontosXp}");

                Console.WriteLine();
                TextoController.CentralizarTexto($"Vida: {this.VidaMax}");

                TextoController.CentralizarTexto($"Mana: {this.ManaMax}");

                TextoController.CentralizarTexto($"Stamina: {this.StaminaMax}\n\n");

                TextoController.CentralizarTexto("Use as setas para navegar, Enter para selecionar:\n");

                Console.ForegroundColor = ConsoleColor.White;

                TextoController.CentralizarTexto("============================================================================================================");
                for (int i = 0; i < opcoes.Length; i++)
                {
                    //verifica se essa é a opçao correspondente. Sim? Pinta de cinza escuro. Não? Pinta de branco
                    Console.ForegroundColor = (i == option) ? ConsoleColor.DarkGray : ConsoleColor.White;

                    //verifica se essa é a opçao correspondente. Sim? Poem ">>" na frente. Não? deixa um espaco vazio qualquer
                    TextoController.CentralizarTexto($"{(i == option ? ">>" : "  ")} {opcoes[i]}");
                }
                Console.ResetColor();
                TextoController.CentralizarTexto("============================================================================================================");

                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (option > 0) option--;
                        else option = opcoes.Length - 1;
                        break;

                    case ConsoleKey.DownArrow:
                        if (option < opcoes.Length - 1) option++;
                        else option = 0;
                        break;

                    case ConsoleKey.Enter:
                        if (option == 3) return; // Sai do menu
                        else
                        {
                            Console.WriteLine();
                            TextoController.CentralizarTexto($"Quantos pontos deseja aplicar em {opcoes[option]}?");
                            TextoController.CentralizarLinha("→ ");

                            if (int.TryParse(Console.ReadLine(), out int quantidade) && quantidade > 0 && quantidade <= pontosXp)
                            {
                                switch (option)
                                {
                                    case 0:
                                        this.VidaMax = VidaMax + (1 * quantidade);
                                        break;
                                    case 1:
                                        this.ManaMax = ManaMax + (1 * quantidade);
                                        break;
                                    case 2:
                                        this.StaminaMax = StaminaMax + (1 * quantidade);
                                        break;
                                }

                                pontosXp -= quantidade;
                            }
                            else
                            {
                                TextoController.CentralizarTexto("Valor inválido.");
                                Console.ReadKey();
                            }
                        }
                        break;
                }
            }
        }

        public void RealizarTurno(Batalha batalha)
        {
            throw new NotImplementedException();
        }
    }
}
