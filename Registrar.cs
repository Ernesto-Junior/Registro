using System;
using System.IO;
using System.Collections.Generic;

public class Programa
{
    private int codigo;
    private string motivo;
    private string checagem;

    public int Codigo { get { return codigo; } set { codigo = value; } }
    public string Motivo { get { return motivo; } set { motivo = value; } }
    public string Checagem { get { return checagem; } set { checagem = value; } }

    public void Registro()
    {
        try
        {
            // Codigo da maquina
            Console.Write("Digite o código: ");
            Codigo = Convert.ToInt32(Console.ReadLine());

            // Digitar o motivo de não dar certo
            Console.Write("Digite o motivo de adicionar: ");
            Motivo = Console.ReadLine();

            // Salvando os dados em um arquivo de texto
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Maquina_nao_monitorada.txt");

            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine($"Código: {Codigo}");
                sw.WriteLine($"Motivo: {Motivo}");
                sw.WriteLine("----------------------------");
            }

            Console.WriteLine("Informações salvas com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocorreu um erro: {ex.Message}");
        }
    }

    public void DeletarRegistro()
    {
        try
        {
            // Caminho do arquivo
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Maquina_nao_monitorada.txt");

            // Verificar se o arquivo existe
            if (!File.Exists(path))
            {
                Console.WriteLine("O arquivo de registros não foi encontrado.");
                return;
            }

            // Carregar todos os registros em uma lista
            List<string> linhas = new List<string>(File.ReadAllLines(path));

            Console.Write("Digite o código que deseja deletar: ");
            int codigoParaDeletar = Convert.ToInt32(Console.ReadLine());

            // Procurar o índice da linha que contém exatamente o código para deletar
            int index = -1;
            for (int i = 0; i < linhas.Count; i++)
            {
                if (linhas[i].Equals($"Código: {codigoParaDeletar}"))
                {
                    index = i;
                    break;
                }
            }

            // Se o código for encontrado, remover o bloco de linhas correspondente
            if (index != -1)
            {
                Console.WriteLine("Para confirmar a exclusão digite: Monitoramento OK");
                checagem = Console.ReadLine();

                if (checagem.Equals("Monitoramento OK", StringComparison.OrdinalIgnoreCase))
                {
                    // Remover as linhas referentes ao código encontrado
                    linhas.RemoveAt(index + 2); // Remover a linha de separação
                    linhas.RemoveAt(index + 1); // Remover linha do motivo
                    linhas.RemoveAt(index); // Remover linha do código

                    // Escrever de volta no arquivo
                    File.WriteAllLines(path, linhas);

                    Console.WriteLine("Registro deletado com sucesso!");
                }
                else
                {
                    Console.WriteLine("Checagem incorreta. O registro não será deletado.");
                }
            }
            else
            {
                Console.WriteLine("Código não encontrado.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocorreu um erro: {ex.Message}");
        }
    }

    public static void Main()
    {
        Programa programa = new Programa();

        // Escolha entre registrar ou deletar um código
        Console.WriteLine("Escolha uma opção:");
        Console.WriteLine("1 - Registrar um novo código");
        Console.WriteLine("2 - Deletar um código existente");
        int opcao = Convert.ToInt32(Console.ReadLine());

        if (opcao == 1)
        {
            programa.Registro();
        }
        else if (opcao == 2)
        {
            programa.DeletarRegistro();
        }
        else
        {
            Console.WriteLine("Opção inválida.");
        }
    }
}
