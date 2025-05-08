using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons;
using CardsAndDragons.ClassesCondicoes;
using CardsAndDragons.Controllers;
using CardsAndDragons.Inimigos;

namespace CardsAndDragonsJogo
{
    public class OInimigo : ICriaturaCombatente
    {
        public int ID { get; set; }

        public InimigoRPG InimigoBase { get; set; }

        public TipoCriatura Tipo { get; set; } = TipoCriatura.Inimigo;

        public int VidaMax { get; set; }

        public int vidaAtual { get; set; }

        public int DanoBase { get; set; }

        public List<ICondicaoTemporaria> Condicoes { get; set; } = new List<ICondicaoTemporaria>();

        public string Nome => InimigoBase.Nome;

        public bool EBoss => InimigoBase.EBoss;

        public int Dificuldade => InimigoBase.Dificuldade;

        public int RecargaHabilidade { get; set; }

        public List<string> Modelo => InimigoBase.Modelo;

        private int ReguladorDeStatus(int valor, int valorMax)
        {
            if (valor < 0) return 0;
            //Se o valor (o status atual) for menor do que zero, o codigo breca e poem ele como zero, evitando coisas como -15 de vida.

            if (valor > valorMax) return valorMax;
            //Se o valor (o status atual) for maior que o limite maximo estipulado, o codigo breca e limita ele, impedido vc ter vida infinita por exemplo

            // Se estiver no intervalo permitido manda o valor normal
            return valor;
        }

        public int VidaAtual
        {
            get => vidaAtual;
            set => vidaAtual = ReguladorDeStatus(value, VidaMax);
            //Pega a vida atual, seta ela pelos parametros passados e limita ela pelo minimo e maximo
        }

        // Bônus temporários de combate
        public int Escudo { get; set; }

        public int ModificadorDefesa { get; set; }

        public int ModificadorDano { get; set; }

        public static int id = 0;

        public OInimigo(InimigoRPG baseInimigo)
        {
            id++;
            this.ID = id; // Atribui um ID único a cada instância de OInimigo
            InimigoBase = baseInimigo;

            VidaMax = baseInimigo.VidaMax;
            VidaAtual = VidaMax;
            DanoBase = baseInimigo.DanoBase;
            RecargaHabilidade = baseInimigo.RecargaHabilidade;

        }

        public void RealizarTurno(Batalha batalha)
        {
            int rodada = batalha.rodadaAtual;

            if (CondicaoController.VerificarCondicao<Atordoamento>(Condicoes))
            {
                TextoController.CentralizarTexto($"{this.Nome} está atordoado e não pode agir nesta rodada!");
                return;
            }

            else if (CondicaoController.VerificarCondicao<Paranoia>(Condicoes))
            {
                int nivelParanoia = 0;
                foreach (var condicao in Condicoes)
                {
                    if (condicao is Paranoia paranoia)
                    {
                        nivelParanoia = paranoia.Nivel;
                        break;
                    }
                }

                int chance = nivelParanoia * 10;
                BatalhaController.GerarRNG(chance);

                if (chance == 0)
                {
                    TextoController.CentralizarTexto($"{this.Nome} está confuso e em panico!");
                    var alvo = AlvoController.EscolherInimigoAleatorio(batalha.Inimigos);

                    Atacar(batalha, alvo);
                    return;
                }

            }

            else if (CondicaoController.VerificarCondicao<Atracao>(Condicoes))
            {
                int nivelAtracao = 0;
                foreach (var condicao in Condicoes)
                {
                    if (condicao is Atracao atracao)
                    {
                        nivelAtracao = atracao.Nivel;
                        break;
                    }
                }

                int chance = nivelAtracao * 10;
                BatalhaController.GerarRNG(chance);

                if (chance == 0)
                {
                    TextoController.CentralizarTexto($"{this.Nome} está apaixonado e evita {batalha.Jogador.Nome}!");
                    var alvo = AlvoController.EscolherAlvoAliadoAleatorio(batalha.Aliados);

                    Atacar(batalha, alvo);
                    return;
                }
            }
            else
            {
                var alvo = AlvoController.EscolherAlvoAleatorioDosAliados(batalha);
                Atacar(batalha, alvo);
            }
        }

        public void Atacar(Batalha batalha, ICriaturaCombatente alvo)
        {
            if (vidaAtual >= 0)
            {
                if (batalha.rodadaAtual % RecargaHabilidade == 0)
                {
                    if (CondicaoController.VerificarCondicao<Silencio>(Condicoes))
                    {
                        Console.WriteLine($"{this.Nome} foi silenciado e não pode usar sua habilidade...");

                        InimigoBase.Atacar(batalha, this, alvo);
                    }
                    else
                    {
                        InimigoBase.UsarHabilidade(batalha, this, alvo);
                    }

                }
                else
                {
                    InimigoBase.Atacar(batalha, this, alvo);
                }
            }
        }

        public void SofrerDano(int quantidade, bool foiCondicao)
        {
            if (!foiCondicao)
            {
                if (quantidade > 0)
                {
                    int danoFinal = quantidade;
                    danoFinal -= this.ModificadorDefesa;

                    if (Escudo > 0)
                    {
                        int danoAbsorvido = Math.Min(danoFinal, Escudo);
                        danoFinal -= danoAbsorvido;
                        Escudo -= danoAbsorvido;
                        Console.WriteLine();
                        TextoController.CentralizarTexto($"{this.Nome} absorveu {danoAbsorvido} de dano com o escudo!");
                    }

                    danoFinal = Math.Max(danoFinal, 0);

                    this.VidaAtual -= danoFinal;

                    Console.WriteLine();
                    TextoController.CentralizarTexto($"{this.Nome} recebeu {danoFinal} de dano!");

                    CondicaoController.SangrarFerida(this);
                }
                else
                {
                    Console.WriteLine();
                    TextoController.CentralizarTexto($"O golpe foi muito fraco para ferir {this.Nome}...");
                }
            }
            else
            {
                this.VidaAtual -= quantidade;
                TextoController.CentralizarTexto($"{this.Nome} recebeu {quantidade} de dano!");
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

        public override string ToString()
        {
            return $"{this.Nome} - PS:{this.VidaAtual}/{this.VidaMax}";
        }
    }
}


