using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons;
using CardsAndDragons.ClassesCondicoes;
using CardsAndDragons.Controllers;
using CardsAndDragonsJogo;
using RPGCardsAndDragons.condicoes.doencas;

namespace RPGCardsAndDragons.doencas
{
    public class Doenca : ICondicaoContagiosa
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Nivel { get; set; }      // nível fixo ao ser transmitido
        public int NivelAtual { get; set; }     // nível que pode evoluir no hospedeiro
        public bool EAgrassiva { get; set; }
        public int Duracao { get; set; }
        public ITipoTransmissao Transmissao { get; set; }
        public List<IEfeitoDoenca> Efeitos { get; set; }
        public TipoDoenca Tipo { get; set; }

        public Doenca(TipoDoenca tipo, int nivel, bool eagressiva, int duracao, ITipoTransmissao transmissao, List<IEfeitoDoenca> efeitos, string nome)
        {
            this.Tipo = tipo;
            this.Descricao = tipo.Descricao;
            this.Nivel = nivel;
            this.NivelAtual = nivel;
            this.EAgrassiva = eagressiva;
            this.Duracao = duracao;
            this.Transmissao = transmissao;
            this.Efeitos = efeitos;

            this.Nome = nome;
        }

        public Doenca(Doenca doenca)
        {
            this.Tipo = doenca.Tipo;
            this.Nome = doenca.Nome;
            this.Descricao = doenca.Descricao;
            this.Nivel = doenca.Nivel;
            this.NivelAtual = doenca.Nivel;
            this.EAgrassiva = doenca.EAgrassiva;
            this.Duracao = doenca.Duracao;
            this.Transmissao = (ITipoTransmissao)Activator.CreateInstance(doenca.Transmissao.GetType());
            this.Efeitos = doenca.Efeitos;
        }

        public void AplicarEfeito(OInimigo alvo, Batalha batalha)
        {
            foreach (var efeito in Efeitos)
            {
                Console.WriteLine();
                efeito.Aplicar(batalha, alvo, this.NivelAtual);
            }

            int chance = NivelAtual * 5;

            var cloneParaTransmitir = new Doenca(this);

            if (!cloneParaTransmitir.TentarTransmitir(batalha, chance, this))
            {
                // Aumenta o nível SOMENTE da original
                NivelAtual += EAgrassiva ? 1 : 0;
            }
        }


        public bool TentarTransmitir(Batalha batalha, int chance, Doenca clone)
        {
            return Transmissao.TentarTransmitir(clone, batalha, chance);
        }

        public bool Expirou()
        {
            return Duracao <= 0;
        }

        public override string ToString()
        {
            return $"{this.Nome} - Nivel: {this.NivelAtual} | Vigor: {this.Duracao}";
        }

        public void Atualizar()
        {
            // Define a chance base de redução (por exemplo, 50%)
            int chanceBase = 50;

            // Reduz a chance com base no NivelAtual da doença
            int chanceDeReduzir = Math.Max(5, chanceBase - (NivelAtual * 5));
            // A chance mínima é 5% para evitar que fique impossível reduzir

            // Gera um número aleatório entre 1 e 100
            Random random = new Random();
            int numeroAleatorio = random.Next(1, 101);

            // Verifica se a duração será reduzida
            if (numeroAleatorio <= chanceDeReduzir)
            {
                Duracao--;
                TextoController.CentralizarTexto($"{Nome}: A duração foi reduzida! Turnos restantes: {Duracao}");
            }
            else
            {
                TextoController.CentralizarTexto($"{Nome}: A duração não foi reduzida neste turno.");
            }

            // Garante que a duração não fique negativa
            if (Duracao < 0)
            {
                Duracao = 0;
            }
        }



        public void AplicarEfeito(Personagem jogador)
        {
            throw new NotImplementedException();
        }
        public void AplicarEfeito(ICriaturaCombatente criatura)
        {
            throw new NotImplementedException();
        }
    }


}
