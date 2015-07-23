using System.Collections.Generic;

namespace BO
{
    public interface IBenefitCalculator
    {
        BenefitResults CalculateBenefits(string employee, IList<string> dependents);
    }
}
