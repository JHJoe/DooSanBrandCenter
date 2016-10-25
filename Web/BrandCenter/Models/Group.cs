using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrandCenter.Models
{
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