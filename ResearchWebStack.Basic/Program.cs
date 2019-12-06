using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Auk.CsharpBootstrapper.Extensions;
using Auk.CsharpBootstrapper.Helper;
using Auk.CsharpBootstrapper.Implementations.Converter;
using log4net;
using NUnit.Framework.Internal;
using ResearchWebStack.BusinessLayer;
using ResearchWebStack.DataModel;

namespace ResearchWebStack.Basic
{
    class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));
        
        static void Main(string[] args)
        {
            LogHelper.EnableDebugMode();
            var configFilePath = PathHelper.GetPathCombineFromBase("Auk.CsharpBootstrapper.Example.log4net.config");
            LogHelper.GetConfiguredLog4Net(configFilePath);
            LogHelper.InjectLog4NetLogger(Log);

            Serializer ser = new Serializer();
            string path = string.Empty;
            string xmlInputData = string.Empty;
            string xmlOutputData = string.Empty;
            
            // EXAMPLE 1
            path = Directory.GetCurrentDirectory() + @"\SampleTrxXMLTestLogFile.trx";
            xmlInputData = File.ReadAllText(path);

            TestRun testRun = ser.Deserialize<TestRun>(xmlInputData);
            LogHelper.QInfo("Successfully parsed data from xml file.");


            //UnitTestResults.UnitTestResultsAll(testRun.Results);
            UnitTestResults.GetFailedTest(testRun);
            //UnitTestResults.GetNonPassingTests(testRun);
            //UnitTestResults.GetInfo(testRun, "(null,00000000-0000-0000-0000-000000000000,null)", (int) FilterTypeEnum.FilterType.Contains);

            Console.ReadKey();
        }
    }
}
