﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons;
using CardsAndDragons.ClassesCondicoes;
using CardsAndDragons.Condicoes;
using CardsAndDragons.Controllers;
using CardsAndDragonsJogo;
using RPGCardsAndDragons.doencas;

namespace RPGCardsAndDragons.condicoes.doencas.efeitoDoenca
{
    public class Intoxicacao : IEfeitoDoenca
    {
        public string Nome { get; set; } = "Intoxicação";

        public string Descricao { get; set; } = "O hospédeiro sofre de efeitos de envenenamento.";

        public Intoxicacao() { }

        public Intoxicacao(Intoxicacao original)
        {
            Nome = original.Nome;
            Descricao = original.Descricao;
        }

        public void Aplicar(Batalha batalha, OInimigo alvo, int nivel)
        {
            batalha.Aplicadores.Add(new AplicadorCondicao(new Veneno(nivel, 1), alvo));

            TextoController.CentralizarTexto($"{alvo.Nome} sofre de intoxicação");
        }
    }
}
