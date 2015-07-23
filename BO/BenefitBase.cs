using DAL.DataStore;
using System;

namespace BO
{
	public class BenefitBase : BenefitComponent
	{
		private decimal _cost;
		private string _name = String.Empty;

		public BenefitBase(IBenefitRepository benefitCost)
		{
			_cost = benefitCost.GetBenefitCost();
		}

		public override decimal GetCost()
		{
			return _cost;
		}

		public override string DependentName()
		{
			return _name;
		}
	}
}
