using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Tasks.ValidationAttributes
{
    public class ValidationAttributes
    {
        [AttributeUsage(AttributeTargets.Property)]
        public class PhoneValidatorAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (value is string phone)
                {
                    var pattern = @"(?:\+7|8)?[ |-]?(?:[\d]{3}|\([\d]{3}\))[ |-]?[\d]{3}[ |-]?[\d]{2}[ |-]?[\d]{2}";
                    if (Regex.IsMatch(phone, pattern))
                    {
                        return true;
                    }
                    else
                    {
                        ErrorMessage = "Некорректный формат телефона";
                    }
                }
                return false;
            }
        }

        [AttributeUsage(AttributeTargets.Property)]
        public class EmailValidatorAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (value is string email)
                {
                    var pattern = @"^[\w\.]+@[\w\.]+\.[\w]+$";
                    if (Regex.IsMatch(email, pattern))
                    {
                        return true;
                    }
                    else
                    {
                        ErrorMessage = "Некорректный формат Email";
                    }
                }
                return false;
            }
        }

        public class ContactInfo
        {
            [EmailValidator]
            public string Email { get; set; }
            [PhoneValidator]
            public string Phone { get; set; }
        }
    }
}
