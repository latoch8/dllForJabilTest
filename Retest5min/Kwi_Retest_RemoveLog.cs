using JabilTest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retest5min
{
    class Kwi_Retest_RemoveLog : JabilTest.Test
    {
        public Kwi_Retest_RemoveLog(ScriptVariableSpace varSpace, object OtherParameter)
            : base(varSpace, null)
        {

        }

        public override TestResult Execute()
        {
            try
            {
                //initialization
                string serialNumber = (string)this.GetObjectVariable("Argument0", "String"); //Serial number of unit
                string dirToTempFail = (string)this.GetObjectVariable("Argument1", "String"); //dir for temporary fails (buffor)
                string path = dirToTempFail + "list.fail";
                dirToTempFail = Path.Combine(dirToTempFail, "temporaryLogs");
                List<string> FailList = new List<string>();

                //delate file with curent serial number
                string pathFailLog = Path.Combine(dirToTempFail, serialNumber + ".tar");
                if (File.Exists(pathFailLog))
                {
                    File.Delete(pathFailLog);
                }

                //load file if exists
                if (File.Exists(path)) 
                {
                    FailList = File.ReadAllLines(path).ToList<string>();
                    FailList.Remove(FailList.Find(x => x.Contains(serialNumber))); //remove if sn exist in file
                }

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
    }
}
