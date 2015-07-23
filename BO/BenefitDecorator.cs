using System;

namespace BO
{
    public abstract class BenefitDecorator : BenefitComponent
    {
        private BenefitComponent _baseComponent = null;

        protected decimal _cost = 0M;
        protected string _name = String.Empty;

        protected BenefitDecorator(BenefitComponent baseComponent)
        {
            _baseComponent = baseComponent;
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
