using System;
using Microsoft.Extensions.CommandLineUtils;

namespace Company.ConsoleApplication1
{
    public interface ICommand
    {
        string Name { get; }
        Action<CommandLineApplication> Configure();
    }
}