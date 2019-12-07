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

            try
            {
                var data = new string[]{};
                var result = Parser.Default.ParseArguments<Options>(args);
                var retCode = result.MapResult(async options =>
                {
                    if (options.IsRunAsync == "True")
                    {
                        data = CommandProcessor.CreateFiveCommandLineAsync(options);
                    }
                    else
                    {
                        data = CommandProcessor.CreateFiveCommandLineNonAsync(options);
                    }
                }, _ => Task.FromResult(1));
                //Console.WriteLine($"retCode={retCode}");
                LogHelper.LogModelWithStateData(null, data);
                LogHelper.QInfo("Finish");
                Console.WriteLine(data);
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            //var retCode = result.MapResult(async options => await RunAndReturnExitCodeAsync(options), _ => Task.FromResult(1));
            


           
        }

        static async Task<int> RunAndReturnExitCodeAsync(Options options)
        {
            LogHelper.QInfo(options.ProcessName, options.ProcessPath);
            await Task.Delay(20); //simulate async method
            return 0;
        }
    }
}
