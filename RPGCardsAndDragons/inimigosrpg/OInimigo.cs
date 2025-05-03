using System;
using System.Collections.Generic;
using System.Linq;
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
        public InimigoRPG InimigoBase { get; set; }
        public TipoCriatura Tipo { get; set; } = TipoCriatura.Inimigo;

        public int VidaMax { get; set; }

        public int vidaAtual { get; set; }

        public int DanoBase { get; set; }

        public List<ICondicaoTemporaria> Condicoes { get; set; } = new List<ICondicaoTemporaria>();

        public string Nome => InimigoBase.Nome;

        public Bioma BiomaDeOrigem => InimigoBase.BiomaDeOrigem;

        public bool EBoss => InimigoBase.EBoss;

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

        public OInimigo(InimigoRPG baseInimigo)
        {
            InimigoBase = baseInimigo;

            VidaMax = baseInimigo.VidaMax;
            VidaAtual = VidaMax;
            DanoBase = baseInimigo.DanoBase;

        }

        public void RealizarTurno(Batalha batalha)
        {
            int rodada = batalha.rodadaAtual;

            if (vidaAtual >= 0)
            {
                if (PodeUsarHabilidade(rodada) && !CondicaoController.VerificarSilencio(Condicoes))
                {
                    InimigoBase.UsarHabilidade(batalha, this);
                }
                else
                {
                    if (PodeUsarHabilidade(rodada) && CondicaoController.VerificarSilencio(Condicoes))
                    {
                        Console.WriteLine($"{this.Nome} foi silenciado e não pode usar sua habilidade...");
                        InimigoBase.Atacar(batalha, this);
                    }
                    else
                    {
                        InimigoBase.Atacar(batalha, this);
                    }
                }
            }
        }

        public bool PodeUsarHabilidade(int rodada)
        {
            return InimigoBase.PodeUsarHabilidade(rodada);
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
            this.VidaAtual += quantidade;
        }

        public override string ToString()
        {
            return $"{this.Nome} - PS:{this.VidaAtual}/{this.VidaMax}";
        }
    }
}


