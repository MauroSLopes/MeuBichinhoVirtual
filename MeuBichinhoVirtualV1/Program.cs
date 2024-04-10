using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Xml.Schema;
using System.Drawing;

namespace MeuBichinhoVirtualV1
{
    /*
                - Mecanicas do jogo -
                Inicio do jogo por prompt de comando.
                Manter dados do bichinho mesmo ao finalizar o jogo.
                Salva os status de Saciado, Felciidade e Limpeza em um arquivo de texto.
                Status diminuem com o tempo.
    */

    internal class Program  
    {
        static void Main(string[] args)
        {
            

            // Variaveis de entrada/saida
            string entrada = "sim";

            // Informações necessarias para o bichinho.
            string nomeBichinho = "";
            string nomeDono = "";
            float statusSaciado = 100;
            float statusFelicidade = 100;
            float statusLimpeza = 100;

            // Variaveis da Imagem

            string caminhoFotoPet = Environment.CurrentDirectory + "\\Gato.jpg";

            // Diminuir status do bichinho
            Random rand = new Random();

            // Inicio do programa
            Console.WriteLine("Olá, Bem Vindo ao Meu Bichinho Virtual!");
            ExibirImagem(caminhoFotoPet, 30, 15);
            Console.WriteLine("Imagem por: Riniik on DeviantArt!");
            Console.WriteLine("Qual é o nome do seu bichinho?");
            nomeBichinho = Console.ReadLine();

            // Carregar o bixinho/Criar o bixinho
            // Coletar status atuais do bixinho pelo arquivo
            BichinhoLoader(nomeBichinho, ref nomeDono, ref statusSaciado, ref statusLimpeza, ref statusFelicidade);

            // Laço do jogo
            while (entrada.ToLower() != "nada" && statusLimpeza > 0 && statusSaciado > 0 && statusFelicidade > 0)
            {
                // Alteração dos status
                BichinhoStatusDecreaser(rand, ref statusFelicidade, ref statusLimpeza, ref statusSaciado);
                Console.Clear();

                // Interação com o usuário
                BichinhoToPlayerStatus(statusSaciado, statusFelicidade, statusLimpeza);

                // O que fazer? / Incremento dos Status

                BichinhoTalk(rand);
                BichinhoPlayerInteraction(ref entrada, nomeBichinho, nomeDono);
                BichinhoStatusIncrease(rand, nomeBichinho, entrada, ref statusFelicidade, ref statusSaciado, ref statusLimpeza);

            }

            // Saida do jogo

            BichinhoExitGameCondition(statusFelicidade, statusSaciado,statusLimpeza,nomeDono);

            // Armazem de informações.
            BichinhoSaver(nomeBichinho, nomeDono, statusSaciado, statusLimpeza, statusFelicidade);

            Console.ReadKey();

        }
        static void BichinhoLoader(string nomeBichinho, ref string nomeDono, ref float statusSaciado, ref float statusLimpeza, ref float statusFelicidade)
        {
            string localArquivo = Environment.CurrentDirectory + "\\" + nomeBichinho + ".txt";
            if (File.Exists(localArquivo))
            {
                string[] dadosBichinho = File.ReadAllLines(localArquivo);
                nomeDono = dadosBichinho[1];
                statusSaciado = float.Parse(dadosBichinho[2]);
                statusLimpeza = float.Parse(dadosBichinho[3]);
                statusFelicidade = float.Parse(dadosBichinho[4]);
                // Bichinho morto em seção anterior.

                if (statusLimpeza <= 0 || statusSaciado <= 0 || statusFelicidade <= 0)
                {
                    Console.WriteLine("Parece que seu bixinho não está mais aqui...");
                    Console.WriteLine("Vamos cuidar dele e convence-lo a voltar!");
                    statusFelicidade = 100;
                    statusLimpeza = 100;
                    statusSaciado = 100;
                    Thread.Sleep(3000);
                    Console.WriteLine("Encontramos! Ele parece bem agora... tome mais cuidado dessa vez.");
                    Thread.Sleep(5000);
                }
            }
            else
            {
                Console.WriteLine("Oi, eu sou o/a {0} qual é o seu nome?", nomeBichinho);
                Console.Write("Digite o nome do dono: ");
                nomeDono = Console.ReadLine();
            }
        }

        static void BichinhoSaver(string nomeBichinho, string nomeDono, float statusSaciado, float statusLimpeza, float statusFelicidade)
        {
            string localArquivo = Environment.CurrentDirectory + "\\" + nomeBichinho + ".txt";

            string conteudoDoArquivo = nomeBichinho + Environment.NewLine;
            conteudoDoArquivo += nomeDono + Environment.NewLine;
            conteudoDoArquivo += statusSaciado + Environment.NewLine;
            conteudoDoArquivo += statusLimpeza + Environment.NewLine;
            conteudoDoArquivo += statusFelicidade + Environment.NewLine;

            File.WriteAllText(localArquivo, conteudoDoArquivo);
        }

        static void BichinhoTalk(Random rand)
        {
            string[] frasesBichinho = {
            "Hoje o dia foi divertido, quase comi o sofá inteiro!",
            "Estava morrendo de saudades, é dificil ficar sem você...",
            "Você já viu o show da xuxa, vi hoje é bem divertido!",
            "Você gosta de batatas? eu adoro batatas, por favor me dê batatas!",
            "Estava me sentindo meio sozinho e abandonado, você não me ama mais?",
            "Fala comigo, Raposo não pegue! Raposo não pegue!",
            "Eu sou o Batman da sua Gotham city.",
            "Eu quero ser um parasita pra sempre sempre ficar com você :3",
            "Sua certidão de nascimento é um pedido de desculpas da fabrica de camisinha.",
            "Calma, muita informação",
            "Inha",
            "Você é desempregado!",
            "Sabia que cabem 2 guaxinims no cu de um ser humano?",
            "Amiga vc tá faraônica babilónica mesopotámica lacronica divonica bafonica bafonerica estratroferica astronómica sambastica revolucionaria deslumbrante babadeira arrasante",
            "Vou te deixar sujo e te dar um filho pra ver se tu sobrevive fdp",
            "Eu sei onde você mora, eu sei que você guarda todos os seus sapatos e não é no guarda roupa",
            "Poggers",
            "Avavago",
            "Violetas são azuis, rosas são vermelhas... Vacilou perdeu a carteira"
            };

            Console.WriteLine("Hoje eu estava pensando...");
            Console.WriteLine(frasesBichinho[rand.Next(frasesBichinho.Length)]);
        }

        static void BichinhoStatusDecreaser(Random rand, ref float statusFelicidade, ref float statusLimpeza, ref float statusSaciado)
        {
            int statusHandler = rand.Next(3);

            switch (statusHandler)
            {
                case 0:
                    statusFelicidade -= rand.Next(35);
                    break;
                case 1:
                    statusLimpeza -= rand.Next(35);
                    break;
                case 2:
                    statusSaciado -= rand.Next(35);
                    break;
            }
        }

        static void BichinhoToPlayerStatus(float statusSaciado, float statusFelicidade, float statusLimpeza)
        {
            if (statusSaciado < 70)
            {
                Console.WriteLine("Eu estou com muita fome! ");
                Console.WriteLine("Nada seria melhor que uma comidinha agora.");
            }

            if (statusFelicidade < 70)
            {
                Console.WriteLine("Estou entediado... ");
                Console.WriteLine("Queria brincar um pouco...");
            }

            if (statusLimpeza < 70)
            {
                Console.WriteLine("Estou muito fedido");
                Console.WriteLine("Preciso lavar o meu pé desse xulé");
            }

            if (statusLimpeza >= 70 && statusFelicidade >= 70 && statusSaciado >= 70)
            {
                Console.WriteLine("Estou muito bem!");
            }

            // Timing para frase desaparecer
            Thread.Sleep(2000);
            Console.Clear();
        }

        static void BichinhoPlayerInteraction(ref string entrada, string nomeBichinho, string nomeDono)
        { 
            Console.WriteLine("");
            Console.WriteLine("O que vamos fazer hoje {0}?", nomeDono);
            Console.Write("Brincar/Comer/Banho/Nada: ");
            entrada = Console.ReadLine().ToLower();
            Console.WriteLine("");
        }

        static void BichinhoStatusIncrease(Random rand, string nomeBichinho, string entrada, ref float statusFelicidade, ref float statusSaciado, ref float statusLimpeza)
        {
            switch (entrada)
            {
                case "brincar":
                    statusFelicidade += rand.Next(10, 40);

                    // Controle de felicidade maxima
                    if (statusFelicidade > 100)
                    {
                        statusFelicidade = 100;
                        Console.WriteLine("{0} fica muito feliz com sua atenção.", nomeBichinho);
                    }
                    else
                    {
                        Console.WriteLine("Brincadeira de criança\r\nComo é bom, como é bom\r\nGuardo ainda na lembrança\r\nComo é bom, como é bom");
                    }

                    break;

                case "comer":

                    statusSaciado += rand.Next(10, 40);

                    // Controle de saciedade maxima
                    if (statusSaciado > 100)
                    {
                        statusSaciado = 100;
                        Console.WriteLine("{0} está de buxin chei.", nomeBichinho);
                    }
                    else
                    {
                        Console.WriteLine("Fazer uma sopa pá nós.");
                    }

                    break;

                case "banho":
                    statusLimpeza += rand.Next(10, 40);

                    // Controle de Limpeza maxima
                    if (statusLimpeza > 100)
                    {
                        statusLimpeza = 100;
                        Console.WriteLine("{0} se sente hiper mega limpo!", nomeBichinho);
                    }
                    else
                    {
                        Console.WriteLine("Lava, lava, lava\r\nLava, lava, lava\r\nUma orelha, uma orelha\r\nOutro orelha, outra orelha");
                    }

                    break;

                case "nada":
                    Console.WriteLine("Tchauu...");
                    break;

            }
            Thread.Sleep(2000);
            Console.Clear();
        }

        static void BichinhoExitGameCondition(float statusFelicidade, float statusSaciado, float statusLimpeza, string nomeDono)
        {
            if (statusFelicidade <= 0)
            {
                Console.WriteLine("Seu bixinho fugiu... ele não parecia muito feliz.");
            }
            else if (statusSaciado <= 0)
            {
                Console.WriteLine("Seu bixinho passou mal de fome... tadinho.");
            }
            else if (statusLimpeza <= 0)
            {
                Console.WriteLine("Seu bixinho foi levado pelo caminhão de lixo... ele estava fedendo demais.");
            }
            else
            {
                Console.WriteLine("{0} obrigado por vir.", nomeDono);
                Console.WriteLine("Até outra hora!");
            }
        }

        // Desenho

        static Bitmap ResizeImage(Bitmap image, int width, int height)
        {
            // Cria um novo Bitmap com a largura e altura desejadas
            Bitmap resizedImage = new Bitmap(width, height);

            // Desenha a imagem original no novo Bitmap usando as dimensões desejadas
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.DrawImage(image, 0, 0, width, height);
            }

            return resizedImage;
        }

        static string ConvertToAscii(Bitmap image)
        {
            // Caracteres ASCII usados para representar a imagem
            char[] asciiChars = { ' ', '.', ':', '-', '=', '+', '*', '#', '%', '@' };

            StringBuilder asciiArt = new StringBuilder();

            // Percorre os pixels da imagem e converte cada um em um caractere ASCII correspondente
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    int grayScale = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    int asciiIndex = grayScale * (asciiChars.Length - 1) / 255;
                    char asciiChar = asciiChars[asciiIndex];
                    asciiArt.Append(asciiChar);
                }
                asciiArt.Append(Environment.NewLine);
            }

            return asciiArt.ToString();
        }

        static void ExibirImagem(string imagePath, int width, int height)
        {
            // Caminho para a imagem que deseja exibir
            //string imagePath = @"C:\Users\Danilo Filitto\Downloads\Panda.jpg";

            // Carrega a imagem
            Bitmap image = new Bitmap(imagePath);

            // Redimensiona a imagem para a largura e altura desejadas
            int consoleWidth = width;
            int consoleHeight = height;
            Bitmap resizedImage = ResizeImage(image, consoleWidth, consoleHeight);

            // Converte a imagem em texto ASCII
            string asciiArt = ConvertToAscii(resizedImage);

            // Exibe o texto ASCII no console
            Console.WriteLine(asciiArt);


        }
    }
}
