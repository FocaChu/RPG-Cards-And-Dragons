using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsAndDragons.Cartas;
using CardsAndDragons.ClassesCondicoes;
using CardsAndDragons.Condicoes;
using CardsAndDragons.Controllers;
using CardsAndDragonsJogo;

namespace CardsAndDragons.Aliados
{
    public enum TipoSoftware
    {
        Cura = 0,
        Ataque = 1,
        Amplificacao = 2,
        Sabotagem = 3,
    }

    public enum TipoHardware
    {
        Eficiente = 0,
        Resistente = 1,
        Multitarefa = 2,
        Economico = 3
    }

    public class RoboAliado : ICriaturaCombatente
    {
        public string Nome { get; set; }

        public TipoCriatura Tipo { get; set; } = TipoCriatura.Robo;

        public TipoSoftware Software { get; set; }

        public TipoHardware Hardware { get; set; }


        public int vidaAtual { get; set; }

        public int VidaMax { get; set; }

        public int Eficiencia { get; set; }

        public List<string> Modelo { get; set; }

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

        public RoboAliado(TipoSoftware software, TipoHardware hardware, string nome)
        {
            this.Software = software;
            this.Hardware = hardware;

            this.Nome = nome;

            this.VidaMax = DefinirVidaBase(hardware);
            this.VidaAtual = VidaMax;
            this.Eficiencia = DefinirEficienciaBase(hardware);
            this.Modelo = DefinirModelo(this.Hardware);

            this.Condicoes = new List<ICondicaoTemporaria>();
        }

        private int DefinirEficienciaBase(TipoHardware hardware)
        {
            switch (hardware)
            {
                case TipoHardware.Eficiente:
                    return 20;

                case TipoHardware.Resistente:
                    return 10;

                case TipoHardware.Multitarefa:
                    return 10;

                case TipoHardware.Economico:
                    return 15;

                default:
                    return 10;
            }
        }

        private int DefinirVidaBase(TipoHardware hardware)
        {
            switch (hardware)
            {
                case TipoHardware.Eficiente:
                    return 50;

                case TipoHardware.Resistente:
                    return 80;

                case TipoHardware.Multitarefa:
                    return 50;

                case TipoHardware.Economico:
                    return 65;

                default:
                    return 60;
            }
        }

        private List<string> DefinirModelo(TipoHardware hardware)
        {
            switch (hardware)
            {
                case TipoHardware.Eficiente:
                    return new List<string>()
        {
            //1234567890123456789012345 = 25
            @"                        ", //1 
            @"           [-]          ", //2
            @"          (   )         ", //3
            @"           |_|          ", //4
            @"        __/===\__       ", //5
            @"       //| o=o |\\      ", //6
            @"     <]  | o=o |  [>    ", //7
            @"         \=====/        ", //8
            @"        / / | \ \       ", //9
            @"       <_________>      ", //10
             // preencher 10 linhas no total
        };

                case TipoHardware.Resistente:
                    return new List<string>()
        {
            //1234567890123456789012345 = 25
            @"                        ", //1 
            @"           [-]          ", //2
            @"          (   )         ", //3
            @"           |_|          ", //4
            @"        __/===\__       ", //5
            @"       //| o=o |\\      ", //6
            @"     <]  | o=o |  [>    ", //7
            @"         \=====/        ", //8
            @"        / / | \ \       ", //9
            @"       <_________>      ", //10
             // preencher 10 linhas no total
        };

                case TipoHardware.Multitarefa:
                    return new List<string>()
        {
            //1234567890123456789012345 = 25
            @"                        ", //1 
            @"           [-]          ", //2
            @"          (   )         ", //3
            @"           |_|          ", //4
            @"        __/===\__       ", //5
            @"       //| o=o |\\      ", //6
            @"     <]  | o=o |  [>    ", //7
            @"         \=====/        ", //8
            @"        / / | \ \       ", //9
            @"       <_________>      ", //10
             // preencher 10 linhas no total
        };

                case TipoHardware.Economico:
                    return new List<string>()
        {
            //1234567890123456789012345 = 25
            @"                        ", //1 
            @"           [-]          ", //2
            @"          (   )         ", //3
            @"           |_|          ", //4
            @"        __/===\__       ", //5
            @"       //| o=o |\\      ", //6
            @"     <]  | o=o |  [>    ", //7
            @"         \=====/        ", //8
            @"        / / | \ \       ", //9
            @"       <_________>      ", //10
             // preencher 10 linhas no total
        };

                default:
                    return null;
            }

            /*
            
       
      [-]
     (   )
      |_|
   __/===\__
  //| o=o |\\
<]  | o=o |  [>
    \=====/
   / / | \ \
  <_________>
            
            */
        }

        public void RealizarTurno(Batalha batalha)
        {
            int qtdVezes = this.Hardware == TipoHardware.Multitarefa ? 2 : 1;

            // lógica baseada em Software
            for (int i = 0; i < qtdVezes; i++)
            {
                switch (Software)
                {
                    case TipoSoftware.Cura:
                        AcaoCurar(batalha);
                        break;
                    case TipoSoftware.Ataque:
                        AcaoAtacar(batalha);
                        break;
                    case TipoSoftware.Amplificacao:
                        AcaoAmplificar(batalha);
                        break;
                    case TipoSoftware.Sabotagem:
                        AcaoSabotar(batalha);
                        break;
                }

                Console.ReadKey();
            }

        }

        private void AcaoCurar(Batalha batalha)
        {
            int curaTotal = this.Eficiencia + this.ModificadorDefesa;

            List<ICriaturaCombatente> alvos = BatalhaController.ObterAlvosAliados(batalha);

            List<ICriaturaCombatente> alvosValidos = new List<ICriaturaCombatente>();

            foreach (var alvo in alvos)
            {
                if (alvo.VidaAtual < alvo.VidaMax || CondicaoController.VerificarCondicao<Sangramento>(alvo.Condicoes) || CondicaoController.VerificarCondicao<Veneno>(alvo.Condicoes))
                {
                    alvosValidos.Add(alvo);
                }
            }

            if (alvosValidos.Count > 0)
            {
                var alvo = AlvoController.EscolherAlvoAliadoAleatorio(alvosValidos);
                if (alvo.VidaAtual < alvo.VidaMax)
                {
                    alvo.Curar(curaTotal);
                }

                if(alvo.Condicoes.Count > 0)
                {
                    foreach (var condicao in alvo.Condicoes)
                    {
                        if (condicao is Sangramento sangramento)
                        {
                            if(sangramento.Duracao > 0)
                            {
                                sangramento.Atualizar();
                                TextoController.CentralizarTexto($"{this.Nome} tratou o sangramento de {alvo.Nome}");
                            }
                        }
                    }
                    foreach (var condicao in alvo.Condicoes)
                    {
                        if (condicao is Veneno veneno)
                        {
                            if(veneno.Duracao > 0)
                            {
                                veneno.Atualizar();
                                TextoController.CentralizarTexto($"{this.Nome} tratou o envenamento de {alvo.Nome}");
                            }
                        }
                    }
                }

            }
            else
            {
                Console.WriteLine();
                TextoController.CentralizarTexto($"{this.Nome} não tinha aliados feridos para curar");

            }

        }

        private void AcaoAtacar(Batalha batalha)
        {
            int danoTotal = this.Eficiencia + ModificadorDano;

            Random random = new Random();

            var alvo = AlvoController.EscolherInimigoAleatorio(batalha.Inimigos);

            alvo.SofrerDano(this, danoTotal, false, true);

        }

        private void AcaoAmplificar(Batalha batalha)
        {
            int modificador = this.Eficiencia + ModificadorDefesa;

            int nivel = modificador / 4;
            int duracao = modificador / 6;

            var alvo = AlvoController.EscolherAlvoAleatorioDosAliados(batalha);

            Random random = new Random();
            int indiceAleatorio = random.Next(2);

            switch (indiceAleatorio)
            {
                case 0:
                    CondicaoController.AplicarOuAtualizarCondicao(new ModificacaoEscudo(nivel, duracao), alvo.Condicoes);
                    TextoController.CentralizarTexto($"{this.Nome} aplicou um escudo em {alvo.Nome}");
                    break;

                case 1:
                    CondicaoController.AplicarOuAtualizarCondicao(new ModificacaoDano(nivel, duracao), alvo.Condicoes);
                    TextoController.CentralizarTexto($"{this.Nome} aplicou um aumeneto de dano em {alvo.Nome}");
                    break;

                case 2:
                    CondicaoController.AplicarOuAtualizarCondicao(new ModificacaoDefesa(nivel, duracao), alvo.Condicoes);
                    TextoController.CentralizarTexto($"{this.Nome} aplicou um aumento de defesa em {alvo.Nome}");
                    break;
            }

        }

        private void AcaoSabotar(Batalha batalha)
        {
            int modificador = this.Eficiencia + ModificadorDano;

            int nivel = modificador / 4;
            int duracao = modificador / 6;

            Random random = new Random();
            int indiceAleatorio = random.Next(3);

            var alvo = AlvoController.EscolherInimigoAleatorio(batalha.Inimigos);

            switch (indiceAleatorio)
            {
                case 0:
                    CondicaoController.AplicarOuAtualizarCondicao(new Veneno(nivel, duracao), alvo.Condicoes);
                    TextoController.CentralizarTexto($"{this.Nome} envenenou {alvo.Nome}");
                    break;

                case 1:
                    CondicaoController.AplicarOuAtualizarCondicao(new Maldicao(nivel), alvo.Condicoes);
                    TextoController.CentralizarTexto($"{this.Nome} amaldiçoou {alvo.Nome}");
                    break;

                case 2:
                    CondicaoController.AplicarOuAtualizarCondicao(new ModificacaoDano((nivel * -1), duracao), alvo.Condicoes);
                    TextoController.CentralizarTexto($"{this.Nome} aplicou uma redução de dano em {alvo.Nome}");
                    break;

                case 3:
                    CondicaoController.AplicarOuAtualizarCondicao(new ModificacaoDefesa((nivel * -1), duracao), alvo.Condicoes);
                    TextoController.CentralizarTexto($"{this.Nome} aplicou uma redução de defesa em {alvo.Nome}");
                    break;
            }

        }


        public void Curar(int quantidade)
        {
            this.VidaAtual += quantidade;
            Console.WriteLine();
            TextoController.CentralizarTexto($"{this.Nome} recebeu {quantidade} de cura\n");
        }

        public void SofrerDano(ICriaturaCombatente agressor, int quantidade, bool foiCondicao, bool aoSofrerDano)
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
            else
            {
                TextoController.CentralizarTexto($"{this.Nome} é imune a essa condição\n");
            }
        }
        public void AoSofrerDano(ICriaturaCombatente agressor, int quantidade)
        {
            return;
        }

        public void AoMorrer(Batalha batalha, ICriaturaCombatente agressor)
        {
            return;
        }
    }
}


