using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MVC_1.Validation
{
    /// <summary>
    /// Власний атрибут: дозволені розширення завантаженого файлу.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class AllowedExtensionsAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(params string[] extensions)
        {
            _extensions = extensions.Select(e => e.ToLowerInvariant()).ToArray();
            ErrorMessage = "Поле «{0}»: дозволені формати файлу — {1}.";
        }

        public override string FormatErrorMessage(string name)
            => string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, string.Join(", ", _extensions));

        public override bool IsValid(object? value)
        {
            if (value is not IFormFile file || file.Length == 0) return true;
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            return _extensions.Contains(ext);
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            ClientValidationHelper.MergeAttribute(context.Attributes, "data-val", "true");
            ClientValidationHelper.MergeAttribute(context.Attributes, "data-val-fileextensions",
                FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
            ClientValidationHelper.MergeAttribute(context.Attributes, "data-val-fileextensions-extensions",
                string.Join(",", _extensions));
        }
    }
}
