using BloodBank.Logics.Interfaces;
using BloodBank.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BloodBank.Logics.Validators
{
    public class BloodDonatorValidator:AbstractValidator<BloodDonator>,IMyValidator
    {
        public bool PeselValidation(DateTime birthDate,string pesel)
        {
            if (pesel.Length!=11)
            {
                return false;
            }
            if(!Int64.TryParse(pesel,out var num))
            {
                return false;
            }
            var tmpMonth = Int32.Parse(pesel[2].ToString() + pesel[3]);

            if (tmpMonth >= 21 && tmpMonth <= 32)
            {
                tmpMonth -= 20;
            }

            if (tmpMonth >= 41 && tmpMonth <= 52)
            {
                tmpMonth -= 40;
            }

            if (birthDate.Year % 100 != Int32.Parse(pesel[0].ToString() + pesel[1].ToString()))
            {
                return false;
            }

            if (birthDate.Month != tmpMonth)
            {
                return false;
            }

            if (birthDate.Day != Int32.Parse(pesel[4].ToString() + pesel[5].ToString()))
            {
                return false;
            }

            int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            var controlNum = 10 - pesel.Zip(weights, (l1, l2) => Int32.Parse(l1.ToString()) * l2).Sum() % 10;

            if (controlNum != Int32.Parse(pesel[10].ToString()))
            {
                return false;
            }

            return true;
        }

        public bool PhoneNumberValidator(string phoneNumber)
        {
            return Int32.TryParse(phoneNumber, out var number);
        }

        public BloodDonatorValidator()
        {
            //RuleFor(m => m.BirthDate)
            //    .Cascade(CascadeMode.StopOnFirstFailure)
            //    .NotEmpty()
            //    .LessThan(p => DateTime.Now.AddYears(-18))
            //    .WithMessage("Failure to validate birth date");

            //RuleFor(m => m)
            //    .Cascade(CascadeMode.StopOnFirstFailure)
            //    .Must(c => PeselValidation(c.BirthDate, c.Pesel))
            //    .WithMessage("Failure to validate pesel");

            //RuleFor(m => m.FirstName)
            //    .Cascade(CascadeMode.StopOnFirstFailure)
            //    .NotEmpty()
            //    .MaximumLength(30)
            //    .WithMessage("Failure to validate firstname");

            //RuleFor(m => m.Surname)
            //    .Cascade(CascadeMode.StopOnFirstFailure)
            //    .NotEmpty()
            //    .MaximumLength(30)
            //    .WithMessage("Failure to validate surname");

            //RuleFor(m => m.PhoneNumber)
            //    .Cascade(CascadeMode.StopOnFirstFailure)
            //    .Must(x => x.ToString().Length == 9)
            //    .Must(PhoneNumberValidator)
            //    .WithMessage("Failure to validate phone number");

            //RuleFor(m => m.HomeAdress)
            //    .Cascade(CascadeMode.StopOnFirstFailure)
            //    .NotEmpty()
            //    .MaximumLength(50)
            //    .WithMessage("Failure to validate home adress");

            RuleSet("PhoneAndAdress", () =>
            {
                RuleFor(m => m.PhoneNumber)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .Must(x => x.ToString().Length == 9)
                .Must(PhoneNumberValidator)
                .WithMessage("Failure to validate phone number");

                RuleFor(m => m.HomeAdress)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Failure to validate home adress");
            });

            RuleSet("ValidateRestOfProperties", () =>
             {
                 RuleFor(m => m.BirthDate)
                 .Cascade(CascadeMode.StopOnFirstFailure)
                 .NotEmpty()
                 .LessThan(p => DateTime.Now.AddYears(-18))
                 .WithMessage("Failure to validate birth date");

                 RuleFor(m => m)
                     .Cascade(CascadeMode.StopOnFirstFailure)
                     .Must(c => PeselValidation(c.BirthDate, c.Pesel))
                     .WithMessage("Failure to validate pesel");

                 RuleFor(m => m.FirstName)
                     .Cascade(CascadeMode.StopOnFirstFailure)
                     .NotEmpty()
                     .MaximumLength(30)
                     .WithMessage("Failure to validate firstname");

                 RuleFor(m => m.Surname)
                     .Cascade(CascadeMode.StopOnFirstFailure)
                     .NotEmpty()
                     .MaximumLength(30)
                     .WithMessage("Failure to validate surname");
             });
        }
    }
}
