﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons;
using CardsAndDragons.Condicoes;
using CardsAndDragons.Controllers;
using CardsAndDragonsJogo;
using RPGCardsAndDragons.condicoes.doencas;

namespace RPGCardsAndDragons.doencas
{
    public interface IEfeitoDoenca
    {
        string Nome { get; set; }

        string Descricao { get; set; }

        void Aplicar(Batalha batalha, OInimigo alvo, int nivel);
    }
}
