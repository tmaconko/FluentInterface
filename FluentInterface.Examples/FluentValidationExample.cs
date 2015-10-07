using System.Linq;
using System.Text.RegularExpressions;
using FluentInterface.Examples.Classes;
using FluentValidation;

namespace FluentInterface.Examples
{
    // ReSharper disable once CompareOfFloatsByEqualityOperator

    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.SocialSecurityNumber)
                .NotEmpty()
                .WithMessage("Social security number is required to perform action!")
                .Must(BeAValidSocialSecurityNumber)
                .WithMessage("Social security number is in bad format!");

            RuleFor(customer => customer.Name)
                .NotEmpty()
                .WithMessage("Name is required to perform action!")    
                .Length(5, 50)
                .WithMessage("Name should be 5 to 50 characters length!")
                .Must(p => p.All(char.IsLetter))
                .WithMessage("Only letters can appear in name!");

            RuleForEach(customer => customer.Accounts)
                .NotEmpty()
                .WithMessage("Empty account found!")
                .Must(acc => acc.CurrentBalance == acc.SavedBalance + acc.BalanceDelta)
                .WithMessage("Account's balanace is broken!");
        }

        private static bool BeAValidSocialSecurityNumber(string ssnString)
        {
            if (ssnString.Length != 11)
                return false;

            if (!Regex.IsMatch(ssnString.Substring(0, 1), "[3-6]"))
                return false;

            if (!Regex.IsMatch(ssnString.Substring(1, 2), "[0-9]{2}"))
                return false;

            if (!Regex.IsMatch(ssnString.Substring(3, 1), "[0-1]"))
                return false;

            if (!Regex.IsMatch(ssnString.Substring(4, 1), "[0-9]"))
                return false;

            if (!Regex.IsMatch(ssnString.Substring(5, 1), "[0-3]"))
                return false;

            if (!Regex.IsMatch(ssnString.Substring(6, 1), "[0-9]"))
                return false;

            if (!Regex.IsMatch(ssnString.Substring(7, 4), "[0-9]"))
                return false;

            return true;
        }
    }
}