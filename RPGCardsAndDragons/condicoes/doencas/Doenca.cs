using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons;
using CardsAndDragons.ClassesCondicoes;
using CardsAndDragonsJogo;

namespace RPGCardsAndDragons.doencas
{
    public class Doenca : ICondicaoContagiosa
    {
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public int Nivel { get; set; }

        public bool EAgrassiva { get; set; }

        public int Duracao { get; set; }

        public ITipoTransmissao Transmissao { get; set; }

        public List<IEfeitoDoenca> Efeitos { get; set; }


        public Doenca(string nome, string descricao, int nivel, bool eagressiva, int duracao, ITipoTransmissao transmissao, List<IEfeitoDoenca> efeitos)
        {
            this.Nome = nome;
            this.Descricao = descricao;
            this.Nivel = nivel;
            this.EAgrassiva = eagressiva;
            this.Duracao = duracao;
            this.Transmissao = transmissao;
            this.Efeitos = efeitos;
        }

        public Doenca(Doenca doenca)
        {
            this.Nome = doenca.Nome;
            this.Descricao = doenca.Descricao;
            this.Nivel = doenca.Nivel;
            this.EAgrassiva = doenca.EAgrassiva;
            this.Duracao = doenca.Duracao;

            this.Transmissao = (ITipoTransmissao)Activator.CreateInstance(doenca.Transmissao.GetType());

            this.Efeitos = PegarEfeitos(doenca.Efeitos);
        }

        public List<IEfeitoDoenca> PegarEfeitos(List<IEfeitoDoenca> efeitos)
        {
            List<IEfeitoDoenca> efeistosCopia = new List<IEfeitoDoenca>();

            for(int i = 0; i < efeitos.Count; i++)
            {
                var efeitoCopia = (IEfeitoDoenca)Activator.CreateInstance(efeitos[i].GetType());

                efeistosCopia.Add(efeitoCopia);
            }

            return efeistosCopia;
        }

        public void AplicarEfeito(OInimigo alvo, Batalha batalha)
        {
            foreach (var efeito in Efeitos)
                efeito.Aplicar(alvo, this.Nivel);

            int chance = Nivel * 10;

            // filtra os alvos: apenas os que ainda não estão com essa doença e não o próprio
            var alvosValidos = batalha.Inimigos
                .Where(i => i != alvo && !i.Condicoes.Any(c => c is ICondicaoContagiosa d && c.Nome == this.Nome))
                .ToList();

            if (!TentarTransmitir(alvosValidos, chance))
            {
                Nivel += EAgrassiva ? 1 : 0;
            }
        }



        public bool TentarTransmitir(List<OInimigo> alvos, int chance)
        {
            // Cria um clone da doença para transmitir aos outros inimigos
            Doenca clone = new Doenca(this);
            return Transmissao.TentarTransmitir(clone, alvos, chance);
        }

        //jogador é imune
        public void AplicarEfeito(Personagem jogador)
        {
        }

        public void AplicarEfeito(ICriaturaCombatente alvo)
        {
        }


        public Doenca Clonar(Doenca doenca)
        {
            return new Doenca(this);
        }

        public bool Expirou()
        {
            return Duracao <= 0;
        }

        public void Atualizar()
        {
        }

        public override string ToString()
        {
            return $"{this.Nome} - Nivel: {this.Nivel} | Vigor: {this.Duracao}";
        }
    }

}
