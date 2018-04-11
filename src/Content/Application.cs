using System;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace Company.ConsoleApplication1
{
    public class Application
    {
        private readonly IRootCommand[] _rootCommands;
        private readonly ILogger _logger;

        public Application(IRootCommand[] rootCommands, ILogger<Application> logger)
        {
            _rootCommands = rootCommands;
            _logger = logger;
        }

        public int Run(string[] args)
        {
            try
            {
                var app = new CommandLineApplication
                {
                    Name = "Company.ConsoleApplication1",
                    FullName = "APPLICATION-FULL-NAME",
                    Description = "APPLICATION-DESCRIPTION",
                };

                app.HelpOption("-?|-h|--help");

                RegisterCommands(app);

                return app.Execute(args);
            }
            catch (Exception e)
            {
                _logger.LogError(new EventId(), e, "Unexpected exception");
                return 1;
            }
        }
        private void RegisterCommands(CommandLineApplication app)
        {
            foreach (var command in _rootCommands)
            {
                app.Command(command.Name, command.Configure());
            }
            app.OnExecute(() => { app.ShowHelp(); return 0; });
        }
    }
}