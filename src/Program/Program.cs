using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

namespace NavalBattle
{
    /// <summary>
    /// Un programa que implementa un bot de Telegram.
    /// </summary>
    public class Program
    {
        // El token provisto por Telegram al crear el bot. Mira el archivo README.md en la raíz de este repo para
        // obtener indicaciones sobre cómo configurarlo.
        private static string token;

        // Esta clase es un POCO -vean https://en.wikipedia.org/wiki/Plain_old_CLR_object- para representar el token
        // secreto del bot.
        private class BotSecret
        {
            public string Token { get; set; }
        }

        // Una interfaz requerida para configurar el servicio que lee el token secreto del bot.
        private interface ISecretService
        {
            string Token { get; }
        }

        // Una clase que provee el servicio de leer el token secreto del bot.
        private class SecretService : ISecretService
        {
            private readonly BotSecret _secrets;

            public SecretService(IOptions<BotSecret> secrets)
            {
                _secrets = secrets.Value ?? throw new ArgumentNullException(nameof(secrets));
            }

            public string Token { get { return _secrets.Token; } }
        }

        // Configura la aplicación.
        private static void Start()
        {
            // Lee una variable de entorno NETCORE_ENVIRONMENT que si no existe o tiene el valor 'development' indica
            // que estamos en un ambiente de desarrollo.
            var developmentEnvironment = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
            var isDevelopment =
                string.IsNullOrEmpty(developmentEnvironment) ||
                developmentEnvironment.ToLower() == "development";

            var builder = new ConfigurationBuilder();
            builder
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            // En el ambiente de desarrollo el token secreto del bot se toma de la configuración secreta
            if (isDevelopment)
            {
                builder.AddUserSecrets<Program>();
            }

            var configuration = builder.Build();

            IServiceCollection services = new ServiceCollection();

            // Mapeamos la implementación de las clases para  inyección de dependencias
            services
                .Configure<BotSecret>(configuration.GetSection(nameof(BotSecret)))
                .AddSingleton<ISecretService, SecretService>();

            var serviceProvider = services.BuildServiceProvider();
            var revealer = serviceProvider.GetService<ISecretService>();
            token = revealer.Token;
        }

        /// <summary>
        /// Punto de entrada al programa.
        /// </summary>
        public static void Main()
        {
            Start();

            Bot.Handlers();

            Console.WriteLine($"Bot is up!");
        }
    }
}