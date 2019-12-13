using System;
using System.Collections.Generic;
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
        private  readonly TestRun testRun = ParseData.GetDataParsed();
        public string[] GetFailedTest()
        {
            var result = UnitTestResults.GetFailedTest(testRun);
            return result;
        }

        public string[] GetNonPassingTest()
        {
            var result = UnitTestResults.GetNonPassingTests(testRun);
            return result;
        }

        public string[] GetInfo()
        {
            var result = UnitTestResults.GetInfo(testRun, "OnDeactivated_When_Call_Result");
            return result;
        }

        public string[] UnitTestResultsAll()
        {
            var result = UnitTestResults.UnitTestResultsAll(testRun.Results);
            return result;
        }
    }
}
