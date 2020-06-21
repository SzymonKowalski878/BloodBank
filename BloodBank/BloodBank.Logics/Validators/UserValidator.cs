using BloodBank.Logics.Interfaces;
using BloodBank.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace BloodBank.Logics.Validators
{
    public class UserValidator:AbstractValidator<User>,IMyValidator
    {
        public bool ValidatePasswordStrength(string password)
        {
            var pattern = @"^.*((?=.*[!@#$%^&*()\-_=+{};:,<.>]){1})(?=.*\d)((?=.*[a-z]){1})((?=.*[A-Z]){1}).*$";

            return Regex.IsMatch(password,
                pattern,
                RegexOptions.ECMAScript);
        }
        public UserValidator()
        {
            RuleFor(m => m.Password)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(ValidatePasswordStrength);

            RuleFor(m => m.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
