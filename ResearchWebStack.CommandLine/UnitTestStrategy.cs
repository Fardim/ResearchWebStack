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
    public class UnitTestStrategy: IStrategy
    {
        public string[] ProcessCommand(Options options)
        {
            var result = new string[] { };
            try
            {
                var testRun = ParseData.GetDataParsed();
                if (options.Arguments == "getFailedTest")
                {
                    result = UnitTestResults.GetFailedTest(testRun);
                }
                else if (options.Arguments == "getNonPassingTest")
                {
                    result = UnitTestResults.GetNonPassingTests(testRun);
                }
                else if (options.Arguments == "getPassingTest")
                {
                    result = UnitTestResults.GetPassingTests(testRun);
                }
                else if (options.Arguments == "getInfo")
                {
                    var type = Convert.ToInt32(options.FilterType);
                    result = UnitTestResults.GetInfo(testRun, options.SearchBy, type);
                }
                else
                {
                    result = UnitTestResults.UnitTestResultsAll(testRun);
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
