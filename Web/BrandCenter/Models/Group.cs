using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation.Attributes;

namespace BrandCenter.Models
{
    [Validator(typeof(Validators.GroupValidator))]
    [NotMapped]
    public class Group : tblGroup
    {
        [Display(Name = "Full Name")]
        public string NameWithDesc
        {
            get
            {
                return Name + ", " + Descript;
            }
        }



    }
}