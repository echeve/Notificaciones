using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsolaLector
{
    class Program
    {
        private static ILogger<Program> logger;
        private static HubConnection connection;

        static void Main(string[] args)
        {
            logger = CreateLogger<Program>();

            var services = new ServiceCollection()
                .AddLogging(build =>
                {
                    build.ClearProviders();
                    build.AddConsole();
                });

            var servicesProvider = services.BuildServiceProvider();

            connection = new HubConnectionBuilder()
                        .WithUrl("https://localhost:44301/hub/notificaciones", (opts) =>
                        {
                            opts.HttpMessageHandlerFactory = (message) =>
                            {
                                if (message is HttpClientHandler clientHandler)

                                    clientHandler.ServerCertificateCustomValidationCallback +=
                                            (sender, certificate, chain, sslPolicyErrors) => { return true; };
                                return message;
                            };
                        })
                        .WithAutomaticReconnect()
                        .Build();
            connection.On<string>("EnviarMensajeLectores", (Broadcast) => Console.WriteLine(Broadcast));

            bool showMenu = true;
            do
            {
                try
                {
                    showMenu = ShowMenu().Result;
                }
                catch (Exception ex)
                {
                    logger.LogError("Ooooops!!!");
                    logger.LogError(ex.ToString());
                    logger.LogInformation("Pulsa enter para continuar");
                    Console.ReadLine();
                }
            } while (showMenu);

        }

        static async Task<bool> ShowMenu()
        {

            Console.WriteLine("1. Conectar");
            Console.WriteLine("2. Desconectar");
            Console.WriteLine("0. Exit");
            Console.WriteLine("Escoge una opción:");

            switch (Console.ReadLine())
            {
                case "1":
                    if (connection.State != HubConnectionState.Connected)
                    {
                        await connection.StartAsync();
                        await connection.InvokeAsync("AnadirLector");
                    }
                    else
                    {
                        logger.LogInformation("Conexión ya establecida");
                    }
                    return true;
                case "2":
                    if (connection.State != HubConnectionState.Connected)
                    {
                        logger.LogInformation("No está conectado");
                    }
                    else
                    {
                        await connection.StopAsync();
                    }
                    return true;
                case "0":
                    return false;
                default:
                    logger.LogInformation("Escoge una opción:");
                    return true;
            }
        }


        static ILogger<TLogger> CreateLogger<TLogger>()
        {
            var logFactory = LoggerFactory.Create(build =>
            {
                build.ClearProviders()
                    .AddConsole();
            });

            return logFactory.CreateLogger<TLogger>();
        }

    }
}
