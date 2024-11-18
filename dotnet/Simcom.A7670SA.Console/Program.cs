using System.Text.RegularExpressions;
using Simcom.A7670SA;

var opcao = 0;
var strOpcao = "\0";
var simcomA7670SA = new SimcomA7670SA("COM7", 115200);

do
{
    Console.Clear();
    System.Console.WriteLine("1 - LISTAR SMS");
    System.Console.WriteLine("2 - LER SMS");
    System.Console.WriteLine("3 - ENVIAR SMS");
    System.Console.WriteLine("4 - ENVIAR LOTE DE SMS");
    System.Console.WriteLine("5 - MONITORAR SMS NÃO LIDOS");
    System.Console.WriteLine();
    System.Console.WriteLine("9 - Sair");

    System.Console.WriteLine("Informe uma opção: ");
    strOpcao = Console.ReadKey().KeyChar.ToString();
    int.TryParse(strOpcao, out opcao);

    switch (opcao)
    {
        case 1:
            ListarSMS(simcomA7670SA);
            break;
        case 2:
            LerSMS(simcomA7670SA);
            break;
        case 3:
            EnviarSMS(simcomA7670SA);
            break;
        case 4:
            EnviarLoteSMS(simcomA7670SA);
            break;
        case 5:
            MonitorarSMSNaoLidos(simcomA7670SA);
            break;
    }
} while (opcao != 9);


static void ListarSMS(SimcomA7670SA simcomA7670SA)
{
    string strOpcao = "\0";
    var opcao = 0;

    do
    {
        Console.Clear();
        System.Console.WriteLine("Informe o tipo de listagem: ");
        System.Console.WriteLine("1 - Todas as Mensagens");
        System.Console.WriteLine("2 - Recebidas não lidas");
        System.Console.WriteLine("3 - Recebidas já lidas");
        System.Console.WriteLine("4 - Armazenadas, mas não enviadas");
        System.Console.WriteLine("5 - Armazenadas e enviadas");
        System.Console.WriteLine();
        System.Console.WriteLine("9 - Retornar");

        strOpcao = Console.ReadKey().KeyChar.ToString();
        int.TryParse(strOpcao, out opcao);

        if (opcao >= 1 && opcao <= 5)
        {
            var listSmsType = (SimcomA7670SASmsReadEnum)opcao;
            var result = simcomA7670SA.ListSMS(listSmsType);

            System.Console.WriteLine("\n");
            System.Console.WriteLine("[MENSAGENS]");
            System.Console.WriteLine(result);

            System.Console.WriteLine("Pressione qualquer tecla para continuar ...");
            Console.ReadKey(true);
            return;
        }

    } while (opcao != 9);
}

static void LerSMS(SimcomA7670SA simcomA7670SA)
{
    var index = 0;
    var strIndex = "\0";
    var continuar = String.Empty;

    do
    {
        Console.Clear();
        System.Console.WriteLine("Informe o índice da mensagem: ");
        strIndex = Console.ReadLine();
        int.TryParse(strIndex, out index);

        var result = simcomA7670SA.ReadSMS(index);
        System.Console.WriteLine("\n");
        System.Console.WriteLine("[RESULTADO]");
        System.Console.WriteLine(result);

        System.Console.WriteLine("Deseja ler uma nova mensagem? [S/N]");
        continuar = Console.ReadKey().KeyChar.ToString();
    } while (continuar == "S");
}

static void EnviarSMS(SimcomA7670SA simcomA7670SA)
{

    var continuar = String.Empty;
    do
    {
        Console.Clear();
        System.Console.WriteLine("Informe o número de telefone com DDD:");

        var telefone = Console.ReadLine() ?? String.Empty;
        var telefoneFormatado = Regex.Replace(telefone, @"\D", String.Empty);

        System.Console.WriteLine("Informe a mensagem: ");
        var mensagem = Console.ReadLine() ?? String.Empty;

        var result = simcomA7670SA.SendSMS(telefoneFormatado, mensagem);

        System.Console.WriteLine("\n");
        System.Console.WriteLine("[RESULTADO]");
        System.Console.WriteLine(result);

        System.Console.WriteLine("\n");
        System.Console.WriteLine("Deseja enviar uma nova mensagem? [S/N]");
        continuar = Console.ReadKey().KeyChar.ToString();
    } while (continuar == "S");
}

static void EnviarLoteSMS(SimcomA7670SA simcomA7670SA) {
    Console.Clear();
    System.Console.WriteLine("Informe o número de telefone com DDD:");

    int count = 0;
    var continuar = true;
    var telefone = Console.ReadLine() ?? String.Empty;
    var telefoneFormatado = Regex.Replace(telefone, @"\D", String.Empty);

    System.Console.WriteLine("\n[Enviando mensagens a cada 5 segundos]");
    System.Console.WriteLine("Pressione qualquer tecla para interromper ...");
    
    do
    {
        count++;
        var mensagem = $"ENVIANDO SMS {count} LINGUAGEM CSHARP. TESTE DO HORÁRIO {DateTime.Now}";
        System.Console.WriteLine($"\n{mensagem}");
        simcomA7670SA.SendSMS(telefoneFormatado, mensagem);

        Thread.Sleep(5000);

        if (Console.KeyAvailable) {
            Console.ReadKey(true);
            System.Console.WriteLine("\nProcesso interrompido.");
            System.Console.WriteLine("Pressione qualquer tecla para continuar ...");
            Console.ReadKey(true);
            continuar = false;
        }
    } while (continuar);
}

static void MonitorarSMSNaoLidos(SimcomA7670SA simcomA7670SA) {
    Console.Clear();

    var continuar = true;
    var listSmsType = SimcomA7670SASmsReadEnum.RecUnread;
    do
    {
        Console.Clear();
        System.Console.WriteLine("\n[Monitorando mensagens não lidas a cada 5 segundos]");
        System.Console.WriteLine("Pressione qualquer tecla para interromper ...");

        System.Console.WriteLine("\n[MENSAGENS RECEBIDAS]");
        
        var result = simcomA7670SA.ListSMS(listSmsType);
        System.Console.WriteLine(result);

        Thread.Sleep(5000);

        if (Console.KeyAvailable) {
            Console.ReadKey(true);
            System.Console.WriteLine("\nProcesso interrompido.");
            System.Console.WriteLine("Pressione qualquer tecla para continuar ...");
            Console.ReadKey(true);
            continuar = false;
        }
    } while (continuar);
}