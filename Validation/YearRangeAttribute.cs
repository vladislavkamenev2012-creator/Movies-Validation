using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MVC_1.Validation
{
    /// <summary>
    /// Власний атрибут: рік має бути від MinYear до поточного року.
    /// Працює і на сервері (IsValid), і на клієнті (IClientModelValidator + adapter "yearrange" у validation.js).
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class YearRangeAttribute : ValidationAttribute, IClientModelValidator
    {
        public int MinYear { get; }
        public int MaxYear => DateTime.Now.Year;

        public YearRangeAttribute(int minYear)
        {
            MinYear = minYear;
            ErrorMessage = "Поле «{0}» має містити рік від {1} до {2}.";
        }

        public override string FormatErrorMessage(string name)
            => string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, MinYear, MaxYear);

        public override bool IsValid(object? value)
        {
            if (value is null) return true;
            return value is int year && year >= MinYear && year <= MaxYear;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            ClientValidationHelper.MergeAttribute(context.Attributes, "data-val", "true");
            ClientValidationHelper.MergeAttribute(context.Attributes, "data-val-yearrange",
                FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
            ClientValidationHelper.MergeAttribute(context.Attributes, "data-val-yearrange-min",
                MinYear.ToString(CultureInfo.InvariantCulture));
            ClientValidationHelper.MergeAttribute(context.Attributes, "data-val-yearrange-max",
                MaxYear.ToString(CultureInfo.InvariantCulture));
        }
    }
}
