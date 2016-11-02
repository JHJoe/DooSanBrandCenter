using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using BrandCenter.Models;

namespace BrandCenter.Validators
{
    public class GroupValidator : AbstractValidator<tblGroup>
    {
        public GroupValidator()
        {
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Descript).NotNull();
            //RuleFor(x => x.GroupId).NotNull().When(x => x.L2D_LAPPURCHASED == -1).WithMessage("'Country' must be selected.");
        }
    }
}