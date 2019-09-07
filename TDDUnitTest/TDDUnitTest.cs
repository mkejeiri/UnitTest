
using System;
using BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TDDUnitTest
{
    [TestClass]
    public class TDDUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void raise_exception_when_user_is_null()
        {
            var manager = new Manager();
            var user = new User();
            manager.AddUser(null);
        }
    }
}
