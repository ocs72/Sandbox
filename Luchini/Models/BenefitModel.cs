using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Luchini.Models
{
    public class BenefitModel
    {
        [Required]
        public string Employee { get; set; }

        public IList<string> Dependents { get; set; }

        public BenefitModel()
        {
            Dependents = new List<string>();
        }
    }
}