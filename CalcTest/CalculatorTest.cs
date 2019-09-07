using Calculation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static Calculation.Calculator;

namespace CalcTest
{
    [TestClass]
    //this would skip could coverage diagnostics not test
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class CalculatorTest
    {
        private static Calculator _Calc;

        //Intitialze the class before running the test
        [ClassInitialize]
        public static void CreateCalc(TestContext context)
        {
            _Calc= new Calculator();
        }

        //run this before the test
        [TestInitialize]
        public void ClearCalc()
        {
            _Calc.ProcessClear();
        }

        
        [TestMethod]
        [TestCategory("Addition")]
        [TestProperty("Dependency","No")]
        //[Ignore] //this skipped 
        public void calculate_when_summing_two_numbers()
        {
            //Arrange
            _Calc.ProcessDigit(1);
            _Calc.ProcessDigit(2);
            _Calc.ProcessDecimalPoint();
            _Calc.ProcessOp(OpType.Addition);
            var expectedValue = 3;

            //Act
            _Calc.ProcessEquals();
            //Assert
            Assert.Equals(expectedValue, _Calc.CurrentValue);
        }

        [TestMethod]
        [Priority(10)]
        [TestCategory("Substraction")]
        [TestProperty("Dependency", "No")]
        public void calculate_when_substracting_two_numbers()
        {
            //Arrange
            _Calc.ProcessDigit(1);
            _Calc.ProcessDigit(2);
            _Calc.ProcessOp(OpType.Addition);
            var expectedValue = -1;
            //Act
            _Calc.ProcessEquals();
            //Assert
            Assert.Equals(expectedValue, _Calc.CurrentValue);
        }

        [TestMethod]
        [Priority(5)]
        [Owner("Kejeiri")]
        [TestCategory("Multiplication")]
        [TestProperty("Dependency", "No")]
        public void calculate_when_multiplying_two_numbers()
        {
            //Arrange
            _Calc.ProcessDigit(3);
            _Calc.ProcessDigit(2);
            _Calc.ProcessOp(OpType.Mutiplication);
            var expectedValue = 6;
            //Act
            _Calc.ProcessEquals();
            //Assert
            Assert.Equals(expectedValue, _Calc.CurrentValue);
        }


        [TestMethod]
        [TestCategory("Multiplication")]
        [TestProperty("Dependency", "No")]
        public void calculate_when_multiplying()
        {
           Assert.Inconclusive();
        }
        
        [TestMethod]
        [TestCategory("Multiplication")]
        [TestProperty("Dependency", "No")]
        public void calculate_when_using_expression()
        {
            //Arrange
            var expectedValue = 6;
            //Act
            _Calc.ProcessExpression("2*3");
            //Assert
            Assert.Equals(expectedValue, _Calc.CurrentValue);
        }

        [TestMethod]
        [TestCategory("Multiplication")]
        [TestProperty("Dependency", "Yes")]
        public void calculate_when_using_expression_x_instead_of_star()
        {
            //Arrange
            var expectedValue = 6;
            //Act
            _Calc.ProcessExpression("2x3");
            //Assert
            Assert.Equals(expectedValue, _Calc.CurrentValue);
        }

        [TestMethod]
        [TestCategory("Multiplication")]
        [TestProperty("Dependency", "No")]
        public void calculate_when_using_expression_x_instead_of_star_exception_classic()
        {
            //Arrange
            //Act
            try
            {
                _Calc.ProcessExpression("2x3");
                Assert.Fail("Should have thrown an exception");
            }
            catch (ArgumentException) { }
        }

        [TestMethod]
        [TestCategory("Multiplication")]
        [TestProperty("Dependency", "No")]
        [ExpectedException(typeof(ArgumentException))]
        public void calculate_when_using_expression_x_instead_of_star_exception()
        {
            _Calc.ProcessExpression("2x3");
        }

        [TestMethod]
        [TestCategory("Multiplication")]
        [DeploymentItem("expression.txt")]
        [TestProperty("Dependency", "Yes")]
        public void calculate_when_using_expression_from_file()
        {
            var expectedValue = 163;
            using (var FS = System.IO.File.OpenText(" "))
            {
                var expression = FS.ReadToEnd();
                _Calc.ProcessExpression(expression);
            }
            Assert.Equals(expectedValue, _Calc.CurrentValue);
        }

        [DataSource(@"System.Data.Odbc",
            @"Dsn=ExcelFiles;Driver={Microsoft Excel Driver (*.xlsx)};dbq=|DataDirectory|\data.xlsx;defaultdir=.",
            "Sheet1$", Microsoft.VisualStudio.TestTools.UnitTesting.DataAccessMethod.Sequential)]

        [TestMethod]
        [TestCategory("Multiplication")]
        [TestProperty("Dependency", "Yes")]
        [DeploymentItem("data.xlsx")]
        

        public void calculate_using_multiple_test_expression_from_xls_sheet()
        {

            var Expression = Convert.ToString(TestContext.DataRow["Expression"]);
            var ExpectedResult = Convert.ToDouble(TestContext.DataRow["Result"]);
            
                _Calc.ProcessExpression(Expression);
           
            Assert.AreEqual(ExpectedResult, _Calc.CurrentValue,
                $"Expression : {Expression} != {_Calc.CurrentValue}; Expected {ExpectedResult}");
        }

        public TestContext TestContext { get; set; }

    }

    //Also we can perform multiple searching :
    //Trait:"multi" Outcome:"skipped"
}
 