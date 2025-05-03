using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons;
using CardsAndDragons.Condicoes;
using CardsAndDragons.Controllers;

namespace RPGCardsAndDragons.doencas
{
    public interface IEfeitoDoenca
    {
        void Aplicar(ICriaturaCombatente alvo, int nivel);
    }

    public class DanoPercentualVida : IEfeitoDoenca
    {
        public void Aplicar(ICriaturaCombatente alvo, int nivel)
        {

            int porcentagem = nivel * 10;

            double dano = alvo.VidaAtual * (porcentagem / 100.0);

            alvo.SofrerDano((int)dano, true);
        }
    }

    public class ReduzirEscudo : IEfeitoDoenca
    {
        public void Aplicar(ICriaturaCombatente alvo, int nivel)
        {
            CondicaoController.AplicarOuAtualizarCondicao(new ModificacaoEscudo((nivel * -1), 1), alvo.Condicoes);
            TextoController.CentralizarTexto($"{alvo.Nome} sofre de sintomas de fadiga");
        }
    }

    public class ReduzirDano : IEfeitoDoenca
    {
        public void Aplicar(ICriaturaCombatente alvo, int nivel)
        {
            CondicaoController.AplicarOuAtualizarCondicao(new ModificacaoDano((nivel * -1), 1), alvo.Condicoes);
        }
    }

    public class ReduzirDefesa : IEfeitoDoenca
    {
        public void Aplicar(ICriaturaCombatente alvo, int nivel)
        {
            CondicaoController.AplicarOuAtualizarCondicao(new ModificacaoDefesa((nivel * -1), 1), alvo.Condicoes);
        }
    }

    public class ImpedirCura : IEfeitoDoenca
    {
        public void Aplicar(ICriaturaCombatente alvo, int nivel)
        {
            //alvo.PodeReceberCura = false; 
        }
    }

}
