using JabilTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retest5min
{
    class template : JabilTest.Test
    {
        public template(ScriptVariableSpace varSpace, object OtherParameter)
            : base(varSpace, null)
        {

        }

        public override TestResult Execute()
        {
            try
            {
                string serialNumber = (string)this.GetObjectVariable("Argument0", "String");
                this.testResult.Status = TestStatus.Pass;
            }
            catch (Exception)
            {
                VariableSpace.setVariable(new ScriptVariable("ReturnValue0", VariableType.Object, null));
                this.testResult.Status = TestStatus.Fail;
            }
            return testResult;
        }
    }
}
