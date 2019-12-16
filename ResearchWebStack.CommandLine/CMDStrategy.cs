using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchWebStack.CommandLine
{
    public class CMDStrategy: IStrategy
    {
        public string[] ProcessCommand(Options options)
        {
            return new string[] { options.ProcessName, options.ProcessPath, options.Arguments, options.SearchBy, options.FilterType };
        }
    }
}
