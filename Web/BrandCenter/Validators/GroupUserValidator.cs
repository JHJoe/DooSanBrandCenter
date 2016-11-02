using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using BrandCenter.Models;

namespace BrandCenter.Validators
{
    public class GroupUserValidator : AbstractValidator<ViewModels.GroupUser>
    {
        public GroupUserValidator()
        {
            RuleFor(x => x.GROUPID).NotNull();
            RuleFor(x => x.USERID).NotNull().Must(UserIdExist);
            RuleFor(x => x.NAME).NotNull().NotEqual("error");
            //RuleFor(x => x.USERID).NotNull().When(x => x.GROUPID == -1).WithMessage("'Country' must be selected.");
        }

        private bool UserIdExist(short userid)
        {
            //사용자id가 존재하는지 db 서치하고..(실제로 keyin하진 않지만)
            if (userid.Equals(57))
                return false;
            else
                return true;

        }
    }
}