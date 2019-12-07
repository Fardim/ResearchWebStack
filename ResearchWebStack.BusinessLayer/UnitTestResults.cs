using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Auk.CsharpBootstrapper.Helper;
using Newtonsoft.Json;
using ResearchWebStack.DataModel;

namespace ResearchWebStack.BusinessLayer
{
    public class UnitTestResults
    {
        public UnitTestResults()
        {
            
        }

        public static string[] UnitTestResultsAll(IList<TestRunResultsUnitTestResult> results)
        {
            var allResults = results.Select(re =>
            {
                //LogHelper.QDebug(Newtonsoft.Json.JsonConvert.SerializeObject(re));
                return Newtonsoft.Json.JsonConvert.SerializeObject(re);
            }).ToArray();
            return allResults;
        }

        public static string[] GetFailedTest(TestRun testRun)
        {
            var failedTests = testRun.Results.Where(r => r.outcome.Equals("NotExecuted")).Select(r =>
            {
                var obj = new
                {
                    TestName = r.testName,
                    ClassName = testRun.TestDefinitions.FirstOrDefault(d => d.id == r.testId)?.TestMethod.FirstOrDefault()?.className,
                    FullyQuantifiedName = new StringBuilder().Append(r.testId).Append(" - ").Append(r.testName).Append(" - ").Append(r.computerName),
                    Status = r.outcome
                };
                //LogHelper.QWarn(Newtonsoft.Json.JsonConvert.SerializeObject(obj));
                return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }).ToArray();
            return failedTests;
        }

        public static string[] GetNonPassingTests(TestRun testRun)
        {
            var nonPassingTests = testRun.Results.Where(r => !r.outcome.Equals("Passed")).Select(r =>
            {
                var obj = new
                {
                    TestName = r.testName,
                    ClassName = testRun.TestDefinitions.FirstOrDefault(d => d.id == r.testId)?.TestMethod.FirstOrDefault()?.className,
                    FullyQuantifiedName = new StringBuilder().Append(r.testId).Append(" - ").Append(r.testName).Append(" - ").Append(r.computerName),
                    Status = r.outcome
                };
                //LogHelper.QDebug(Newtonsoft.Json.JsonConvert.SerializeObject(obj));
                return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }).ToArray();
            return nonPassingTests;
        }

        public static List<List<string>> GetInfo(TestRun testRun, string testName= "OldValue_Set_WhenCalled_ShouldNotThrowException", int filterType = 0)
        {
            var infos = new List<List<string>>();
            try
            {
                Func<string, bool> filterExp = FilterExpression(testName, filterType);
                infos = testRun.TestDefinitions.Select(t => t.TestMethod.Where(m => filterExp(m.name)).Select(d =>
                {
                    //LogHelper.QDebug(JsonConvert.SerializeObject(d));
                    return JsonConvert.SerializeObject(d);
                }).ToList()).Where(d=> d.Any()).ToList();
                return infos;
            }
            catch (Exception e)
            {
                LogHelper.QError(e.Message);
                return infos;
            }
        }

        private static Func<string, bool> FilterExpression(string testName, int filterType)
        {
            if (filterType == (int)FilterTypeEnum.FilterType.Contains)
            {
                return x => x.Contains(testName);
            }
            else if (filterType == (int)FilterTypeEnum.FilterType.StartsWith)
            {
                return x => x.StartsWith(testName);
            }
            else if(filterType == (int)FilterTypeEnum.FilterType.EndsWith)
            {
                return x => x.EndsWith(testName);
            }
            else
            {
                return x => false;
            }
        }
    }
}
