using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrandCenter.Models
{
    [Table("tblGroup")]
    public class Group
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [StringLength(150, ErrorMessage = "150글자보다 작아야합니다.")]
        [Column("Desc")]
        [Display(Name = "Description")]
        public string Descript { get; set; }

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