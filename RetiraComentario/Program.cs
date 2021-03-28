using System;
using System.IO;
using System.Collections.Generic;

namespace RetiraComentario
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Entre com um parametro, caminho até o script");
                string arquivo = Console.ReadLine();
                lerarquivo(arquivo);
            }
            else
            {

                string arquivo = args[0];

                lerarquivo(arquivo);
            }
        }

        public static void lerarquivo(string arquivo)
        {
            string conteudo = File.ReadAllText(arquivo);
            conteudo = retirarcomentario(conteudo);

            File.WriteAllText(arquivo + "new", conteudo);            
        }

        public static string retirarcomentario(string conteudo)
        {

            int cont = 2;

            for (int i = 3; i >= cont; i++)
            {
                if (conteudo.Contains("/*"))
                {
                    int ftag = conteudo.IndexOf("/*");
                    int ltag = conteudo.IndexOf("*/");

                    string retiratag = conteudo.Substring(ftag, ltag - ftag + 2);
                    int cnt = retiratag.Split("/*").Length - 1;

                    if (cnt == 1)
                    {
                        conteudo = conteudo.Replace(retiratag, "");
                    }
                    else
                    {

                        int tagf = retiratag.LastIndexOf("/*");
                        int tagl = retiratag.IndexOf("*/");

                        string rettag = retiratag.Substring(tagf, tagl - tagf + 2);
                        conteudo = conteudo.Replace(rettag, "");

                    }
                }
                else
                {
                    i = 0;
                }
            }

            return conteudo;
        }

    }
}
