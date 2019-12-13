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
        public CommandProcessor()
        {
           
        }
        public static string[] CreateFiveCommandLineAsync(Options options)
        {
            var result = CreateCommandLineProcess(options);
            //Parallel.For(0, 5, i => {  });
            //await Task.Delay(10);
            return result;
        }
        public static string[] CreateFiveCommandLineNonAsync(Options options)
        {
            //var allResults = results.Select(re =>
            //{
            //    LogHelper.QDebug(Newtonsoft.Json.JsonConvert.SerializeObject(re));
            //    return Newtonsoft.Json.JsonConvert.SerializeObject(re);
            //}).ToArray();
            var result = CreateCommandLineProcess(options);
            return result;
        }

        public static  string[] CreateCommandLineProcess(Options options)
        {
            var result = new string[] { };
            try
            {
                var testRun = ParseData.GetDataParsed();
                if (options.Arguments == "getFailedTest")
                {
                    result = UnitTestResults.GetFailedTest(testRun);
                    LogHelper.LogModelWithStateData(null, result);
                }
                else if (options.Arguments == "getNonPassingTest")
                {
                    result = UnitTestResults.GetNonPassingTests(testRun);
                    LogHelper.LogModelWithStateData(null, result);
                }
                else if (options.Arguments == "getInfo")
                {
                    var type = Convert.ToInt32(options.FilterType);
                    result = UnitTestResults.GetInfo(testRun, options.SearchBy, type);
                    //List<string> list = new List<string>();
                    //foreach (var list1 in result1)
                    //{
                    //    list.AddRange(list1);
                    //}
                    //result = list.ToArray();
                    LogHelper.LogModelWithStateData(null, result);
                }
                else
                {
                    result = UnitTestResults.UnitTestResultsAll(testRun.Results);
                    LogHelper.LogModelWithStateData(null, result);
                }

                return result;

            }
            catch (Exception e)
            {
                LogHelper.QFatal(e.Message, "CommandProcessor");
                return result;
            }
        }
    }
}
