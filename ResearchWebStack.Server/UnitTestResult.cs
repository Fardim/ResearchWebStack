using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ResearchWebStack.BusinessLayer;
using ResearchWebStack.DataModel;

namespace ResearchWebStack.Server
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UnitTestResult" in both code and config file together.
    public class UnitTestResult : IUnitTestResult
    {
        private  readonly TestRun _testRun = ParseData.GetDataParsed();
        public string[] GetFailedTest()
        {
            var result = UnitTestResults.GetFailedTest(_testRun);
            return result;
        }

        public string[] GetNonPassingTest()
        {
            var result = UnitTestResults.GetNonPassingTests(_testRun);
            return result;
        }

        public string[] GetPassingTest()
        {
            var result = UnitTestResults.GetPassingTests(_testRun);
            return result;
        }

        public string[] GetInfo(string testName = "OldValue_Set_WhenCalled_ShouldNotThrowException", int filterType = 0)
        {
            var result = UnitTestResults.GetInfo(_testRun, testName, filterType);
            return result;
        }

        public string[] UnitTestResultsAll()
        {
            var result = UnitTestResults.UnitTestResultsAll(_testRun);
            return result;
        }

        public string CommandProcessor(string args)
        {
            var result = UnitTestResults.CommandProc(args);
            return result;
        }
    }
}
