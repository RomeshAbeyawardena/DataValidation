using System.ComponentModel.DataAnnotations;
using DataValidation.Extensions;

namespace DataValidation.Mvc
{
    public class UkPostalCodeAttribute : RegularExpressionAttribute
    {
        public UkPostalCodeAttribute(string postalCodeRegularExpression = null) 
            : base(string.IsNullOrEmpty(postalCodeRegularExpression) 
                ? RegularExpressions.UkPostalCode 
                : postalCodeRegularExpression)
        {

        }
    }
}