using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using IgrejaOnlineWesleyana.Models;
using IgrejaOnlineWesleyana.ViewModels;

namespace IgrejaOnlineWesleyana.Extensions
{
    public class CPFValidator: AbstractValidator<Membro>
    {
        public CPFValidator()
        {
            RuleFor(x => x.CPF).NotEmpty().WithMessage("CPF é um campo obrigatório").Must(CPFUnico).WithMessage("Este CPF já existe");
        }

        private bool CPFUnico(Membro membro, string CPF)
        {
            var _db = new IMWModel();
            var dbMembro = _db.Membro.SingleOrDefault(x => x.CPF == CPF);

            if (dbMembro == null)
                return true;

            return dbMembro.ID == membro.ID;
        }
    }

    //public class CPFValidatorViewModel : AbstractValidator<FichaCadastralViewModel>
    //{
    //    public CPFValidatorViewModel()
    //    {
    //        RuleFor(x => x.CPF).NotEmpty().WithMessage("CPF é um campo obrigatório").Must(UnicoCPF).WithMessage("Este CPF já existe");
    //    }

    //    private bool UnicoCPF(Membro membro, string CPF)
    //    {
    //        var _db = new IMWModel();
    //        var dbMembro = _db.Membro.SingleOrDefault(x => x.CPF == CPF);

    //        if (dbMembro == null)
    //            return true;

    //        return dbMembro.ID == membro.ID;
    //    }
    //}

}