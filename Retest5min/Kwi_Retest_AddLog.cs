using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JabilTest;

namespace Retest5min
{
    class Kwi_Retest_AddLog : JabilTest.Test
    {
        public Kwi_Retest_AddLog(ScriptVariableSpace varSpace, object OtherParameter)
            : base(varSpace, null)
        {

        }

        public override TestResult Execute()
        {
            try
            {
                //initialization
                char separator = ';';
                string serialNumber = (string)this.GetObjectVariable("Argument0", "String"); //Serial number of unit
                string dirToTempFail = (string)this.GetObjectVariable("Argument1", "String"); //dir for temporary fails (buffor)
                string logsDir = (string)this.GetObjectVariable("Argument2", "String"); //folder with logs
                string path = dirToTempFail + "list.fail";
                dirToTempFail = Path.Combine(dirToTempFail, "temporaryLogs");
                List<string> FailList = new List<string>();

                //move log file to buffor
                string[] PathsLogs = Directory.GetFiles(logsDir);
                foreach (var PathLog in PathsLogs) 
                {
                    string logName = Path.GetFileName(PathLog); //take only name of file 
                    if (logName.Contains(serialNumber))
                    {
                        if (!Directory.Exists(dirToTempFail)) Directory.CreateDirectory(dirToTempFail);//creat folder if does not exist
                        if (!MoveFile(PathLog, Path.Combine(dirToTempFail, serialNumber + ".tar")))
                        {
                            this.testResult.Status = TestStatus.Fail;
                            return testResult;
                        }
                    }
                }
                //load list file if exists
                if (File.Exists(path)) 
                {
                    FailList = File.ReadAllLines(path).ToList<string>();
                    FailList.Remove(FailList.Find(x => x.Contains(serialNumber))); //remove if sn exist in file
                }
                //prepare new data
                StringBuilder stringBuilder = new StringBuilder(DateTime.Now.ToString());
                stringBuilder.Append(separator);
                stringBuilder.Append(serialNumber);
                stringBuilder.Append(separator);
                FailList.Add(stringBuilder.ToString());
                //save file
                File.WriteAllLines(path, FailList);
                this.testResult.Status = TestStatus.Pass;
            }
            catch (Exception)
            {
                this.testResult.Status = TestStatus.Fail;
            }
            return testResult;
        }

        private bool MoveFile(string sourceFileName, string destFileName)
        {
            try
            {
                File.Copy(sourceFileName, destFileName, true);
                File.Delete(sourceFileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
