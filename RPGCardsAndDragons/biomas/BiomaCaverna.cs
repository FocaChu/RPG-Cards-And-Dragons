﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.Inimigos;
using CardsAndDragons;

namespace RPGCardsAndDragons.fases
{
    public class BiomaCaverna : BiomaJogo
    {

        public override string Nome => "A Caverna Escura";

        public override string Descricao => "Uma caverna escura com camâras grandes, cheias de estruturas de pedra antiga. É dificil identificar os vultos e movimentos no escuro.";

        public override int Dificuldade => 2;

        public override List<string> DefinirModelo()
        {
            return new List<string>
            {
           //1234567890123456789012345678901234567890123456789012 = 52
            "                                                    ", //1
		    "                  .-=========-.                     ", //2
		    "                 .===========++++                   ", //3
		    "                :===+++++++++++++=-..               ", //4
		    "             ..=+*====================.             ", //5
		    "            .-=###*#**+=======+++++++=-.            ", //6
		    "          .-====++++===============+++++.           ", //7
		    "          =+===++++===+*******+==========+..        ", //8
		    "        :+##++====+************+=========+++-.      ", //9
		    "       :*=====+++******#%#******=========++++=:     ", //10
		    "     :=====+++++***##%%%%%#******+======+=+*+=-.    ", //11
		    "    :=====++****#%%%%%%%%%%%#*****===++*+===+++-.   ", //12
		    "    :====++****#%%%%%%%%%%%%%%%#***===++====+++=.   ", //13
		    "  .-=++=++*****#%%%%%%%%%%%%%%%%***+==+++++++++++-. ", //14
		    " .===++=+++****#%%%%%%%%%%%%%%%%#**+==++++**+===+=. ", //15
		    " .===+++=++****#%%%%%%%%%%%%%%%%%#**++==+=+++===++: ", //16
		    " ====++==++*****#%%%%%%%%%%%%#*..-+==++++==+++==++++ ", //17
		    " ..==+==++=+#+==++.             :++===++++++==-.... ", //18
		    "     :==++++=====+=.             .-----:-==....     ", //19
		    "     -===:---==++-::.              ....             "  //20
	        };
        }

        public override List<InimigoRPG> DefinirInimigos()
        {
            return new List<InimigoRPG>
            {
             new ToupeiraGrande(),
             new CachorroCavernoso()
            };

        }

        public override List<InimigoRPG> DefinirChefes()
        {
            return new List<InimigoRPG>
            {
                new DragaoLagarto(),
                new DragaoMorcego()
            };
        }

        /*

                          .-=========-.                     
                         .===========++++                   
                        :===+++++++++++++=-..               
                     ..=+*=====================.            
                    .-=###*#**+=======++++++++=-.           
                  .-====++++===============+++++.           
                  =+===++++===+*******+==========+..        
                :+##++====+************+=========+++-.      
               :*=====+++******#%#******=========++++=:     
             :=====+++++***##%%%%%#******+======+=+*+=-.    
            :=====++****#%%%%%%%%%%%#*****===++*+===+++-.   
            :====++****#%%%%%%%%%%%%%%%#***===++====+++=.   
          .-=++=++*****#%%%%%%%%%%%%%%%%***+==+++++++++++-. 
         .===++=+++****#%%%%%%%%%%%%%%%%#**+==++++**+===+=. 
         .===+++=++****#%%%%%%%%%%%%%%%%%#**++==+=+++===++: 
         ====++==++****#%%%%%%%%%%####..-+==++++==+++==++++ 
         ..==+==++=+#+==++.             :++===++++++==-.... 
             :==++++=====+=.             .-----:-==....     
             -===:---==++-::.              ....             


        */
    }
}
