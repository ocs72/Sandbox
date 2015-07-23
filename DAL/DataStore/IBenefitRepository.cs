
namespace DAL.DataStore
{
    public interface IBenefitRepository
    {
        decimal GetBenefitCost();

        decimal GetSalary();

        int GetPayPeriods();
    }
}
