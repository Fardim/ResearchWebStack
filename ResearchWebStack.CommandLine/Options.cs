using System;
using System.Collections.Generic;
using CommandLine;

namespace ResearchWebStack.CommandLine
{
    public class Options
    {
        [Value(0)]
        public string ProcessName { get; set; }

        [Value(1)]
        public string ProcessPath { get; set; }


        [Value(2)]
        public string IsRunAsync { get; set; }

        [Value(3)]
        public string IsHidden { get; set; }

        [Value(4)]
        public string IsAdmin { get; set; }

        [Value(5)]
        public string Arguments { get; set; }
    }
}
