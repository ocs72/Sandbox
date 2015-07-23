using BO.Decorators;
using DAL.DataStore;
using System;
using System.Collections.Generic;

namespace BO
{
    public class BenefitCalculator : IBenefitCalculator
    {
        private readonly BenefitBase _benefitBase;
        private readonly decimal _salary;
        private readonly int _payPeriods;

        public BenefitCalculator(IBenefitRepository benefitRepository)
        {
            if (benefitRepository == null)
            {
                throw new Exception("BenefitRepository dependency is null");
            }

            _salary = benefitRepository.GetSalary();
            _payPeriods = benefitRepository.GetPayPeriods();

            _benefitBase = new BenefitBase(benefitRepository);
        }

        public BenefitResults CalculateBenefits(string employee, IList<string> dependents)
        {
            BenefitDecorator bd = new ANameDecorator(_benefitBase, employee);

            foreach (var d in dependents)
            {
                if (!String.IsNullOrEmpty(d))
                {
                    bd = new ANameDecorator(new DependentDecorator(bd), d);
                }
            }

            decimal benefitYear = bd.GetCost();
            decimal totalYear = benefitYear + _salary;

            return new BenefitResults
                {
                    BenefitYear = benefitYear,
                    BenefitCheck = decimal.Round(benefitYear / _payPeriods, 2),
                    TotalYear = totalYear,
                    TotalCheck = decimal.Round(totalYear / _payPeriods, 2)
                };
        }
    }
}
