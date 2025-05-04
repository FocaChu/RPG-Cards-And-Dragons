using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RPGCardsAndDragons.condicoes.doencas;

namespace CardsAndDragons
{
    /*
    
    Codigo a base de Reflection(função extracorporea do codigo que o faz ver a si mesmo)

    Oque ele ta fazendo?
    - Procurando todas as classes concretas que herdam de ClasseRPG

    - Criando instâncias automaticamente de cada uma

    - Retornando uma lista com todas essas classes

    Oque significa "public static class"
    É uma classe auxiliar estática. Isso significa que
    Você não precisa criar um objeto dela pra usar dentro do codigo
    Ex "ClasseHelper.ObterTodasAsClassesDisponiveis();" já funciona direto
    */
    public static class TipoDoencaAjudante
    {
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
    }

}
