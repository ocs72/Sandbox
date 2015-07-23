using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Luchini.Models
{
    public class StringListNoEmptyValues : ValidationAttribute, IClientValidatable
    {
        public string CommaSeperatedProperties { get; private set; }

        public override bool IsValid(object strings)
        {
            return Valid((IList<string>)strings);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            return new[] { new ModelClientStringListNoEmptyValues(FormatErrorMessage(metadata.DisplayName), 
                CommaSeperatedProperties.Split(new char[] { ',' })) };
        }

        public class ModelClientStringListNoEmptyValues : ModelClientValidationRule
        {
            public ModelClientStringListNoEmptyValues(string errorMessage, string[] strProperties)
            {
                ErrorMessage = errorMessage;
                ValidationType = "mycustomvalidation";
                for (int intIndex = 0; intIndex < strProperties.Length; intIndex++)
                {
                    ValidationParameters.Add("otherproperty" + intIndex.ToString(), strProperties[intIndex]);
                }
            }
        }

        private bool Valid(IList<string> strings)
        {
            return strings.Any(x => !String.IsNullOrEmpty(x));
        }
    }
}