using System;

namespace dio_series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while(opcaoUsuario.ToUpper() != "X"){
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;                                                                                                                        
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                opcaoUsuario = ObterOpcaoUsuario();
            }
        }
    private static void VisualizarSerie()
    {
        Console.WriteLine($"Digite o id da série: ");
        int indiceSerie = int.Parse(Console.ReadLine());
        var serie = repositorio.RetornaPorId(indiceSerie);
        Console.WriteLine(serie);
    }
    private static void ExcluirSerie()
    {
        Console.WriteLine("Digite o id da Série: ");
        int indiceSerie = int.Parse(Console.ReadLine());
        Console.WriteLine($"Você deseja realmente excluir está Série? S-Sim | N-Não: ");

        if(Console.ReadLine().ToUpper() == "S")
        {
            repositorio.Exclui(indiceSerie);
            Console.WriteLine($"A Série foi excluída com sucesso!!!");
        }
    }
    private static void AtualizarSerie()
    {
        Console.WriteLine("Digite o id da série: ");
        int indiceSerie = int.Parse(Console.ReadLine());
        Serie atualizaSerie = DadosDaSerie(indiceSerie);
        repositorio.Atualiza(indiceSerie, atualizaSerie);
    }
    private static void InserirSerie()
    {
        Console.WriteLine($"Inserir nova Série");
        int idDaSerie = repositorio.ProximoId();
        Serie novaSerie = DadosDaSerie(idDaSerie);
        repositorio.Insere(novaSerie);
    }
    private static void ListarSeries()
    {
        Console.WriteLine($"Listar séries");
        var lista = repositorio.Lista();

        if(lista.Count == 0)
        {
            Console.WriteLine($"Nenhuma série cadastrada.");
            return;
        }
        
        foreach (var serie in lista)
        {
            var excluido = serie.retornarExcluido();

            Console.WriteLine($"#ID {serie.retornaId()}: - {serie.retornaTitulo()} {(excluido?"*Excluido*": "")}");
        }
    }
    private static Serie DadosDaSerie(int indiceSerie)
    {
        foreach (int i in Enum.GetValues(typeof(Genero)))
        {
            Console.WriteLine($"{i}-{Enum.GetName(typeof(Genero), i)}");
        }

        Console.WriteLine("Digite o genêro entre as opções acima: ");
        int entradaGenero = int.Parse(Console.ReadLine());

        Console.WriteLine("Digite o Título da Serie: ");
        string entradaTitulo = Console.ReadLine();

        Console.WriteLine("Digite o Ano de Início da Série: ");
        int entradaAno = int.Parse(Console.ReadLine());

        Console.WriteLine("Digite a Descrição da Série: ");
        string entradaDescricao = Console.ReadLine();

        Serie serie = new Serie(id: indiceSerie,
                                            genero: (Genero)entradaGenero,
                                            titulo: entradaTitulo,
                                            ano: entradaAno,
                                            descricao: entradaDescricao);
        return serie;
    }
    private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine($"Dio Series a seu dispor!!!");
            Console.WriteLine($"Informe a opção desejada:");
            Console.WriteLine($"===============================================");
            Console.WriteLine($"1 - Listar séries");
            Console.WriteLine($"2 - Inserir nova série");
            Console.WriteLine($"3 - Atualizar  série");
            Console.WriteLine($"4 - Excluir série");
            Console.WriteLine($"5 - Visualizar  série");
            Console.WriteLine();
            Console.WriteLine($"-----------------------------------------------");
            Console.WriteLine();
            Console.WriteLine($"C - limpar Tela");
            Console.WriteLine($"X - Sair");
            Console.WriteLine();
            Console.WriteLine($"===============================================");
            
            string opcaoUsuario = Console.ReadLine().ToUpper();
            return opcaoUsuario;
        }
    }
}
