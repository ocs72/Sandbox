
namespace DAL.DataStore
{
    public class BenefitRepository : IBenefitRepository
    {
        // ********** hard coded for challenge **********

        public BenefitRepository()
        {

        }

        public decimal GetBenefitCost()
        {
            return 1000M;
        }

        public decimal GetSalary()
        {
            return (GetPayPeriods() * 2000M);
        }

        public int GetPayPeriods()
        {
            return 26;
        }
    }
}
