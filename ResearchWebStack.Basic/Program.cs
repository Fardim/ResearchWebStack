﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using CommandLine;
using log4net;
using NUnit.Framework.Internal;
using ResearchWebStack.BusinessLayer;
using ResearchWebStack.DataModel;

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
            
            var testRun = ParseData.GetDataParsed();

            #region EndPoints

            //var result = UnitTestResults.UnitTestResultsAll(testRun.Results);
            //var result = UnitTestResults.GetFailedTest(testRun);
            //var result = UnitTestResults.GetNonPassingTests(testRun);
            var result = UnitTestResults.GetInfo(testRun, "(null,00000000-0000-0000-0000-000000000000,null)", (int)FilterTypeEnum.FilterType.Contains);
            LogHelper.LogModelWithStateData(null, result);

            #endregion


            #region CommandLine commands

            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine(@"ResearchWebStack.CommandLine.exe fardim here true false true getInfo OldValue_Set_WhenCalled_ShouldNotThrowException 0");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
            
            #endregion


            Console.ReadKey();
        }
    }
}
