using BloodBank.Logics.Interfaces;
using BloodBank.Logics.Repositories;
using BloodBank.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloodBank.Logics.BloodDonators
{
    public class BloodDonatorLogic:IBloodDonatorLogic
    {
        private readonly Lazy<IBloodDonatorRepository> _bloodDonatorRepository;
        protected IBloodDonatorRepository BloodDonatorRepository => _bloodDonatorRepository.Value;

        public BloodDonatorLogic(Lazy<IBloodDonatorRepository> bloodDonatorRepository)
        {
            _bloodDonatorRepository = bloodDonatorRepository;
        }

        public Result<BloodDonator> UpdateDonatedBloodByUser(string pesel, int ammount)
        {
            if (string.IsNullOrEmpty(pesel) || ammount == 0)
            {
                return Result.Error<BloodDonator>("Pesel was null or ammount equaled 0");
            }

            var donatorToUpdate = BloodDonatorRepository.GetByPesel(pesel);

            if (donatorToUpdate == null)
            {
                return Result.Error<BloodDonator>("Couldn't find blood donator with that pesel");
            }

            donatorToUpdate.AmmountOfBloodDonated += ammount;

            BloodDonatorRepository.SaveChanges();

            return Result.Ok(donatorToUpdate);
        }
    }
}
