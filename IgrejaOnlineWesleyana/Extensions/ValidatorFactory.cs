using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using IgrejaOnlineWesleyana.Models;
using IgrejaOnlineWesleyana.ViewModels;

namespace IgrejaOnlineWesleyana.Extensions
{
    public class ValidatorFactory : ValidatorFactoryBase
    {
        private static readonly Dictionary<Type, IValidator> Validators = new Dictionary<Type, IValidator>();

        static ValidatorFactory()
        {
            Validators.Add(typeof(IValidator<FichaCadastralViewModel>), new CPFValidatorViewModel());
            Validators.Add(typeof(IValidator<Membro>), new CPFValidator());

        }

        public override IValidator CreateInstance(Type validatorType)
        {
            IValidator validator;
            return Validators.TryGetValue(validatorType, out validator) ? validator : null;
        }
    }
}