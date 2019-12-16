using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auk.CsharpBootstrapper.Helper;
using ResearchWebStack.BusinessLayer;
using ResearchWebStack.DataModel;

namespace ResearchWebStack.CommandLine
{
    public class CommandProcessor
    {
        public IStrategy CurrentStrategy;
        public CommandProcessor(IStrategy newStrategy)
        {
            CurrentStrategy = newStrategy;
        }

        public string[] GetCommandResult(Options options)
        {
            return CurrentStrategy.ProcessCommand(options);
        }
    }
}
