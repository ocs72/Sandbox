using BO;
using BO.Decorators;
using DAL.DataStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Luchini.Tests.BO
{
    [TestClass]
    public class BOTest
    {
        private const decimal _baseCost = 1000M;
        private const decimal _totalSalary = 52000M;
        private readonly IBenefitRepository _benefitRepository;

        public BOTest()
        {
            var mock = new Mock<IBenefitRepository>();
            mock.Setup(x => x.GetBenefitCost()).Returns(_baseCost);
            mock.Setup(x => x.GetPayPeriods()).Returns(26);
            mock.Setup(x => x.GetSalary()).Returns(_totalSalary);

            _benefitRepository = mock.Object;
        }

        [TestMethod]
        public void Calc_Base()
        {
            BenefitBase bb = new BenefitBase(_benefitRepository);

            Assert.AreEqual(bb.GetCost(), _baseCost);
        }

        [TestMethod]
        public void Calc_1_DependentDecorator()
        {
            BenefitBase bb = new BenefitBase(_benefitRepository);

            DependentDecorator dd = new DependentDecorator(bb);

            Assert.AreEqual(dd.GetCost(), 1500M);
        }

        [TestMethod]
        public void Calc_1_ANameDecorator_CapitalA_FirstName()
        {
            BenefitBase bb = new BenefitBase(_benefitRepository);

            ANameDecorator ad = new ANameDecorator(bb, "Albert asdfasdf");

            Assert.AreEqual(ad.GetCost(), 900M);
        }

        [TestMethod]
        public void Calc_1_ANameDecorator_LowerA_LastName()
        {
            BenefitBase bb = new BenefitBase(_benefitRepository);

            ANameDecorator ad = new ANameDecorator(bb, "asdfbert afjkddd");

            Assert.AreEqual(ad.GetCost(), 900M);
        }

        [TestMethod]
        public void Calc_1_ANameDecorator_CapitalA_LastName()
        {
            BenefitBase bb = new BenefitBase(_benefitRepository);

            ANameDecorator ad = new ANameDecorator(bb, "bblbert Asdfasdf");

            Assert.AreEqual(ad.GetCost(), 900M);
        }

        [TestMethod]
        public void Calc_1_ANameDecorator_LowerA_FirstName()
        {
            BenefitBase bb = new BenefitBase(_benefitRepository);

            ANameDecorator ad = new ANameDecorator(bb, "albert dfjkddd");

            Assert.AreEqual(ad.GetCost(), 900M);
        }

        [TestMethod]
        public void Calc_1_ANameDecorator_NonA()
        {
            BenefitBase bb = new BenefitBase(_benefitRepository);

            ANameDecorator ad = new ANameDecorator(bb, "tudfjf gdgfsd");

            Assert.AreEqual(ad.GetCost(), _baseCost);
        }

        [TestMethod]
        public void Calc_1_Dependent_1_ANameDecorator_AName()
        {
            BenefitBase bb = new BenefitBase(_benefitRepository);

            DependentDecorator dd = new DependentDecorator(bb);

            ANameDecorator ad = new ANameDecorator(dd, "xdfsde tttreeee Areess");

            Assert.AreEqual(ad.GetCost(), 1350M);
        }

        [TestMethod]
        public void BenefitCalculator_Benefits_EmployeeOnly_NonA()
        {
            IBenefitCalculator calc = new BenefitCalculator(_benefitRepository);

            BenefitResults cost = calc.CalculateBenefits("qweru qrrrrrr werer", new List<string>());

            Assert.AreEqual(cost.BenefitYear, _baseCost);
            Assert.AreEqual(cost.BenefitCheck, decimal.Round(_baseCost / 26, 2));
        }

        [TestMethod]
        public void BenefitCalculator_Benefits_EmployeeOnly_AName()
        {
            IBenefitCalculator calc = new BenefitCalculator(_benefitRepository);

            BenefitResults cost = calc.CalculateBenefits("xweru Adfd", new List<string>());

            Assert.AreEqual(cost.BenefitYear, 900M);
            Assert.AreEqual(cost.BenefitCheck, decimal.Round(900M / 26, 2));
        }

        [TestMethod]
        public void BenefitCalculator_Benefits_EmployeeOnly_NonA_1_Dependent_NonA()
        {
            IBenefitCalculator calc = new BenefitCalculator(_benefitRepository);

            BenefitResults cost = calc.CalculateBenefits("qweru dssss", new List<string> { "dffdvvv ssssf" });

            var test = _baseCost + 500M;

            Assert.AreEqual(cost.BenefitYear, test);
            Assert.AreEqual(cost.BenefitCheck, decimal.Round(test / 26, 2));
        }

        [TestMethod]
        public void BenefitCalculator_Benefits_EmployeeOnly_AName_1_Dependent_AName()
        {
            IBenefitCalculator calc = new BenefitCalculator(_benefitRepository);

            BenefitResults cost = calc.CalculateBenefits("Aweru", new List<string> { "Afdd" });

            var test = 1260M;

            Assert.AreEqual(cost.BenefitYear, test);
            Assert.AreEqual(cost.BenefitCheck, decimal.Round(test / 26, 2));
        }

        [TestMethod]
        public void BenefitCalculator_Totals_EmployeeOnly_NonA()
        {
            IBenefitCalculator calc = new BenefitCalculator(_benefitRepository);

            BenefitResults cost = calc.CalculateBenefits("qweru qrrrrrr werer", new List<string>());

            Assert.AreEqual(cost.TotalYear, _baseCost + _totalSalary);
            Assert.AreEqual(cost.TotalCheck, decimal.Round((_baseCost + _totalSalary) / 26, 2));
        }

        [TestMethod]
        public void BenefitCalculator_Totals_EmployeeOnly_AName()
        {
            IBenefitCalculator calc = new BenefitCalculator(_benefitRepository);

            BenefitResults cost = calc.CalculateBenefits("xweru Adfd", new List<string>());

            Assert.AreEqual(cost.TotalYear, 900M + _totalSalary);
            Assert.AreEqual(cost.TotalCheck, decimal.Round((900M + _totalSalary) / 26, 2));
        }
    }
}
