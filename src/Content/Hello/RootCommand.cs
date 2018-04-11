using System;
using Microsoft.Extensions.CommandLineUtils;

namespace Company.ConsoleApplication1.Hello
{
    /// Example of a root command. Typically just prints the list of the nested commands
    internal class RootCommand : IRootCommand
    {
        private readonly IHelloCommand[] _commands;

        public RootCommand(IHelloCommand[] commands)
        {
            _commands = commands;
        }

        private int Run(CommandLineApplication command)
        {
            command.ShowHelp();
            return 0;
        }

        public string Name => "hello";

        public Action<CommandLineApplication> Configure() => command =>
        {
            command.Description = "A group of commands that say hello";
            command.HelpOption("-?|-h|--help");
            command.OnExecute(() => Run(command));
            foreach(var nestedCommand in _commands)
            {
                command.Command(nestedCommand.Name, nestedCommand.Configure());
            }
        };
    }
}