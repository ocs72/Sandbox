using System.Linq;

namespace BO.Decorators
{
    public class ANameDecorator : BenefitDecorator
    {
        public ANameDecorator(BenefitComponent baseComponent, string name)
            : base(baseComponent)
        {
            decimal cost = baseComponent.GetCost();

            _cost = (name.Split(' ').Any(x => x.StartsWith("A", System.StringComparison.InvariantCultureIgnoreCase)))
                ? cost - (cost * .1M) : cost;
        }
    }
}
