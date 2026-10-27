// Клієнтські адаптери для власних атрибутів валідації (Validation/*.cs).
// Підключається після jquery.validate.unobtrusive.
(function ($) {
    var validator = $.validator;
    var adapters = validator.unobtrusive.adapters;

    // [YearRange] -> data-val-yearrange, -min, -max
    validator.addMethod('yearrange', function (value, element, params) {
        if (this.optional(element)) return true;
        var year = parseInt(value, 10);
        return !isNaN(year) && year >= params.min && year <= params.max;
    });
    adapters.add('yearrange', ['min', 'max'], function (options) {
        options.rules['yearrange'] = {
            min: parseInt(options.params.min, 10),
            max: parseInt(options.params.max, 10)
        };
        options.messages['yearrange'] = options.message;
    });

    // [AllowedExtensions] -> data-val-fileextensions, -extensions
    validator.addMethod('fileextensions', function (value, element, extensions) {
        if (!element.files || element.files.length === 0) return true;
        var name = element.files[0].name.toLowerCase();
        return extensions.split(',').some(function (ext) {
            return name.endsWith(ext.trim());
        });
    });
    adapters.add('fileextensions', ['extensions'], function (options) {
        options.rules['fileextensions'] = options.params.extensions;
        options.messages['fileextensions'] = options.message;
    });

    // [MaxFileSize] -> data-val-maxfilesize, -bytes
    validator.addMethod('maxfilesize', function (value, element, bytes) {
        if (!element.files || element.files.length === 0) return true;
        return element.files[0].size <= bytes;
    });
    adapters.add('maxfilesize', ['bytes'], function (options) {
        options.rules['maxfilesize'] = parseInt(options.params.bytes, 10);
        options.messages['maxfilesize'] = options.message;
    });

    // Перевіряти файл одразу після вибору
    $(function () {
        $('input[type=file]').on('change', function () {
            $(this).valid();
        });
    });
})(jQuery);
