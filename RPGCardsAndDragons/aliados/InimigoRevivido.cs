﻿using System;
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

        public int RecargaHabilidade { get; set; }

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
            this.RecargaHabilidade = InimigoBase.RecargaHabilidade;
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
                    var alvo = AlvoController.EscolherAlvoAleatorioDosAliados(batalha);

                    Atacar(batalha, alvo);
                    return;
                }

            }

            else
            {
                var alvo = AlvoController.EscolherInimigoAleatorio(batalha.Inimigos);
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
            InimigoBase.AoSofrerDano(agressor, quantidade);
        }

        public void AoMorrer(Batalha batalha, ICriaturaCombatente agressor)
        {
            InimigoBase.AoMorrer(batalha, this, agressor);
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
    }
}
