using System;
using System.IO;
using System.Text.RegularExpressions;

namespace RetiraComentario
{
    class Program
    {
        static void Main(string[] args)
        {
            string arquivo;

            if (args.Length <= 0)
            {
                Console.WriteLine("Entre com o caminho do arquivo para remover os comentário:");
                arquivo = Console.ReadLine();
            }
            else
            {
                arquivo = args[0];
            }

            CarregaArquivo(arquivo);
        }

        public static void CarregaArquivo(string arquivo)
        {
            string conteudo = File.ReadAllText(arquivo);
            conteudo = RemoveComentarios(conteudo);

            FileInfo arquivoAtual = new FileInfo(arquivo);
            string novoArquivo = arquivoAtual.DirectoryName + @"\" + Path.GetFileNameWithoutExtension(arquivoAtual.Name) + "_new" + arquivoAtual.Extension;

            File.Copy(arquivoAtual.FullName, novoArquivo);

            File.WriteAllText(novoArquivo, conteudo);
        }

        public static string RemoveComentarios(string conteudo)
        {

            while (conteudo.Contains("/*") && conteudo.Contains("*/"))
            {
                int inicio = conteudo.IndexOf("/*");
                int fim = conteudo.IndexOf("*/") + 2;

                conteudo = conteudo.Remove(inicio, fim - inicio);
            }

            return conteudo;
        }

        public static string RemoveComentariosRegex(string conteudo)
        {
            var matches = Regex.Matches(conteudo, @"\/\*(\*(?!\/)|[^*])*\*\/");
            while (matches.Count > 0)
            {
                int inicio = matches[0].Index;
                int fim = matches[0].Length;

                conteudo = conteudo.Remove(inicio, fim);

                matches = Regex.Matches(conteudo, @"\/\*(\*(?!\/)|[^*])*\*\/");
            }

            return conteudo;
        }

    }
}
