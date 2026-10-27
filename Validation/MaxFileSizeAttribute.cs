using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MVC_1.Validation
{
    /// <summary>
    /// Власний атрибут: максимальний розмір завантаженого файлу (у мегабайтах).
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MaxFileSizeAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly int _megabytes;
        private long MaxBytes => _megabytes * 1024L * 1024L;

        public MaxFileSizeAttribute(int megabytes)
        {
            _megabytes = megabytes;
            ErrorMessage = "Поле «{0}»: розмір файлу не має перевищувати {1} МБ.";
        }

        public override string FormatErrorMessage(string name)
            => string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, _megabytes);

        public override bool IsValid(object? value)
        {
            if (value is not IFormFile file) return true;
            return file.Length <= MaxBytes;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            ClientValidationHelper.MergeAttribute(context.Attributes, "data-val", "true");
            ClientValidationHelper.MergeAttribute(context.Attributes, "data-val-maxfilesize",
                FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
            ClientValidationHelper.MergeAttribute(context.Attributes, "data-val-maxfilesize-bytes",
                MaxBytes.ToString(CultureInfo.InvariantCulture));
        }
    }
}
