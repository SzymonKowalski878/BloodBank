using BloodBank.Logics.Interfaces;
using BloodBank.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloodBank.Logics.Validators
{
    public class BloodTypeValidator:AbstractValidator<BloodType>,IMyValidator
    {
        public BloodTypeValidator()
        {
            List<string> bloodTypes = new List<String>() {
                "0Rh+", "0Rh-", "ARh+", "ARh-", "BRh+", "BRh-", "ABRh+", "ABRh-"
            };
            RuleFor(m => m.BloodGroupName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(x => bloodTypes.Contains(x))
                .WithMessage("There is no blood type with that name");
        }
    }
}
