
namespace BO.Decorators
{
    public class DependentDecorator : BenefitDecorator
    {
        public DependentDecorator(BenefitComponent baseComponent)
            : base(baseComponent)
        {
            _cost = baseComponent.GetCost() + 500M;
        }
    }
}
