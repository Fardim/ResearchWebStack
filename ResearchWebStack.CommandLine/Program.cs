using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auk.CsharpBootstrapper.Helper;
using CommandLine;
using log4net;

namespace ResearchWebStack.CommandLine
{
    public class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));
        static void Main(string[] args)
        {
            LogHelper.EnableDebugMode();
            var configFilePath = PathHelper.GetPathCombineFromBase("Auk.CsharpBootstrapper.Example.log4net.config");
            LogHelper.GetConfiguredLog4Net(configFilePath);
            LogHelper.InjectLog4NetLogger(Log);

            var data = new string[] { };
            var result = Parser.Default.ParseArguments<Options>(args);
            if (result != null)
            {
                result.MapResult(async options =>
                {
                    CommandProcessor cmp = new CommandProcessor(null);
                    switch (options.ProcessName)
                    {
                        case "runNodeJs":
                            cmp.CurrentStrategy = new NodeStrategy();
                            break;
                        case "runPython":
                            cmp.CurrentStrategy = new PythonStrategy();
                            break;
                        case "cmd":
                            cmp.CurrentStrategy = new CMDStrategy();
                            break;
                        case "ps":
                            cmp.CurrentStrategy = new PSStrategy();
                            break;
                        case "UnitTests":
                            cmp.CurrentStrategy = new UnitTestStrategy();
                            break;
                        case "registry":
                            cmp.CurrentStrategy = new RegistryStrategy();
                            break;
                    }
                    data = cmp.GetCommandResult(options);
                    Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(data));
                }, _ => Task.FromResult(1));

            }

        }
        
    }
}
