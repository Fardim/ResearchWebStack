using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public static string[] UnitTestResultsAll(TestRun testRun)
        {
            var allResults = testRun.Results.Select(r =>
            {
                var obj = new
                {
                    TestName = r.testName,
                    ClassName = testRun.TestDefinitions.FirstOrDefault(d => d.id == r.testId)?.TestMethod.FirstOrDefault()?.className,
                    FullyQuantifiedName = r.testId + " - " + r.testName + " - " + r.computerName,
                    Status = r.outcome
                };
                //LogHelper.QDebug(Newtonsoft.Json.JsonConvert.SerializeObject(re));
                return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
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
                    FullyQuantifiedName = r.testId + " - " + r.testName + " - " + r.computerName,
                    Status = r.outcome
                };
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
                    FullyQuantifiedName = r.testId+" - "+r.testName+" - "+r.computerName,
                    Status = r.outcome
                };
                return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }).ToArray();
            return nonPassingTests;
        }

        public static string[] GetPassingTests(TestRun testRun)
        {
            var passingTests = testRun.Results.Where(r => r.outcome.Equals("Passed")).Select(r =>
            {
                var obj = new
                {
                    TestName = r.testName,
                    ClassName = testRun.TestDefinitions.FirstOrDefault(d => d.id == r.testId)?.TestMethod.FirstOrDefault()?.className,
                    FullyQuantifiedName = r.testId+" - "+r.testName+" - "+r.computerName,
                    Status = r.outcome
                };
                return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }).ToArray();
            return passingTests;
        }

        public static string[] GetInfo(TestRun testRun, string testName= "OldValue_Set_WhenCalled_ShouldNotThrowException", int filterType = 0)
        {
            try
            {
                if (!String.IsNullOrEmpty(testName))
                {
                    Func<string, bool> filterExp = FilterExpression(testName, filterType);
                    var infos = testRun.Results.Where(r => filterExp(r.testName)).Select(r =>
                    {
                        var obj = new
                        {
                            TestName = r.testName,
                            ClassName = testRun.TestDefinitions.FirstOrDefault(d => d.id == r.testId)?.TestMethod.FirstOrDefault()?.className,
                            FullyQuantifiedName = r.testId + " - " + r.testName + " - " + r.computerName,
                            Status = r.outcome
                        };
                        return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                    }).ToArray();
                    return infos;
                }
                else
                {
                    return UnitTestResultsAll(testRun);
                }
            }
            catch (Exception e)
            {
                LogHelper.QError(e.Message);
                return new string[] { };
            }
        }

        public static string CommandProc(string args)
        {
            const string fileName = "ResearchWebStack.CommandLine.exe";
            try
            {
                Process cmd = new Process();
                cmd.StartInfo.FileName = fileName;
                cmd.StartInfo.Arguments = args;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();

                var item = cmd.StandardOutput.ReadToEnd();
                cmd.WaitForExit();
                return item;
            }
            catch (Exception e)
            {
                LogHelper.QError(e.Message);
                return "";
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
