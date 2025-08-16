using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helper
{
    internal static class ValidationHelper
    {
        internal static void ModelValidation(object obj)
        {
            ValidationContext validationContext = new ValidationContext(obj);
            var results = new List<ValidationResult>();


            bool isOk = Validator.TryValidateObject(obj, validationContext, results,validateAllProperties:true);

            if (!isOk)
            {
                foreach (var erro in results)
                {
                    throw new ArgumentException($"{erro.MemberNames}:{erro.ErrorMessage}");
                }
            }
        } 
    }
}
