using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.ClassesCondicoes;
using CardsAndDragons.Controllers;
using CardsAndDragons.Inimigos;
using CardsAndDragonsJogo;

namespace CardsAndDragons.Aliados
{
    public class InimigoRevivido : ICriaturaCombatente
    {
        private InimigoRPG InimigoBase { get; set; }

        public TipoCriatura Tipo { get; set; } = TipoCriatura.InimigoRevivido;

        public string Nome => InimigoBase.Nome + " (Revivido)";

        public int vidaAtual { get; set; }

        public int VidaMax { get; set; }

        public int DanoBase => InimigoBase.DanoBase;

        public List<string> Modelo => InimigoBase.Modelo;

        public int Escudo { get; set; }

        public int ModificadorDefesa { get; set; }

        public int ModificadorDano { get; set; }

        public List<ICondicaoTemporaria> Condicoes { get; set; }

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

        public InimigoRevivido(InimigoRPG inimigoBase)
        {
            this.InimigoBase = inimigoBase;

            this.Condicoes = new List<ICondicaoTemporaria>();

            this.VidaMax = inimigoBase.VidaMax - (inimigoBase.VidaMax / 4);
            this.VidaAtual = VidaMax;
        }

        public void RealizarTurno(Batalha batalha)
        {
            int rodada = batalha.rodadaAtual;

            if (PodeUsarHabilidade(rodada) && !CondicaoController.VerificarCondicao<Silencio>(Condicoes))
            {
                InimigoBase.UsarHabilidadeComoAliado(batalha, this);
            }
            else
            {
                if (PodeUsarHabilidade(rodada) && CondicaoController.VerificarCondicao<Silencio>(Condicoes))
                {
                    Console.WriteLine($"{this.Nome} foi silenciado e não pode usar sua habilidade...");
                    InimigoBase.AtacarComoAliado(batalha, this);
                }
                else
                {
                    InimigoBase.AtacarComoAliado(batalha, this);
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
            }
        }

        public void Curar(int quantidade)
        {
            this.VidaAtual += quantidade;
        }
    }
}
