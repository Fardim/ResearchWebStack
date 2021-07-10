using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Auk.CsharpBootstrapper.Extensions;
using Auk.CsharpBootstrapper.Helper;
using Auk.CsharpBootstrapper.Implementations.Converter;
using CommandLine;
using log4net;
using NUnit.Framework.Internal;
using ResearchWebStack.BusinessLayer;
using ResearchWebStack.DataModel;
using ResearchWebStack.Server;

namespace ResearchWebStack.Basic
{
    public class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));
        
        public static void Main(string[] args)
        {
            LogHelper.EnableDebugMode();
            var configFilePath = PathHelper.GetPathCombineFromBase("Auk.CsharpBootstrapper.Example.log4net.config");
            LogHelper.GetConfiguredLog4Net(configFilePath);
            LogHelper.InjectLog4NetLogger(Log);

            using (ServiceHost host = new ServiceHost(typeof(ResearchWebStack.Server.UnitTestResult)))
            {
                try
                {
                    host.Open();
                    LogHelper.QInfo("Host Started @ " + DateTime.Now);
                    Console.ReadLine();
                }
                catch (Exception e)
                {
                    LogHelper.QFatal(e.Message);
                }
            }
            
            #region EndPoints

            //var result = UnitTestResults.UnitTestResultsAll(testRun.Results);
            //var result = UnitTestResults.GetFailedTest(testRun);
            //var result = UnitTestResults.GetNonPassingTests(testRun);
            //var result = UnitTestResults.GetInfo(testRun, "(null,00000000-0000-0000-0000-000000000000,null)", (int)FilterTypeEnum.FilterType.Contains);
            //var result = UnitTestResults.CommandProc("fardim here true false true getInfo OldValue_Set_WhenCalled_ShouldNotThrowException 0");
            //LogHelper.LogModelWithStateData(null, result);
            //Console.ReadLine();

            #endregion


            #region CommandLine commands
            
            //Process cmd = new Process();
            //cmd.StartInfo.FileName = "cmd.exe";
            //cmd.StartInfo.RedirectStandardInput = true;
            //cmd.StartInfo.RedirectStandardOutput = true;
            //cmd.StartInfo.CreateNoWindow = true;
            //cmd.StartInfo.UseShellExecute = false;
            //cmd.Start();

            //cmd.StandardInput.WriteLine(@"ResearchWebStack.CommandLine.exe fardim here true false true getInfo OldValue_Set_WhenCalled_ShouldNotThrowException 0");
            //cmd.StandardInput.Flush();
            //cmd.StandardInput.Close();
            //cmd.WaitForExit();
            //var item = cmd.StandardOutput.ReadToEnd();
            //Console.WriteLine(item);


            #endregion


            //Console.ReadKey();
        }
    }
}
