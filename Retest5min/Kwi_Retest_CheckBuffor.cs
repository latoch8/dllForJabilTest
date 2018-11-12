using JabilTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retest5min
{
    class Kwi_Retest_CheckBuffor : JabilTest.Test
    {
        public Kwi_Retest_CheckBuffor(ScriptVariableSpace varSpace, object OtherParameter)
            : base(varSpace, null)
        {

        }

        public override TestResult Execute()
        {
            try
            {
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
