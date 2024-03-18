using System;
using System.ComponentModel.DataAnnotations;


namespace MovieAPI.Validations
{
    public class EnumDataTypeValidation :ValidationAttribute
    {
        private readonly Type _enumType;

        public EnumDataTypeValidation(Type enumType)
        {
            if (enumType == null)
                throw new ArgumentNullException(nameof(enumType));

            if (!enumType.IsEnum)
                throw new ArgumentException("The type must be an Enum.", nameof(enumType));

            _enumType = enumType;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"Invalid value for {name}. Accepted values are: {string.Join(", ", Enum.GetNames(_enumType))}.";
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
                return true; // Let RequiredAttribute handle null values

            return Enum.IsDefined(_enumType, value);
        }
    }
}