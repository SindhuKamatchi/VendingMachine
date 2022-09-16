using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Services.Test.Helpers
{
    public  class AssertException
    {
        public static void Throws<T>(Action action, string expectedMessage) where T : Exception
        {
            try
            {
                action.Invoke();
            }
            catch (T exc)
            {
                Assert.AreEqual(expectedMessage, exc.Message);

                return;
            }

            Assert.Fail("Exception of type {0} should be thrown.", typeof(T));
        }
    }
}
