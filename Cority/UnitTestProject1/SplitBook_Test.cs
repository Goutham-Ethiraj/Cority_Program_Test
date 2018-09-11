using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cority;

namespace UnitTestProject1
{
    [TestClass]
    public class SplitBook_Test
    {
        SplitBook obj_SplitBook;
        [TestInitialize]
        public void Initialize()
        {
            obj_SplitBook = SplitBook.GetObject();
        }

        [TestMethod]
        public void fnCalculateProfitOrLoss_Test_1()
        {
            decimal d_total;
            decimal[][] d_IndividualContribution = fnCreateCaculateProfitOrLossData(out d_total);
            string[] str_Actual_Result = obj_SplitBook.fnfnCalculateProfitOrLoss_Adapter(d_IndividualContribution, d_total);
            Assert.AreEqual(str_Actual_Result[0], "($1.99)");
            Assert.AreEqual(str_Actual_Result[1], "($8.01)");
            Assert.AreEqual(str_Actual_Result[2], "$10.01");
        }
        [TestMethod]
        public void fnCalculateProfitOrLoss_Test_2()
        {
            decimal d_total;
            decimal[][] d_IndividualContribution = fnCreateCaculateProfitOrLossData(out d_total);
            string[] str_Actual_Result = obj_SplitBook.fnfnCalculateProfitOrLoss_Adapter(d_IndividualContribution, d_total);
            Assert.AreEqual("$-1.99", str_Actual_Result[0]);
            Assert.AreEqual("$-8.01", str_Actual_Result[1]);
            Assert.AreEqual("$10.01", str_Actual_Result[2]);
        }
        [TestMethod]
        public void fnCalculateProfitOrLoss_Test_3()
        {
            decimal d_total;
            decimal[][] d_IndividualContribution = fnCreateCaculateProfitOrLossData_2(out d_total);
            string[] str_Actual_Result = obj_SplitBook.fnfnCalculateProfitOrLoss_Adapter(d_IndividualContribution, d_total);
            Assert.AreEqual("$(2.73)", str_Actual_Result[0]);
            Assert.AreEqual("$2.73", str_Actual_Result[1]);
        }
        [TestMethod]
        public void fnCalculateProfitOrLoss_Test_4()
        {
            decimal d_total;
            decimal[][] d_IndividualContribution = fnCreateCaculateProfitOrLossData_2(out d_total);
            string[] str_Actual_Result = obj_SplitBook.fnfnCalculateProfitOrLoss_Adapter(d_IndividualContribution, d_total);
            Assert.AreEqual("$2.73", str_Actual_Result[0]);
            Assert.AreEqual("$(2.73)", str_Actual_Result[1]);

        }

        private static decimal[][] fnCreateCaculateProfitOrLossData(out decimal d_OverAllTotal)
        {
            decimal[][] d_Actual_Individual_Total = new decimal[3][];
            d_OverAllTotal = new decimal(84.02);
            d_Actual_Individual_Total[0] = new decimal[] { 30m };
            d_Actual_Individual_Total[1] = new decimal[] { 36.02m };
            d_Actual_Individual_Total[2] = new decimal[] { 18m };
            return d_Actual_Individual_Total;
        }

        private static decimal[][] fnCreateCaculateProfitOrLossData_2(out decimal d_OverAllTotal)
        {

            decimal[][] d_Actual_Individual_Total = new decimal[2][];
            d_OverAllTotal = new decimal(116.45);
            d_Actual_Individual_Total[0] = new decimal[] { 30m, 25.5m };
            d_Actual_Individual_Total[1] = new decimal[] { 0m, 60.95m };
            return d_Actual_Individual_Total;
        }

        [TestCleanup]
        public void Complete()
        {
            obj_SplitBook = null;
        }
    }
}
