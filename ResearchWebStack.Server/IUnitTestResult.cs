using ResearchWebStack.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ResearchWebStack.Server
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUnitTestResult" in both code and config file together.
    [ServiceContract]
    public interface IUnitTestResult
    {
        [OperationContract]
        string[] GetFailedTest();

        [OperationContract]
        string[] GetNonPassingTest();

        [OperationContract]
        string[] GetPassingTest();

        [OperationContract]
        string[] GetInfo(string testName, int filterType);

        [OperationContract]
        string[] UnitTestResultsAll();

        [OperationContract]
        string CommandProcessor(string args);
    }
}
