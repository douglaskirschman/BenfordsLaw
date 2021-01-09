using Microsoft.VisualStudio.TestTools.UnitTesting;
using BenfordsLaw.Processor;

namespace BenfordsLaw.Test
{
    [TestClass]
    public class BenfordsLawTests
    {
        [TestMethod]
        public void RandomBenfordTest()
        {
            var logic = new Logic();
            var benford = logic.CalcRandomBenfordsLawResult(1000, 1, 500);

            Assert.IsTrue(benford.Result.TotalElements == 1000);
        }
    }
}
