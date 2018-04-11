using System;
using System.Threading.Tasks;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Company.ConsoleApplication1.Hello
{
    /// Example of an action command. Run method can be asynchronous.
    internal class HelloWorldCommand : IHelloCommand
    {
        private readonly HelloWorldOptions _options;
        private readonly ILogger _logger;
        private CommandOption _delayOption;

        public HelloWorldCommand(
            IOptions<HelloWorldOptions> options,
            ILogger<HelloWorldCommand> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        private async Task<int> RunAsync()
        {
            if (!int.TryParse(_delayOption.Value() ?? "", out var delay))
            {
                delay = _options.DefaultDelayInSec;
                _logger.LogWarning("Invalid or missing delay option. Using default delay value {Delay}", delay);
            }
            
            await Task.Delay(TimeSpan.FromSeconds(delay));
            
            Console.WriteLine("Hello, world!");
            
            return 0;
        }

        public string Name => "world";
        public Action<CommandLineApplication> Configure() => command =>
        {
            command.Description = "Says hello to the world";
            command.HelpOption("-?|-h|--help");
            _delayOption = command.Option("-d|--delay", "a delay before greeting (in seconds)", CommandOptionType.SingleValue);
            command.OnExecute(() => RunAsync());
        };
    }
}