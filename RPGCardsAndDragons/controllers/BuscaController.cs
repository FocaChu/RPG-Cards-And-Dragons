using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons;
using CardsAndDragons.Inimigos;
using CardsAndDragonsJogo;
using RPGCardsAndDragons.condicoes.doencas;
using RPGCardsAndDragons.condicoes.doencas.efeitoDoenca;
using RPGCardsAndDragons.doencas;
using RPGCardsAndDragons.fases;

namespace RPGCardsAndDragons.controllers
{
    public static class BuscaController
    {
        public static List<ClasseRPG> ObterTodasAsClassesDisponiveis()
        {
            return Assembly.GetExecutingAssembly()
            //Isso aqui é o pedaço que faz o Reflection

                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(ClasseRPG)))
                .Select(t => (ClasseRPG)Activator.CreateInstance(t))
                .ToList();
        }

        public static List<EspecieRPG> ObterTodasAsEspeciesDisponiveis()
        {
            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(EspecieRPG)))
                .Select(t => (EspecieRPG)Activator.CreateInstance(t))
                .ToList();
        }

        public static List<TipoDoenca> ObterTodasOsTipoDoencaDisponiveis()
        {
            return Assembly.GetExecutingAssembly()
            //Isso aqui é o pedaço que faz o Reflection

                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(TipoDoenca)))
                /*
                Isso aqui é nosso filtro:
                
                GetType Isso pega todas as classes, structs, interfaces, etc. do nosso projeto.
                
                Where(t => metodo que começa o filtro com suas exigencias. t é a variavel que representa o dado que estamos procurando e a expresão lambida que ta fazned o filtro

                t.IsClass signifca que t(nosso dado) tem que ser uma classe, qualquer outra coisa é ignorada 200%

                !t.IsAbstract signica o nosso dado tem que obrigatoriamente não ser abstrato, ou seja, que de pra instaciar ele

                t.IsSubclassOf(typeof(TipoDoenca)) signifca que tem que ser uma subclasse da nossa classe base TipoDoenca, outras subclasses vão ser ignoradas
                */

                .Select(t => (TipoDoenca)Activator.CreateInstance(t))
                .ToList();

        }

        public static List<BiomaJogo> ObterTodosOsBiomasDisponiveis()
        {
            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BiomaJogo)))
                .Select(t => (BiomaJogo)Activator.CreateInstance(t))
                .ToList();
        }

        public static List<IEfeitoDoenca> ObterTodosOsEfeitoDoencaDisponiveis()
        {

            List<IEfeitoDoenca> efeitosDisponiveis = new List<IEfeitoDoenca>();

            efeitosDisponiveis.Add(new ConfusaoMental());
            efeitosDisponiveis.Add(new DorDeGarganta());
            efeitosDisponiveis.Add(new Fraqueza());
            efeitosDisponiveis.Add(new Hemorragia());
            efeitosDisponiveis.Add(new Intoxicacao());
            efeitosDisponiveis.Add(new Necrose());
            efeitosDisponiveis.Add(new PeleEscaldada());
            efeitosDisponiveis.Add(new Sensibilidade());
            efeitosDisponiveis.Add(new Zumbificacao());

            return efeitosDisponiveis;
        }
    }
}

