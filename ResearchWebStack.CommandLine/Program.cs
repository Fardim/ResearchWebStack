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

            List<Tuple<string, int>> filterTuples = new List<Tuple<string, int>>();
            filterTuples.Add(new Tuple<string, int>("FireIfDeactivating_When_Call_Result(False,0,Inactive,False,0,null,Inactive,null)", 0));
            filterTuples.Add(new Tuple<string, int>("CreateParameter_When_Call_Result(False,0,null,null)", 1));
            filterTuples.Add(new Tuple<string, int>("Scope_Get_WhenCalled_ShouldReturnExpectedValue(User,User)", 2));
            filterTuples.Add(new Tuple<string, int>("CryptographyService_Set_WhenCalled_ShouldNotThrowException", 0));
            filterTuples.Add(new Tuple<string, int>("Status_Get_WhenCalled_ShouldReturnExpectedValue(Inactive,Inactive)", 1));
            try
            {
                var data = new string[]{};
                var result = Parser.Default.ParseArguments<Options>(args);
                var retCode = result.MapResult(async options =>
                {
                    if (options.IsRunAsync == "True")
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            data = CommandProcessor.CreateFiveCommandLineAsync(options, filterTuples[i]);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            data = CommandProcessor.CreateFiveCommandLineNonAsync(options, filterTuples[i]);
                        }
                    }
                }, _ => Task.FromResult(1));
                //Console.WriteLine($"retCode={retCode}");
                //LogHelper.LogModelWithStateData(null, data);
                LogHelper.QInfo("Finish");
                //Console.WriteLine(data);
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
