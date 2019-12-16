using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auk.CsharpBootstrapper.Helper;
using ResearchWebStack.DataModel;

namespace ResearchWebStack.DataModel
{
    public class ParseData
    {
        public static TestRun GetDataParsed()
        {
            Serializer ser = new Serializer();
            string path = string.Empty;
            string xmlInputData = string.Empty;
            path = @"SampleTrxXMLTestLogFile.trx";
            xmlInputData = File.ReadAllText(path);
            TestRun testRun = ser.Deserialize<TestRun>(xmlInputData);
            return testRun;
        }
    }
}
